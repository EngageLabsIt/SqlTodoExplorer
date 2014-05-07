namespace DamnTools.SqlTodoExplorer.Presentation
{
    partial class SqlTodoExplorerView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlTodoExplorerView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageListTreeview = new System.Windows.Forms.ImageList(this.components);
            this.saveFileDialogTxt = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialogXml = new System.Windows.Forms.SaveFileDialog();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelResultsContainer = new System.Windows.Forms.Panel();
            this.treeViewResults = new System.Windows.Forms.TreeView();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.toolStripMainMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExpandAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLocate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExport = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemExportAsText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExportAsXml = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelFilterCommentType = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxCommentTypeFilter = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelGroupBy = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxGroupBy = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelDatabase = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxDatabases = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOptions = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel.SuspendLayout();
            this.panelResultsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.toolStripMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListTreeview
            // 
            this.imageListTreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeview.ImageStream")));
            this.imageListTreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeview.Images.SetKeyName(0, "ico_folder");
            this.imageListTreeview.Images.SetKeyName(1, "ico_todo");
            this.imageListTreeview.Images.SetKeyName(2, "ico_bug");
            this.imageListTreeview.Images.SetKeyName(3, "ico_hack");
            this.imageListTreeview.Images.SetKeyName(4, "ico_custom");
            this.imageListTreeview.Images.SetKeyName(5, "ico_schema");
            this.imageListTreeview.Images.SetKeyName(6, "ico_function");
            this.imageListTreeview.Images.SetKeyName(7, "ico_procedure");
            this.imageListTreeview.Images.SetKeyName(8, "ico_ask");
            // 
            // textBoxSearch
            // 
            resources.ApplyResources(this.textBoxSearch, "textBoxSearch");
            this.textBoxSearch.Name = "textBoxSearch";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.panelResultsContainer, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.textBoxSearch, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.toolStripMainMenu, 0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // panelResultsContainer
            // 
            this.panelResultsContainer.Controls.Add(this.treeViewResults);
            this.panelResultsContainer.Controls.Add(this.dataGridViewResults);
            resources.ApplyResources(this.panelResultsContainer, "panelResultsContainer");
            this.panelResultsContainer.Name = "panelResultsContainer";
            // 
            // treeViewResults
            // 
            this.treeViewResults.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.treeViewResults, "treeViewResults");
            this.treeViewResults.FullRowSelect = true;
            this.treeViewResults.ImageList = this.imageListTreeview;
            this.treeViewResults.Name = "treeViewResults";
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.AllowUserToResizeRows = false;
            this.dataGridViewResults.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewResults.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewResults.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dataGridViewResults, "dataGridViewResults");
            this.dataGridViewResults.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridViewResults.MultiSelect = false;
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewResults.RowHeadersVisible = false;
            this.dataGridViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResults.ShowCellErrors = false;
            this.dataGridViewResults.ShowCellToolTips = false;
            this.dataGridViewResults.ShowEditingIcon = false;
            this.dataGridViewResults.ShowRowErrors = false;
            // 
            // toolStripMainMenu
            // 
            resources.ApplyResources(this.toolStripMainMenu, "toolStripMainMenu");
            this.toolStripMainMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripMainMenu.GripMargin = new System.Windows.Forms.Padding(5);
            this.toolStripMainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExpandAll,
            this.toolStripButtonCollapseAll,
            this.toolStripButtonLocate,
            this.toolStripSeparator5,
            this.toolStripButtonExport,
            this.toolStripSeparator1,
            this.toolStripLabelFilterCommentType,
            this.toolStripComboBoxCommentTypeFilter,
            this.toolStripSeparator2,
            this.toolStripLabelGroupBy,
            this.toolStripComboBoxGroupBy,
            this.toolStripSeparator3,
            this.toolStripLabelDatabase,
            this.toolStripComboBoxDatabases,
            this.toolStripSeparator4,
            this.toolStripButtonRefresh,
            this.toolStripButtonOptions});
            this.toolStripMainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripMainMenu.Name = "toolStripMainMenu";
            // 
            // toolStripButtonExpandAll
            // 
            this.toolStripButtonExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExpandAll.Image = global::DamnTools.SqlTodoExplorer.Properties.Resources.Expand;
            resources.ApplyResources(this.toolStripButtonExpandAll, "toolStripButtonExpandAll");
            this.toolStripButtonExpandAll.Margin = new System.Windows.Forms.Padding(1, 1, 0, 2);
            this.toolStripButtonExpandAll.Name = "toolStripButtonExpandAll";
            // 
            // toolStripButtonCollapseAll
            // 
            this.toolStripButtonCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCollapseAll.Image = global::DamnTools.SqlTodoExplorer.Properties.Resources.Collapse;
            resources.ApplyResources(this.toolStripButtonCollapseAll, "toolStripButtonCollapseAll");
            this.toolStripButtonCollapseAll.Name = "toolStripButtonCollapseAll";
            // 
            // toolStripButtonLocate
            // 
            this.toolStripButtonLocate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLocate.Image = global::DamnTools.SqlTodoExplorer.Properties.Resources.arrow_Sync_16xLG;
            resources.ApplyResources(this.toolStripButtonLocate, "toolStripButtonLocate");
            this.toolStripButtonLocate.Name = "toolStripButtonLocate";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.ForeColor = System.Drawing.Color.LightGray;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolStripButtonExport
            // 
            this.toolStripButtonExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExportAsText,
            this.toolStripMenuItemExportAsXml});
            this.toolStripButtonExport.Name = "toolStripButtonExport";
            resources.ApplyResources(this.toolStripButtonExport, "toolStripButtonExport");
            // 
            // toolStripMenuItemExportAsText
            // 
            this.toolStripMenuItemExportAsText.Image = global::DamnTools.SqlTodoExplorer.Properties.Resources.Textfile_818_16x;
            this.toolStripMenuItemExportAsText.Name = "toolStripMenuItemExportAsText";
            resources.ApplyResources(this.toolStripMenuItemExportAsText, "toolStripMenuItemExportAsText");
            // 
            // toolStripMenuItemExportAsXml
            // 
            this.toolStripMenuItemExportAsXml.Image = global::DamnTools.SqlTodoExplorer.Properties.Resources.XMLFile_828_16x;
            this.toolStripMenuItemExportAsXml.Name = "toolStripMenuItemExportAsXml";
            resources.ApplyResources(this.toolStripMenuItemExportAsXml, "toolStripMenuItemExportAsXml");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.LightGray;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripLabelFilterCommentType
            // 
            this.toolStripLabelFilterCommentType.Margin = new System.Windows.Forms.Padding(0, 4, 0, 2);
            this.toolStripLabelFilterCommentType.Name = "toolStripLabelFilterCommentType";
            resources.ApplyResources(this.toolStripLabelFilterCommentType, "toolStripLabelFilterCommentType");
            // 
            // toolStripComboBoxCommentTypeFilter
            // 
            resources.ApplyResources(this.toolStripComboBoxCommentTypeFilter, "toolStripComboBoxCommentTypeFilter");
            this.toolStripComboBoxCommentTypeFilter.Name = "toolStripComboBoxCommentTypeFilter";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.LightGray;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripLabelGroupBy
            // 
            this.toolStripLabelGroupBy.Margin = new System.Windows.Forms.Padding(0, 4, 0, 2);
            this.toolStripLabelGroupBy.Name = "toolStripLabelGroupBy";
            resources.ApplyResources(this.toolStripLabelGroupBy, "toolStripLabelGroupBy");
            // 
            // toolStripComboBoxGroupBy
            // 
            resources.ApplyResources(this.toolStripComboBoxGroupBy, "toolStripComboBoxGroupBy");
            this.toolStripComboBoxGroupBy.Name = "toolStripComboBoxGroupBy";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.LightGray;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripLabelDatabase
            // 
            this.toolStripLabelDatabase.Margin = new System.Windows.Forms.Padding(0, 4, 0, 2);
            this.toolStripLabelDatabase.Name = "toolStripLabelDatabase";
            resources.ApplyResources(this.toolStripLabelDatabase, "toolStripLabelDatabase");
            // 
            // toolStripComboBoxDatabases
            // 
            resources.ApplyResources(this.toolStripComboBoxDatabases, "toolStripComboBoxDatabases");
            this.toolStripComboBoxDatabases.Name = "toolStripComboBoxDatabases";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.ForeColor = System.Drawing.Color.LightGray;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = global::DamnTools.SqlTodoExplorer.Properties.Resources.refresh_16xLG;
            resources.ApplyResources(this.toolStripButtonRefresh, "toolStripButtonRefresh");
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            // 
            // toolStripButtonOptions
            // 
            this.toolStripButtonOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOptions.Image = global::DamnTools.SqlTodoExplorer.Properties.Resources.PropertyIcon;
            resources.ApplyResources(this.toolStripButtonOptions, "toolStripButtonOptions");
            this.toolStripButtonOptions.Name = "toolStripButtonOptions";
            // 
            // SqlTodoExplorerView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "SqlTodoExplorerView";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.panelResultsContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.toolStripMainMenu.ResumeLayout(false);
            this.toolStripMainMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialogTxt;
        private System.Windows.Forms.SaveFileDialog saveFileDialogXml;
        private System.Windows.Forms.ImageList imageListTreeview;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panelResultsContainer;
        private System.Windows.Forms.TreeView treeViewResults;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.ToolStrip toolStripMainMenu;
        private System.Windows.Forms.ToolStripButton toolStripButtonExpandAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonCollapseAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonLocate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSplitButton toolStripButtonExport;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExportAsText;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExportAsXml;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelFilterCommentType;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxCommentTypeFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabelGroupBy;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxGroupBy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabelDatabase;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxDatabases;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonOptions;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;


    }
}

