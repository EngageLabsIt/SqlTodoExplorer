using System.Collections.Generic;

namespace DamnTools.SqlTodoExplorer.Services
{
    public interface IServerGateway
    {
        IReadOnlyList<string> GetDatabases();

        IReadOnlyList<Routine> GetRoutines(IEnumerable<string> databases = null);

        Routine GetRoutine(string database, int routineId);
    }
}