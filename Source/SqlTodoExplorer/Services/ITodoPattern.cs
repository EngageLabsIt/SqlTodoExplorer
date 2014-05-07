namespace DamnTools.SqlTodoExplorer.Services
{
    public interface ITodoPattern
    {
        int Id { get; }
        string Title { get; }
        string SearchPattern { get; }
    }
}