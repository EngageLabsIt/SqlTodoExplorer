using System.Collections.Generic;

namespace DamnTools.SqlTodoExplorer.Services
{
    public interface ITodoPatternService
    {
        IReadOnlyList<ITodoPattern> GetTodoPatterns();
    }
}