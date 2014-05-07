using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DamnTools.SqlTodoExplorer.Services
{
    public class TodoExplorerManager : ITodoExplorerManager
    {
        private readonly IServerGateway _serverGateway;
        private readonly IScriptService _scriptService;
        private readonly ITodoPatternService _todoPatternService;
        private readonly string[] _databaseExclusionList;

        public TodoExplorerManager(
            IServerGateway serverGateway, 
            IScriptService scriptService,
            ITodoPatternService todoPatternService)
        {
            _serverGateway = serverGateway;
            _scriptService = scriptService;
            _todoPatternService = todoPatternService;

            _databaseExclusionList = Resources.Database_ExclusionList.Split(',');
        }

        public IReadOnlyList<string> GetDatabases(bool includeSystemDatabases = false)
        {
            if (includeSystemDatabases)
            {
                return _serverGateway.GetDatabases().ToList();
            }
            return _serverGateway.GetDatabases()
                                 .Except(_databaseExclusionList)
                                 .ToList();
        }

        public IReadOnlyList<TodoItem> GetTodoItems(string database = null, ITodoPattern pattern = null, bool includeSystemDatabases = false)
        {
            IReadOnlyList<string> databases = database != null ? new[] { database } : this.GetDatabases(includeSystemDatabases);
            IReadOnlyList<ITodoPattern> patterns = pattern != null ? new[] { pattern } : this.GetTodoPatterns();
            return GetTodoItems(databases, patterns);
        }

        public IReadOnlyList<TodoItem> GetTodoItems(IEnumerable<string> databases, IEnumerable<ITodoPattern> patterns)
        {
            var sw = Stopwatch.StartNew();
            var todoItems = new List<TodoItem>();
            var routines = _serverGateway.GetRoutines(databases);
            Trace.TraceInformation("GetRoutines elapsed milliseconds: {0}", sw.ElapsedMilliseconds);
            sw.Restart();
            foreach (var routine in routines.Where(t => t.Id.HasValue &&
                                                        !string.IsNullOrWhiteSpace(t.Definition)))
            {
                foreach (var todoPattern in patterns.OfType<TodoPattern>())
                {
                    var regex = todoPattern.PatternRegex;
                    var matches = regex.Matches(routine.Definition);
                    int count = 0;
                    foreach (Match match in matches)
                    {
                        count++;
                        var tag = match.Groups["TAG"];
                        var todoItem = new TodoItem
                        {
                            Tag = tag.Success ? match.Groups["TAG"].Value : "TODO",
                            Text = match.Value,
                            TextIndex = match.Index,
                            TextLength = match.Length,
                            TextOccurrence = count,
                            Pattern = todoPattern,
                            Database = routine.Database,
                            RoutineId = routine.Id.Value,
                            RoutineSchema = routine.Schema,
                            RoutineName = routine.Name,
                            RoutineType = routine.Type
                        };
                        todoItems.Add(todoItem);
                    }
                }
            }
            sw.Stop();
            Trace.TraceInformation("Regex parsing elapsed milliseconds: {0}", sw.ElapsedMilliseconds);
            return todoItems;
        }

        public IReadOnlyList<ITodoPattern> GetTodoPatterns()
        {
            return _todoPatternService.GetTodoPatterns();
        }

        public void NavigateTo(TodoItem todoItem)
        {
            this.ScriptAsAlter(todoItem);

            // TODO: move the caret to the todo position (vedi: GotoLine, MoveToPoint, MoveToLineAndOffset, MoveToAbsoluteOffset, FindPattern)
        }

        public void ScriptAsCreate(TodoItem todoItem)
        {
            var routine = _serverGateway.GetRoutine(todoItem.Database, todoItem.RoutineId);
            _scriptService.CreateNewScript(routine.Definition);
        }

        public void ScriptAsAlter(TodoItem todoItem)
        {
            var routine = _serverGateway.GetRoutine(todoItem.Database, todoItem.RoutineId);

            var definition = Regex.Replace(routine.Definition, Resources.ScriptAsAlter_ReplaceRegex, "ALTER", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var sb = new StringBuilder();
            sb.AppendFormat("USE [{0}]", todoItem.Database);
            sb.AppendLine();
            sb.AppendLine("GO");
            sb.Append(definition);

            _scriptService.CreateNewScript(sb.ToString());
        }
    }
}