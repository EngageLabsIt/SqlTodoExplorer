using DamnTools.SqlTodoExplorer.Services;

namespace DamnTools.SqlTodoExplorer.Presentation.Model
{
    public class FilterItem
    {
        public ITodoPattern Id { get; set; }
        public string Description { get; set; }
    }
}
