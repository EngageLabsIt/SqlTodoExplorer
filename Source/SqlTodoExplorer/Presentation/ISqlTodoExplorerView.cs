using System;
using System.Collections.Generic;
using System.IO;
using DamnTools.SqlTodoExplorer.Presentation.Model;
using DamnTools.SqlTodoExplorer.Presentation.Model.Data;
using DamnTools.SqlTodoExplorer.Services;

namespace DamnTools.SqlTodoExplorer.Presentation
{
    public interface ISqlTodoExplorerView
    {
        event Action RefreshClicked;

        event Action DatabaseSelected;

        event Action CommentTypeFilterSelected;

        event Action GroupByItemSelected;

        event Action SaveFileDialogTxtConfirmed;

        event Action SaveFileDialogXmlConfirmed;

        event Action SearchTextChanged;

        event Action<TreeNodeMetaData> NodeDoubleClicked;

        string SearchWatermark { get; set; }

        string SearchText { get; set; }

        DatabaseName Database { get; set; }

        GroupByItem GroupBy { get; set; }

        FilterItem FilterType { get; set; }

        void LoadGroupByItems(IEnumerable<GroupByItem> groupByItems);

        void LoadCommentTypeFilters(IEnumerable<FilterItem> filters);

        void LoadDatabases(IEnumerable<DatabaseName> databases);

        void LoadResultsByObjectType(IEnumerable<RoutineDataRecord> routines);

        void LoadResultsByCommentType(IEnumerable<RoutineDataRecord> routines);

        void LoadResultsInFlatList(IEnumerable<RoutineDataRecord> routines);

        void ClearSelectedCommentTypeFilter();

        void ClearSelectedGroupBy();

        void ClearSelectedDatabase();

        /// <summary>
        /// removes the row selection on grid at load or change text
        /// </summary>
        void ClearSelectedResults();

        /// <summary>
        /// Shows the treeview instead of datagridview (hierarchycal list)
        /// </summary>
        void EnableTreeview();

        /// <summary>
        /// Enables the treeview based controls
        /// </summary>
        void EnableTreeviewBasedItems();

        /// <summary>
        /// Shows the datagridview instead of treeview (flat list)
        /// </summary>
        void EnableDatagridview();

        /// <summary>
        /// Disables the treeview based controls
        /// </summary>
        void DisableTreeviewBasedItems();

        void ApplyFilter(string filter);

        void FocusResultsView();

        Stream OpenFileTxt();

        Stream OpenFileXml();

        /// <summary>
        /// Returns a DatabaseName object by a given id
        /// </summary>
        DatabaseName GetDabaseNameFromId(int id);

        /// <summary>
        /// Sets a DatabaseName object to the database list based control
        /// </summary>
        void SetDabaseName(DatabaseName database);
    }
}