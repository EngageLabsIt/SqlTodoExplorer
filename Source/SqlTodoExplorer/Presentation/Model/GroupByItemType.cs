namespace DamnTools.SqlTodoExplorer.Presentation.Model
{
    public class GroupByItem
    {
        public GroupByItemType Id { get; set; }
        public string Description { get; set; }
    }

    public enum GroupByItemType
    {
        Unknown = 0,
        ObjectType = 1,
        CommentType = 2,
        Flat = 3
    }
}
