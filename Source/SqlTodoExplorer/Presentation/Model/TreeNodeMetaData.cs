using DamnTools.SqlTodoExplorer.Services;

namespace DamnTools.SqlTodoExplorer.Presentation.Model
{
    public class TreeNodeMetaData
    {
        public NodeType NodeType { get; set; }

        public TodoItem TodoItem { get; set; }
    }
}
