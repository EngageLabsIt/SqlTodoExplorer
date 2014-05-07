using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DamnTools.SqlTodoExplorer.Presentation.Model.Data;
using DamnTools.SqlTodoExplorer.Services;
using RoutineType = DamnTools.SqlTodoExplorer.Presentation.Model.Data.RoutineType;

namespace DamnTools.SqlTodoExplorer.Presentation.Model
{
    public class PresentationDataService : IPresentationDataService
    {
        private ITodoExplorerManager _todoExplorerManager;

        public PresentationDataService()
        {
            _todoExplorerManager = TodoExplorer.CreateFromCurrentConnection();
        }

        /// <summary>
        /// Gets data from the source provider
        /// </summary>
        public List<RoutineDataRecord> GetDataRecords(string databaseName, ITodoPattern searchPattern)
        {
            var todoItems = _todoExplorerManager.GetTodoItems(databaseName, searchPattern);
            var dataRecords = todoItems.Select(t => new RoutineDataRecord
            {
                Id = t.RoutineId,
                Schema = t.RoutineSchema,
                Name = t.RoutineName,
                Definition = t.Text,
                Tag = t.Pattern,
                Type = GetDataRoutineType(t.RoutineType),
                CommentIndex = t.TextIndex,
                TodoItem = t
            }).ToList();
            return dataRecords;
        }

        /// <summary>
        /// Gets database list from the source provider
        /// </summary>
        public List<DatabaseDataRecord> GetDatabases()
        {
            var databases = _todoExplorerManager.GetDatabases();
            var dataRecords = databases.Select(t => new DatabaseDataRecord
            {
                Id = t.GetHashCode(), // HACK: the API requires only the database name. You can compare strings instead of the id if you're using in the view for filtering or so on.
                Name = t
            }).ToList();
            return dataRecords;
        }

        /// <summary>
        /// Gets all the search patterns.
        /// </summary>
        public IReadOnlyList<ITodoPattern> GetPatterns()
        {
            return _todoExplorerManager.GetTodoPatterns();
        }

        /// <summary>
        /// Refresh the connection selecting the current instance 
        /// where the user is working on. It first checks the active 
        /// script/document, if not document is active, it fallsback
        /// to the object explorer and get the selected instance.
        /// </summary>
        public void RefreshCurrentConnection()
        {
            _todoExplorerManager = TodoExplorer.CreateFromCurrentConnection();
        }

        /// <summary>
        /// Script the object as alter, and move the caret where 
        /// the to-do has been found.
        /// </summary>
        public void NavigateTo(TodoItem todoItem)
        {
            _todoExplorerManager.NavigateTo(todoItem);
        }

        public RoutineType GetDataRoutineType(Services.RoutineType type)
        {
            switch (type)
            {
                case Services.RoutineType.StoredProcedure:
                    return RoutineType.StoredProcedure;
                case Services.RoutineType.Function:
                    return RoutineType.Function;
                default:
                    return RoutineType.Unknown;
            }
        }
    }
}
