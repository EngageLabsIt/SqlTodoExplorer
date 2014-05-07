using System;
using System.Data.SqlClient;

namespace DamnTools.SqlTodoExplorer.Services
{
    public static class SqlDataReaderEx
    {
        public static T Get<T>(this SqlDataReader reader, string fieldName)
        {
            int ordinal;
            try
            {
                ordinal = reader.GetOrdinal(fieldName);
            }
            catch (Exception)
            {
                throw new IndexOutOfRangeException(string.Format("Field name '{0}' not found.", fieldName));
            }
            return !reader.IsDBNull(ordinal) ? (T)reader.GetValue(ordinal) : default(T);
        }
    }
}
