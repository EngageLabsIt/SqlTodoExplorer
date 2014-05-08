using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DamnTools.SqlTodoExplorer.Presentation.Helpers;
using DamnTools.SqlTodoExplorer.Presentation.Model;
using DamnTools.SqlTodoExplorer.Presentation.Model.Data;
using DamnTools.SqlTodoExplorer.Services;
using Microsoft.SqlServer.Management.Smo;
using Resources = DamnTools.SqlTodoExplorer.Properties.Resources;
using RoutineType = DamnTools.SqlTodoExplorer.Presentation.Model.Data.RoutineType;

namespace DamnTools.SqlTodoExplorer.Presentation
{
    public partial class SqlTodoExplorerView : UserControl, ISqlTodoExplorerView
    {
        public SqlTodoExplorerView()
        {
            InitializeComponent();

            InitializeControls();
        }

        #region Initializations

        /// <summary>
        /// Inits the controls
        /// </summary>
        private void InitializeControls()
        {
            toolStripMainMenu.Renderer = new ToolStripRender();

            if (toolStripComboBoxCommentTypeFilter.ComboBox != null)
            {
                toolStripComboBoxCommentTypeFilter.ComboBox.DisplayMember = "Description";
                toolStripComboBoxCommentTypeFilter.ComboBox.ValueMember = "Id";
                toolStripComboBoxCommentTypeFilter.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                this.toolStripComboBoxCommentTypeFilter.ComboBox.SelectionChangeCommitted += this.toolStripComboBoxCommentTypeFilter_SelectionChangeCommitted;
            }

            if (toolStripComboBoxGroupBy.ComboBox != null)
            {
                toolStripComboBoxGroupBy.ComboBox.DisplayMember = "Description";
                toolStripComboBoxGroupBy.ComboBox.ValueMember = "Id";
                toolStripComboBoxGroupBy.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                this.toolStripComboBoxGroupBy.ComboBox.SelectionChangeCommitted += this.toolStripComboBoxGroupBy_SelectionChangeCommitted;
            }

            if (toolStripComboBoxDatabases.ComboBox != null)
            {
                toolStripComboBoxDatabases.ComboBox.DisplayMember = "Name";
                toolStripComboBoxDatabases.ComboBox.ValueMember = "Id";
                toolStripComboBoxDatabases.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            //as you type search
            textBoxSearch.KeyUp += textBoxSearch_KeyUp;

            //buttons
            toolStripButtonCollapseAll.Enabled = true;
            toolStripButtonExpandAll.Enabled = true;

            //events
            toolStripButtonExpandAll.Click += toolStripButtonExpandAll_Click;
            toolStripButtonCollapseAll.Click += toolStripButtonCollapseAll_Click;
            toolStripMenuItemExportAsText.Click += toolStripMenuItemExportAsText_Click;
            toolStripButtonExport.ButtonClick += toolStripButtonExport_ButtonClick;
            toolStripMenuItemExportAsXml.Click += toolStripMenuItemExportAsXml_Click;
            toolStripButtonRefresh.Click += toolStripButtonRefresh_Click;
            saveFileDialogTxt.FileOk += saveFileDialogTxt_FileOk;
            saveFileDialogXml.FileOk += saveFileDialogXml_FileOk;
            textBoxSearch.KeyUp += textBoxSearch_KeyUp;
            if (toolStripComboBoxDatabases.ComboBox != null)
                toolStripComboBoxDatabases.ComboBox.SelectionChangeCommitted += toolStripComboBoxDatabases_SelectionChangeCommitted;
            treeViewResults.NodeMouseDoubleClick += treeViewResults_NodeMouseDoubleClick;
        }

        #endregion

        #region Interface Events Implementation

        public event Action SearchTextChanged;

        protected virtual void OnSearchTextChanged()
        {
            var handler = SearchTextChanged;
            if (handler != null) handler();
        }

        public event Action SaveFileDialogTxtConfirmed;

        protected virtual void OnSaveFileDialogTxtConfirmed()
        {
            var handler = SaveFileDialogTxtConfirmed;
            if (handler != null) handler();
        }

        public event Action SaveFileDialogXmlConfirmed;

        protected virtual void OnSaveFileDialogXmlConfirmed()
        {
            var handler = SaveFileDialogXmlConfirmed;
            if (handler != null) handler();
        }

        public event Action GroupByItemSelected;

        protected virtual void OnGroupByItemSelected()
        {
            var handler = GroupByItemSelected;
            if (handler != null) handler();
        }

        public event Action CommentTypeFilterSelected;

        protected virtual void OnCommentTypeFilterSelected()
        {
            var handler = CommentTypeFilterSelected;
            if (handler != null) handler();
        }

        public event Action DatabaseSelected;

        protected virtual void OnDatabaseSelected()
        {
            var handler = DatabaseSelected;
            if (handler != null) handler();
        }

        public event Action RefreshClicked;

        protected virtual void OnRefreshClick()
        {
            var handler = RefreshClicked;
            if (handler != null) handler();
        }

        public event Action<TreeNodeMetaData> NodeDoubleClicked;

        protected virtual void OnNodeDoubleClicked(TreeNodeMetaData obj)
        {
            Action<TreeNodeMetaData> handler = NodeDoubleClicked;
            if (handler != null) handler(obj);
        }

        #endregion

        #region Interface Properties Implementation

        private string _searchWatermark;
        public string SearchWatermark
        {
            get
            {
                return _searchWatermark;
            }
            set
            {
                _searchWatermark = value;
                this.textBoxSearch.SetWatermark(value);
            }
        }

        public string SearchText
        {
            get
            {
                return this.textBoxSearch.Text;
            }
            set
            {
                this.textBoxSearch.Text = value;
            }
        }

        public DatabaseName Database
        {
            get
            {
                return (DatabaseName)this.toolStripComboBoxDatabases.SelectedItem;
            }
            set
            {
                this.toolStripComboBoxDatabases.SelectedItem = value;
            }
        }

        public GroupByItem GroupBy
        {
            get
            {
                return (GroupByItem)this.toolStripComboBoxGroupBy.SelectedItem;
            }
            set
            {
                this.toolStripComboBoxGroupBy.SelectedItem = value;
            }
        }

        public FilterItem FilterType
        {
            get
            {
                return (FilterItem)this.toolStripComboBoxCommentTypeFilter.SelectedItem;
            }
            set
            {
                this.toolStripComboBoxCommentTypeFilter.SelectedItem = value;
            }
        }

        #endregion

        #region Interface Methods Implementation

        public void LoadGroupByItems(IEnumerable<GroupByItem> groupByItems)
        {
            this.toolStripComboBoxGroupBy.BeginUpdate();
            try
            {
                this.toolStripComboBoxGroupBy.Items.Clear();
                foreach (var groupByItem in groupByItems)
                {
                    this.toolStripComboBoxGroupBy.Items.Add(groupByItem);
                }
            }
            finally
            {
                this.toolStripComboBoxGroupBy.EndUpdate();
            }
        }

        public void LoadCommentTypeFilters(IEnumerable<FilterItem> filters)
        {
            this.toolStripComboBoxCommentTypeFilter.BeginUpdate();
            try
            {
                this.toolStripComboBoxCommentTypeFilter.Items.Clear();
                foreach (var filter in filters)
                {
                    this.toolStripComboBoxCommentTypeFilter.Items.Add(filter);
                }
            }
            finally
            {
                this.toolStripComboBoxCommentTypeFilter.EndUpdate();
            }
        }

        public void LoadDatabases(IEnumerable<DatabaseName> databases)
        {
            this.toolStripComboBoxDatabases.BeginUpdate();
            try
            {
                this.toolStripComboBoxDatabases.Items.Clear();
                foreach (var database in databases)
                {
                    this.toolStripComboBoxDatabases.Items.Add(database);
                }
            }
            finally
            {
                this.toolStripComboBoxDatabases.EndUpdate();
            }
        }

        public void LoadResultsByObjectType(IEnumerable<RoutineDataRecord> routines)
        {
            var treeNodes = new List<TreeNode>();

            foreach (var record in routines)
            {
                switch (record.Type)
                {
                    case RoutineType.StoredProcedure:

                        UpdateCollectionWithProcedureRootNode(treeNodes, record);

                        break;
                    case RoutineType.Function:

                        UpdateCollectionWithFunctionRootNode(treeNodes, record);

                        break;
                    case RoutineType.Unknown:
                        break;
                }
            }

            UpdateTreeView(treeNodes);
        }

        public void LoadResultsByCommentType(IEnumerable<RoutineDataRecord> routines)
        {
            var treeNodes = new List<TreeNode>();

            foreach (var record in routines)
            {
                UpdateCollectionWithCommentRootNode(treeNodes, record);
            }

            UpdateTreeView(treeNodes);
        }

        public void LoadResultsInFlatList(IEnumerable<RoutineDataRecord> routines)
        {
            dataGridViewResults.DataSource = null;

            var table = new DataTable();
            table.Columns.AddRange(new[]
            {
                new DataColumn("Object_id"),
                new DataColumn("Schema"),
                new DataColumn("Name"),
                new DataColumn("FullName"),
                new DataColumn("Type"),
                new DataColumn("Tag"),
                new DataColumn("Comment")
            });

            foreach (var record in routines)
            {
                var row = table.NewRow();
                row["Object_id"] = record.Id;
                row["Schema"] = record.Schema;
                row["Name"] = record.Name;
                row["FullName"] = record.FullName;
                row["Type"] = record.Type;
                row["Tag"] = record.Tag;
                row["Comment"] = record.Definition;

                table.Rows.Add(row);
            }
            dataGridViewResults.DataSource = table;

            // ASK: Could it be better to move it in the init phase, and refers to the column name using const string
            // column properties
            dataGridViewResults.Columns["Object_id"].Width = 80;
            dataGridViewResults.Columns["Object_id"].HeaderText = Resources.ColumnObjectId;
            dataGridViewResults.Columns["Schema"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewResults.Columns["Schema"].HeaderText = Resources.ColumnSchema;
            dataGridViewResults.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewResults.Columns["Name"].HeaderText = Resources.ColumnName;
            dataGridViewResults.Columns["FullName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewResults.Columns["FullName"].HeaderText = Resources.ColumnFullName;
            dataGridViewResults.Columns["Type"].Width = 120;
            dataGridViewResults.Columns["Type"].HeaderText = Resources.ColumnType;
            dataGridViewResults.Columns["Tag"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewResults.Columns["Tag"].HeaderText = Resources.ColumnCommentType;
            dataGridViewResults.Columns["Comment"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewResults.Columns["Comment"].HeaderText = Resources.ColumnDefinition;
        }

        public void ClearSelectedCommentTypeFilter()
        {
            this.toolStripComboBoxCommentTypeFilter.SelectedIndex = 0;
        }

        public void ClearSelectedGroupBy()
        {
            this.toolStripComboBoxGroupBy.SelectedIndex = 0;
        }

        public void ClearSelectedDatabase()
        {
            toolStripComboBoxDatabases.SelectedIndex = 0;
        }

        public void ClearSelectedResults()
        {
            if (dataGridViewResults.Rows.Count > 0) dataGridViewResults.Rows[0].Selected = false;
        }

        public void EnableTreeview()
        {
            treeViewResults.Visible = true;
            treeViewResults.Enabled = true;
            dataGridViewResults.Visible = false;
            dataGridViewResults.Enabled = false;
            textBoxSearch.Enabled = false;
        }

        public void EnableTreeviewBasedItems()
        {
            toolStripButtonCollapseAll.Enabled = true;
            toolStripButtonExpandAll.Enabled = true;
        }

        public void EnableDatagridview()
        {
            treeViewResults.Visible = false;
            treeViewResults.Enabled = false;
            dataGridViewResults.Visible = true;
            dataGridViewResults.Enabled = true;
            textBoxSearch.Enabled = true;
        }

        public void DisableTreeviewBasedItems()
        {
            toolStripButtonCollapseAll.Enabled = false;
            toolStripButtonExpandAll.Enabled = false;
        }

        public void ApplyFilter(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                var dataSource = dataGridViewResults.DataSource as DataTable;
                if (dataSource != null)
                {
                    var view = dataSource.DefaultView;
                    view.RowFilter = String.Format("Comment LIKE '%{0}%'", filter);
                }
            }
        }

        public void FocusResultsView()
        {
           this.treeViewResults.Select();
        }

        public Stream OpenFileTxt()
        {
            return this.saveFileDialogTxt.OpenFile();
        }

        public Stream OpenFileXml()
        {
            return this.saveFileDialogXml.OpenFile();
        }

        #endregion

        #region Interface Events Forwarders

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            OnRefreshClick();
        }

        private void toolStripComboBoxCommentTypeFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {
            OnCommentTypeFilterSelected();
        }

        private void toolStripComboBoxGroupBy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            OnGroupByItemSelected();
        }

        private void toolStripComboBoxDatabases_SelectionChangeCommitted(object sender, EventArgs e)
        {
            OnDatabaseSelected();
        }

        private void saveFileDialogTxt_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OnSaveFileDialogTxtConfirmed();
        }

        private void saveFileDialogXml_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OnSaveFileDialogXmlConfirmed();
        }

