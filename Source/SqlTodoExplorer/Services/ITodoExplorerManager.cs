using System.Collections.Generic;

namespace DamnTools.SqlTodoExplorer.Services
{
    public interface ITodoExplorerManager
    {
        IReadOnlyList<string> GetDatabases(bool includeSystemDatabases = false);

        IReadOnlyList<TodoItem> GetTodoItems(string database = null, ITodoPattern pattern = null, bool includeSystemDatabases = false);

        IReadOnlyList<TodoItem> GetTodoItems(IEnumerable<string> databases, IEnumerable<ITodoPattern> patterns);

        IReadOnlyList<ITodoPattern> GetTodoPatterns();

        void NavigateTo(TodoItem todoItem);
    }
}