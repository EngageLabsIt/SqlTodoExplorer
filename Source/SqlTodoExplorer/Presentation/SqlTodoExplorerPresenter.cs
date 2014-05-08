using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DamnTools.SqlTodoExplorer.Presentation.Model;
using DamnTools.SqlTodoExplorer.Presentation.Model.Data;
using DamnTools.SqlTodoExplorer.Services;
using Resources = DamnTools.SqlTodoExplorer.Properties.Resources;

namespace DamnTools.SqlTodoExplorer.Presentation
{
    public class SqlTodoExplorerPresenter
    {
        private readonly ISqlTodoExplorerView _view;
        private readonly IPresentationDataService _dataService;

        public List<RoutineDataRecord> DataRecords { get; private set; }

        public SqlTodoExplorerPresenter(ISqlTodoExplorerView view, IPresentationDataService dataService)
        {
            _view = view;
            _dataService = dataService;

            _view.RefreshClicked += ViewOnRefreshClicked;
            _view.DatabaseSelected += ViewOnDatabaseSelected;
            _view.CommentTypeFilterSelected += ViewOnCommentTypeFilterSelected;
            _view.GroupByItemSelected += ViewOnGroupByItemSelected;
            _view.SaveFileDialogTxtConfirmed += ViewOnSaveFileDialogTxtConfirmed;
            _view.SaveFileDialogXmlConfirmed += ViewOnSaveFileDialogXmlConfirmed;
            _view.SearchTextChanged += ViewOnSearchTextChanged;
            _view.NodeDoubleClicked += ViewOnNodeDoubleClicked;
        }
        
        public void ViewOnRefreshClicked()
        {
            // gets the selected database id (prior to refhresh)
            var databaseId = _view.Database.Id;

            // updates/rebinds controls
            RefreshControlsAndRebindData();
            BindDatabases();

            // selects the last database selected
            _view.SetDabaseName(_view.GetDabaseNameFromId(databaseId));
        }

        public void ViewOnDatabaseSelected()
        {
            RefreshControlsAndRebindData();
        }

        /// <summary>
        /// Changes the treeview results based on FilterItemType 
        /// </summary>
        public void ViewOnCommentTypeFilterSelected()
        {
            if (_view.FilterType != null)
            {
                var selectedFilterOption = _view.FilterType.Id;
                var comboBoxGroupByItem = _view.GroupBy;

                if (comboBoxGroupByItem == null)
                {
                    _view.EnableTreeview();
                    _view.EnableTreeviewBasedItems();
                    BindTreeviewData(GroupByItemType.Unknown, selectedFilterOption);
                }
                else
                {
                    var selectedGroupByOption = comboBoxGroupByItem.Id;

                    if (selectedGroupByOption != GroupByItemType.Flat)
                    {
                        _view.EnableTreeview();
                        _view.EnableTreeviewBasedItems();
                        BindTreeviewData(selectedGroupByOption, selectedFilterOption);
                    }
                    else
                    {
                        _view.EnableDatagridview();
                        _view.DisableTreeviewBasedItems();
                        BindFlatListData(selectedFilterOption);
                    }
                }

                _view.FocusResultsView();
            }
        }

        /// <summary>
        /// Changes the result view layout (grouped by object types or comment type)
        /// </summary>
        public void ViewOnGroupByItemSelected()
        {
            _view.SearchText = string.Empty;

            if (_view.GroupBy != null)
            {
                var selectedGroupByOption = _view.GroupBy.Id;
                var comboBoxFilterItem = _view.FilterType;

                if (comboBoxFilterItem == null)
                {
                    if (selectedGroupByOption != GroupByItemType.Flat)
                    {
                        _view.EnableTreeview();
                        _view.EnableTreeviewBasedItems();
                        BindTreeviewData(selectedGroupByOption, null);
                    }
                    else
                    {
                        _view.EnableDatagridview();
                        _view.DisableTreeviewBasedItems();
                        BindFlatListData(null);
                    }
                }
                else
                {
                    var selectedFilterOption = ((FilterItem)comboBoxFilterItem).Id;
                    if (selectedGroupByOption != GroupByItemType.Flat)
                    {
                        _view.EnableTreeview();
                        _view.EnableTreeviewBasedItems();
                        BindTreeviewData(selectedGroupByOption, selectedFilterOption);
                    }
                    else
                    {
                        _view.EnableDatagridview();
                        _view.DisableTreeviewBasedItems();
                        BindFlatListData(selectedFilterOption);
                    }
                }

                _view.FocusResultsView();
            }
        }

