using System;
using System.Drawing;
using System.Windows.Forms;
using DamnTools.SqlTodoExplorer.Presentation;

namespace DamnTools.SqlTodoExplorer.Tests.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var hostView = new Form();

            var view = new SqlTodoExplorerView();

            var presenter = new SqlTodoExplorerPresenter(view, new FakePresentationDataService());
            presenter.Init();

            hostView.SuspendLayout();
            // 
            // sqlTodoExplorerView1
            // 
            view.Dock = DockStyle.Fill;
            view.Location = new Point(0, 0);
            view.Name = "SqlTodoExplorerView";
            view.Size = new Size(818, 272);
            view.TabIndex = 0;
            // 
            // HostView
            // 
            hostView.AutoScaleDimensions = new SizeF(6F, 13F);
            hostView.AutoScaleMode = AutoScaleMode.Font;
            hostView.ClientSize = new Size(818, 272);
            hostView.Controls.Add(view);
            hostView.Name = "HostView";
            hostView.Text = "SQL Todo Explorer by DamnTools";
            hostView.ResumeLayout(false);

            Application.Run(hostView);
        }
    }
}
