namespace DamnTools.SqlTodoExplorer.Presentation.Model
{
    public enum NodeType
    {
        Unknown = 0,
        RootStoredProcedure = 1,
        RootFunction = 2,
        Schema = 3,
        StoredProcedure = 4,
        Function = 5,
        Comment = 6
    }
}