        /// <summary>
        /// Export to TXT file (csv format)
        /// </summary>
        public void ViewOnSaveFileDialogTxtConfirmed()
        {
            using (var file = new StreamWriter(_view.OpenFileTxt()))
            {
                try
                {
                    var sb = new StringBuilder();

                    // headers
                    sb.AppendFormat("{0}, ", Resources.ColumnObjectId);
                    sb.AppendFormat("{0}, ", Resources.ColumnSchema);
                    sb.AppendFormat("{0}, ", Resources.ColumnName);
                    sb.AppendFormat("{0}, ", Resources.ColumnFullName);
                    sb.AppendFormat("{0}, ", Resources.ColumnType);
                    sb.AppendFormat("{0}, ", Resources.ColumnCommentType);
                    sb.AppendFormat("{0}{1}", Resources.ColumnDefinition, Environment.NewLine);

                    foreach (var record in this.DataRecords)
                    {
                        if (!String.IsNullOrEmpty(_view.SearchText))
                        {
                            if (record.Definition.Contains(_view.SearchText))
                            {
                                sb.AppendFormat("{0}, ", record.Id);
                                sb.AppendFormat("{0}, ", record.Schema);
                                sb.AppendFormat("{0}, ", record.Name);
                                sb.AppendFormat("{0}, ", record.FullName);
                                sb.AppendFormat("{0}, ", record.Type);
                                sb.AppendFormat("{0}, ", record.Tag);
                                sb.AppendFormat("{0}{1}", record.Definition, Environment.NewLine);
                            }
                        }
                        else
                        {
                            sb.AppendFormat("{0}, ", record.Id);
                            sb.AppendFormat("{0}, ", record.Schema);
                            sb.AppendFormat("{0}, ", record.Name);
                            sb.AppendFormat("{0}, ", record.FullName);
                            sb.AppendFormat("{0}, ", record.Type);
                            sb.AppendFormat("{0}, ", record.Tag);
                            sb.AppendFormat("{0}{1}", record.Definition, Environment.NewLine);
                        }
                    }

                    file.WriteLine(sb.ToString());
                    file.Close();

                    Trace.TraceInformation("File saved successfully");
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    file.Close();
                }
            }
        }

        /// <summary>
        /// Export to XML file
        /// </summary>
        public void ViewOnSaveFileDialogXmlConfirmed()
        {
            using (var file = new StreamWriter(_view.OpenFileXml()))
            {
                try
                {
                    var xml = new XElement("Comments", this.DataRecords
                                            .Where(x => x.Definition.Contains(_view.SearchText))
                                            .Select(x => new XElement("Comment",
                                                new XAttribute("ObjectId", x.Id),
                                                new XAttribute("Schema", x.Schema),
                                                new XAttribute("ObjectName", x.Name),
                                                new XAttribute("FullName", x.FullName),
                                                new XAttribute("Type", x.Type),
                                                new XAttribute("Tag", x.Tag),
                                                new XAttribute("Definition", x.Definition))));

                    xml.Save(file);
                    file.Close();

                    Trace.TraceInformation("File saved successfully");
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    file.Close();
                }
            }
        }

        /// <summary>
        /// Filters with the as you type search
        /// </summary>
        public void ViewOnSearchTextChanged()
        {
            var selectedGroupByType = _view.GroupBy.Id;
            switch (selectedGroupByType)
            {
                case GroupByItemType.Flat:
                    _view.ApplyFilter(_view.SearchText);
                    break;
                case GroupByItemType.CommentType:
                case GroupByItemType.ObjectType:
                    break;
            }

            _view.ClearSelectedResults();
        }

        /// <summary>
        /// Script the object as alter, and move the caret where 
        /// the to-do has been found.
        /// </summary>
        public void ViewOnNodeDoubleClicked(TreeNodeMetaData treeNodeMetaData)
        {
            if (treeNodeMetaData == null) return;
            if (treeNodeMetaData.TodoItem == null) return;
            
            _dataService.NavigateTo(treeNodeMetaData.TodoItem);
        }

