using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;

namespace DamnTools.SqlTodoExplorer.Services
{
    public interface IWindowsManager
    {
        Window2 ShowWindow<T>(AddIn addIn, string title, bool isFloating = true, int width = 450, int height = 450) where T : UserControl;
    }
}