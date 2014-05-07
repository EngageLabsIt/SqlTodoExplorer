using System.Windows.Forms;

namespace DamnTools.SqlTodoExplorer.Presentation.Helpers
{
    public class ToolStripRender : ToolStripSystemRenderer
    {
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // prevents the defaul render
            //base.OnRenderToolStripBorder(e);
        }
    }
}
