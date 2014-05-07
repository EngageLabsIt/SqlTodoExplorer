using System;
using System.Reflection;
using System.Windows.Forms;
using DamnTools.SqlTodoExplorer.Presentation;
using DamnTools.SqlTodoExplorer.Presentation.Model;
using EnvDTE;
using EnvDTE80;

namespace DamnTools.SqlTodoExplorer.Services
{
    public class WindowsManager : IWindowsManager
    {
        private readonly _DTE _dte;

        public WindowsManager(_DTE dte)
        {
            _dte = dte;
        }

        public Window2 ShowWindow<T>(AddIn addIn, string title, bool isFloating = true, int width = 450, int height = 450)
            where T : UserControl
        {
            Windows2 toolWindows = _dte.Windows as Windows2;
            if (toolWindows != null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var location = assembly.Location;
                var type = typeof(T);
                object controlObject = null;

                var toolWindow = (Window2)toolWindows.CreateToolWindow2(addIn, location, type.FullName, title, Guid.NewGuid().ToString(), ref controlObject);

                var dataService = new PresentationDataService();
                var presenter = new SqlTodoExplorerPresenter((ISqlTodoExplorerView)controlObject, dataService);
                presenter.Init();

                toolWindow.WindowState = vsWindowState.vsWindowStateNormal;
                toolWindow.IsFloating = isFloating;
                toolWindow.Width = width;
                toolWindow.Height = height;
                toolWindow.Visible = true;
                
                return toolWindow;
            }
            return null;
        }

        // TODO: should we implement a Load and Save method to store the state of the window, or is up to the visual studio IDE to do this?
    }
}