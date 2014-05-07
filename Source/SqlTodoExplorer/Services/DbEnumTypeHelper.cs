using System;

namespace DamnTools.SqlTodoExplorer.Services
{
    public static class DbEnumTypeHelper
    {
        public static RoutineType GetRoutineType(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (value.Equals("procedure", StringComparison.InvariantCultureIgnoreCase))
            {
                return RoutineType.StoredProcedure;
            }
            if (value.Equals("function", StringComparison.InvariantCultureIgnoreCase))
            {
                return RoutineType.Function;
            }
            throw new Exception(string.Format("The value '{0}' is not a valid RoutineType.", value));
        }
    }
}