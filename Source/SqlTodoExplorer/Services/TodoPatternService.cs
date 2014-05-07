using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DamnTools.SqlTodoExplorer.Services
{
    public class TodoPatternService : ITodoPatternService
    {
        public IReadOnlyList<ITodoPattern> GetTodoPatterns()
        {
            using (var reader = new StringReader(Resources.DefaultTodoPatterns))
            {
                var serializer = new XmlSerializer(typeof(List<TodoPattern>));
                var todoPatterns = (List<TodoPattern>)serializer.Deserialize(reader);
                return todoPatterns;
            }
        }
    }
}
