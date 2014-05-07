namespace DamnTools.SqlTodoExplorer.Services
{
    public interface IServerConnectionService
    {
        ServerConnectionInfo GetCurrentConnection();
    }
}