        /// <summary>
        /// Inits the controls
        /// </summary>
        public void Init()
        {
            _view.SearchWatermark = Resources.SearchWatermark;

            // toolbox data load
            BindToolboxData();

            _view.ClearSelectedCommentTypeFilter();
            _view.ClearSelectedGroupBy();

            // gets all data
            _view.EnableTreeview();
            BindTreeviewData(GroupByItemType.Unknown, null);
        }

        /// <summary>
        /// Loads toolstrip control data
        /// </summary>
        public void BindToolboxData()
        {
            BindGroupByObjectTypes();
            BindFilterItemTypes();
            BindDatabases();
        }

        /// <summary>
        /// Binds the group by type list
        /// </summary>
        public void BindGroupByObjectTypes()
        {
            var groupByItemTypeObjectType = new GroupByItem { Id = GroupByItemType.ObjectType, Description = Resources.GroupByItemTypeObjectType };
            var groupByItemTypeCommentType = new GroupByItem { Id = GroupByItemType.CommentType, Description = Resources.GroupByItemTypeCommentType };
            var groupByItemTypeFlat = new GroupByItem { Id = GroupByItemType.Flat, Description = Resources.GroupByItemTypeFlat };

            _view.LoadGroupByItems(new[] { groupByItemTypeObjectType, groupByItemTypeCommentType, groupByItemTypeFlat });
        }

        /// <summary>
        /// Binds the filter type list
        /// </summary>
        public void BindFilterItemTypes()
        {
            var patterns = _dataService.GetPatterns();
            
            var patternList = new List<FilterItem>();

            patternList.Add(new FilterItem { Id = null, Description = Resources.FilterItemTypeAll });
            patternList.AddRange(patterns.Select(todoPattern => new FilterItem {Id = todoPattern, Description = todoPattern.Title}));

            _view.LoadCommentTypeFilters(patternList);
        }

        /// <summary>
        /// Binds the database list given by the data source
        /// </summary>
        public void BindDatabases()
        {
            List<DatabaseDataRecord> data = _dataService.GetDatabases();

            var database = data.Select(t => new DatabaseName { Id = t.Id, Name = t.Name }).ToList();

            _view.LoadDatabases(database);

            _view.ClearSelectedDatabase();
        }

        /// <summary>
        /// Binds the treeview from data source
        /// </summary>
        /// <param name="groupByItemType">Layout stlye</param>
        /// <param name="todoPattern">ITodoPattern filter</param>
        public void BindTreeviewData(GroupByItemType groupByItemType, ITodoPattern todoPattern)
        {
            var data = GetRoutineDataRecords(_view.Database.Name, todoPattern);

            switch (groupByItemType)
            {
                case GroupByItemType.Unknown:
                case GroupByItemType.ObjectType:
                    _view.LoadResultsByObjectType(data);
                    break;
                case GroupByItemType.CommentType:
                    _view.LoadResultsByCommentType(data);
                    break;
                default:
                    _view.LoadResultsByObjectType(data);
                    break;
            }
        }

        /// <summary>
        /// Groups the results by a flat list
        /// </summary>
        /// <param name="todoPattern">ITodoPattern filter</param>
        public void BindFlatListData(ITodoPattern todoPattern)
        {
            var data = GetRoutineDataRecords(_view.Database.Name, todoPattern);

            _view.LoadResultsInFlatList(data);

            _view.ApplyFilter(_view.SearchText);

            _view.ClearSelectedResults();
        }

        /// <summary>
        /// Updates the status of the results controls
        /// </summary>
        public void RefreshControlsAndRebindData()
        {
            var filterSelectedItem = _view.FilterType;
            var groupBySelectedItem = _view.GroupBy;

            if (filterSelectedItem == null || groupBySelectedItem == null)
                return;

            var selectedGroupByOption = groupBySelectedItem.Id;
            var selectedFilterOption = filterSelectedItem.Id;
            
            if (selectedGroupByOption != GroupByItemType.Flat)
            {
                _view.EnableTreeview();
                _view.EnableTreeviewBasedItems();
                BindTreeviewData(selectedGroupByOption, selectedFilterOption);
            }
            else
            {
                _view.EnableDatagridview();
                _view.DisableTreeviewBasedItems();
                BindFlatListData(selectedFilterOption);
            }
        }

        public List<RoutineDataRecord> GetRoutineDataRecords(string database, ITodoPattern searchPattern)
        {
            this.DataRecords = _dataService.GetDataRecords(database, searchPattern);
            return this.DataRecords;
        }
    }
}