        private void textBoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            OnSearchTextChanged();
        }

        private void treeViewResults_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs treeNodeMouseClickEventArgs)
        {
            var treeNodeMetaData = treeNodeMouseClickEventArgs.Node.Tag as TreeNodeMetaData;
            if (treeNodeMetaData != null)
            {
                OnNodeDoubleClicked(treeNodeMetaData);
            }
        }

        public DatabaseName GetDabaseNameFromId(int id)
        {
            return this.toolStripComboBoxDatabases.Items.Cast<DatabaseName>().FirstOrDefault(item => item.Id == id);
        }

        public void SetDabaseName(DatabaseName database)
        {
            this.toolStripComboBoxDatabases.SelectedItem = database;
        }

        #endregion

        #region Layout management

        private void UpdateTreeView(IEnumerable<TreeNode> nodes)
        {
            this.treeViewResults.BeginUpdate();
            try
            {
                this.treeViewResults.Nodes.Clear();

                // results load
                foreach (var item in nodes)
                {
                    this.treeViewResults.Nodes.Add(item);
                }
            }
            finally
            {
                this.treeViewResults.EndUpdate();
            }
            treeViewResults.ExpandAll();
            treeViewResults.SelectedNode = treeViewResults.Nodes.Cast<TreeNode>().FirstOrDefault();
            // scroll top 
            if (treeViewResults.SelectedNode != null) treeViewResults.SelectedNode.EnsureVisible();
        }

        /// <summary>
        /// Updates the given collection with a new item (OBJECT TYPE LAYOUT, Procedures)
        /// </summary>
        /// <param name="collectionToBeUpdated">Collection to be updated</param>
        /// <param name="newItem">New item to manage</param>
        private static void UpdateCollectionWithProcedureRootNode(List<TreeNode> collectionToBeUpdated, RoutineDataRecord newItem)
        {
            // root node
            TreeNode rootNode = null;
            if (collectionToBeUpdated.All(n => ((TreeNodeMetaData)n.Tag).NodeType != NodeType.RootStoredProcedure))
            {
                rootNode = new TreeNode
                {
                    Text = Resources.RootNodeTextProcedures,
                    Tag = new TreeNodeMetaData
                    {
                        NodeType = NodeType.RootStoredProcedure
                    },
                    ImageKey = @"ico_folder",
                    SelectedImageKey = @"ico_folder"
                };
                collectionToBeUpdated.Add(rootNode);
            }
            else
            {
                rootNode = collectionToBeUpdated.First(n => ((TreeNodeMetaData)n.Tag).NodeType == NodeType.RootStoredProcedure);
            }

            // schema node
            var nodeSchema = rootNode.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.Schema && t.Name == newItem.Schema);

            if (nodeSchema == null)
            {
                // adds the new schema node to the root node
                nodeSchema = new TreeNode
                {
                    Name = newItem.Schema,
                    Text = String.Format(Resources.NodeTextSchema, newItem.Schema),
                    Tag = new TreeNodeMetaData
                    {
                        NodeType = NodeType.Schema
                    },
                    ImageKey = @"ico_schema",
                    SelectedImageKey = @"ico_schema"
                };
                rootNode.Nodes.Add(nodeSchema);
            }

            // item node
            var nodeItem = nodeSchema.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.StoredProcedure && t.Name == newItem.FullName);

            if (nodeItem == null)
            {
                nodeItem = new TreeNode
                {
                    Name = newItem.FullName,
                    Text = newItem.FullName,
                    Tag = new TreeNodeMetaData
                    {
                        NodeType = NodeType.StoredProcedure
                    },
                    ImageKey = @"ico_procedure",
                    SelectedImageKey = @"ico_procedure"
                };
                nodeSchema.Nodes.Add(nodeItem);
            }

            // comment node
            var nodeComment = nodeItem.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.Comment && t.Name == newItem.CommentIndex.ToString());

            if (nodeComment == null)
            {
                nodeComment = new TreeNode
                {
                    Name = newItem.CommentIndex.ToString(),
                    Text = newItem.Definition,
                    Tag = new TreeNodeMetaData
                    {
                        NodeType = NodeType.Comment,
                        TodoItem = newItem.TodoItem
                    },
                    ImageKey = string.Format("ico_{0}", newItem.Tag.Title.ToLower()),
                    SelectedImageKey = string.Format("ico_{0}", newItem.Tag.Title.ToLower())
                };
                nodeItem.Nodes.Add(nodeComment);
            }

        }

        /// <summary>
        /// Updates the given collection with a new item (OBJECT TYPE LAYOUT, Functions)
        /// </summary>
        /// <param name="collectionToBeUpdated">Collection to be updated</param>
        /// <param name="newItem">New item to manage</param>
        private static void UpdateCollectionWithFunctionRootNode(List<TreeNode> collectionToBeUpdated, RoutineDataRecord newItem)
        {
            // root node
            TreeNode rootNode = null;
            if (collectionToBeUpdated.All(n => ((TreeNodeMetaData)n.Tag).NodeType != NodeType.RootFunction))
            {
                rootNode = new TreeNode
                {
                    Text = Resources.RootNodeTextFunctions,
                    Tag = new TreeNodeMetaData
                    {
                        NodeType = NodeType.RootFunction
                    },
                    ImageKey = @"ico_folder",
                    SelectedImageKey = @"ico_folder"
                };
                collectionToBeUpdated.Add(rootNode);
            }
            else
            {
                rootNode = collectionToBeUpdated.First(n => ((TreeNodeMetaData)n.Tag).NodeType == NodeType.RootFunction);
            }

            // schema node
            var nodeSchema = rootNode.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.Schema && t.Name == newItem.Schema);

            if (nodeSchema == null)
            {
                // adds the new schema node to the root node
                nodeSchema = new TreeNode
                {
                    Name = newItem.Schema,
                    Text = String.Format(Resources.NodeTextSchema, newItem.Schema),
                    Tag = new TreeNodeMetaData
                    {
                        NodeType = NodeType.Schema
                    },
                    ImageKey = @"ico_schema",
                    SelectedImageKey = @"ico_schema"
                };
                rootNode.Nodes.Add(nodeSchema);
            }

            // item node
            var nodeItem = nodeSchema.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.Function && t.Name == newItem.FullName);

            if (nodeItem == null)
            {
                nodeItem = new TreeNode
                {
                    Name = newItem.FullName,
                    Text = newItem.FullName,
                    Tag = new TreeNodeMetaData
                    {
                        NodeType = NodeType.Function
                    },
                    ImageKey = @"ico_function",
                    SelectedImageKey = @"ico_function"
                };
                nodeSchema.Nodes.Add(nodeItem);
            }

            // comment node
            var nodeComment = nodeItem.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.Comment && t.Name == newItem.CommentIndex.ToString());

            if (nodeComment == null)
            {
                nodeComment = new TreeNode
                {
                    Name = newItem.CommentIndex.ToString(),
                    Text = newItem.Definition,
                    Tag = new TreeNodeMetaData
                    {
                        NodeType = NodeType.Comment,
                        TodoItem = newItem.TodoItem
                    },
                    ImageKey = string.Format("ico_{0}", newItem.Tag.Title.ToLower()),
                    SelectedImageKey = string.Format("ico_{0}", newItem.Tag.Title.ToLower())
                };
                nodeItem.Nodes.Add(nodeComment);
            }
        }

        /// <summary>
        /// Updates the given collection with a new item (COMMENT TYPE LAYOUT, Hack)
        /// </summary>
        /// <param name="collectionToBeUpdated">Collection to be updated</param>
        /// <param name="newItem">New item to manage</param>
        private static void UpdateCollectionWithCommentRootNode(List<TreeNode> collectionToBeUpdated, RoutineDataRecord newItem)
        {
            // root node
            TreeNode rootNode = null;
            if (collectionToBeUpdated.All(n => (n.Name != newItem.Tag.Title)))
            {
                rootNode = new TreeNode
                {
                    Name = newItem.Tag.Title,
                    Text = newItem.Tag.Title,
                    Tag = new TreeNodeMetaData
                    {
                        NodeType = NodeType.Comment
                    },
                    ImageKey = string.Format("ico_{0}", newItem.Tag.Title.ToLower()),
                    SelectedImageKey = string.Format("ico_{0}", newItem.Tag.Title.ToLower())
                };
                collectionToBeUpdated.Add(rootNode);
            }
            else
            {
                rootNode = collectionToBeUpdated.First(n => (n.Name == newItem.Tag.Title));
            }

            // schema node
            var nodeSchema = rootNode.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.Schema && t.Name == newItem.Schema);

            if (nodeSchema == null)
            {
                // adds the new schema node to the root node
                nodeSchema = new TreeNode
                {
                    Name = newItem.Schema,
                    Text = String.Format(Resources.NodeTextSchema, newItem.Schema),
                    Tag = new TreeNodeMetaData
                    {
                        NodeType = NodeType.Schema
                    },
                    ImageKey = @"ico_schema",
                    SelectedImageKey = @"ico_schema"
                };
                rootNode.Nodes.Add(nodeSchema);
            }

            // object type node
            TreeNode nodeObjectType = null;
            switch (newItem.Type)
            {
                case RoutineType.StoredProcedure:
                    nodeObjectType = nodeSchema.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.RootStoredProcedure);

                    if (nodeObjectType == null)
                    {
                        nodeObjectType = new TreeNode
                        {
                            Text = Resources.RootNodeTextProcedures,
                            Tag = new TreeNodeMetaData
                            {
                                NodeType = NodeType.RootStoredProcedure
                            },
                            ImageKey = @"ico_procedure",
                            SelectedImageKey = @"ico_procedure"
                        };
                        nodeSchema.Nodes.Add(nodeObjectType);
                    }
                    break;
                case RoutineType.Function:
                    nodeObjectType = nodeSchema.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.RootFunction);

                    if (nodeObjectType == null)
                    {
                        nodeObjectType = new TreeNode
                        {
                            Text = Resources.RootNodeTextFunctions,
                            Tag = new TreeNodeMetaData
                            {
                                NodeType = NodeType.RootFunction
                            },
                            ImageKey = @"ico_function",
                            SelectedImageKey = @"ico_function"
                        };
                        nodeSchema.Nodes.Add(nodeObjectType);
                    }
                    break;
                default:
                    break;
            }

            // item node
            if (nodeObjectType != null)
            {
                var nodeItem = nodeObjectType.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.StoredProcedure && t.Name == newItem.FullName);

                if (nodeItem == null)
                {
                    nodeItem = new TreeNode
                    {
                        Name = newItem.FullName,
                        Text = newItem.FullName,
                        Tag = new TreeNodeMetaData
                        {
                            NodeType = NodeType.StoredProcedure
                        },
                        ImageKey = @"ico_procedure",
                        SelectedImageKey = @"ico_procedure"
                    };
                    nodeObjectType.Nodes.Add(nodeItem);
                }

                // comment node
                var nodeComment = nodeItem.Nodes.Cast<TreeNode>().FirstOrDefault(t => ((TreeNodeMetaData)t.Tag).NodeType == NodeType.Comment && t.Name == newItem.Id.ToString());

                if (nodeComment == null)
                {
                    nodeComment = new TreeNode
                    {
                        Name = newItem.Id.ToString(),
                        Text = newItem.Definition,
                        Tag = new TreeNodeMetaData
                        {
                            NodeType = NodeType.Comment,
                            TodoItem = newItem.TodoItem
                        },
                        ImageKey = string.Format("ico_{0}", newItem.Tag.Title.ToLower()),
                        SelectedImageKey = string.Format("ico_{0}", newItem.Tag.Title.ToLower())
                    };
                    nodeItem.Nodes.Add(nodeComment);
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Expands all nodes of the treeview
        /// </summary>
        private void toolStripButtonExpandAll_Click(object sender, EventArgs e)
        {
            treeViewResults.ExpandAll();
        }

        /// <summary>
        /// Collapse all nodes of the treeview
        /// </summary>
        private void toolStripButtonCollapseAll_Click(object sender, EventArgs e)
        {
            treeViewResults.CollapseAll();
        }

        /// <summary>
        /// Shows the save file dialog (TXT)
        /// </summary>
        private void toolStripMenuItemExportAsText_Click(object sender, EventArgs e)
        {
            saveFileDialogTxt.Filter = Resources.FileTypeTxt;
            saveFileDialogTxt.ShowDialog();
        }

        /// <summary>
        /// Default export behavior
        /// </summary>
        private void toolStripButtonExport_ButtonClick(object sender, EventArgs e)
        {
            // default behavior
            saveFileDialogTxt.Filter = Resources.FileTypeTxt;
            saveFileDialogTxt.ShowDialog();
        }

        /// <summary>
        /// Shows the save file dialog (XML)
        /// </summary>
        private void toolStripMenuItemExportAsXml_Click(object sender, EventArgs e)
        {
            saveFileDialogXml.Filter = Resources.FileTypeXml;
            saveFileDialogXml.ShowDialog();
        }

        #endregion
    }
}
