using System.Collections.Generic;
using DamnTools.SqlTodoExplorer.Presentation.Model.Data;
using DamnTools.SqlTodoExplorer.Services;

namespace DamnTools.SqlTodoExplorer.Presentation.Model
{
    public interface IPresentationDataService
    {
        /// <summary>
        /// Gets data from the source provider.
        /// </summary>
        List<RoutineDataRecord> GetDataRecords(string databaseName, ITodoPattern searchPattern);

        /// <summary>
        /// Gets database list from the source provider.
        /// </summary>
        List<DatabaseDataRecord> GetDatabases();

        /// <summary>
        /// Gets all the search patterns.
        /// </summary>
        IReadOnlyList<ITodoPattern> GetPatterns(); 

        /// <summary>
        /// Refresh the connection selecting the current instance 
        /// where the user is working on. It first checks the active 
        /// script/document, if not document is active, it fallsback
        /// to the object explorer and get the selected instance.
        /// </summary>
        void RefreshCurrentConnection();

        /// <summary>
        /// Script the object as alter, and move the caret where 
        /// the to-do has been found.
        /// </summary>
        void NavigateTo(TodoItem todoItem);
    }
}