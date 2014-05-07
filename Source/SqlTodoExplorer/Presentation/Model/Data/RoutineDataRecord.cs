using System;
using DamnTools.SqlTodoExplorer.Services;

namespace DamnTools.SqlTodoExplorer.Presentation.Model.Data
{
    public class RoutineDataRecord
    {
        public int Id { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public RoutineType Type { get; set; }
        public ITodoPattern Tag { get; set; }
        public string Definition { get; set; }
        public int CommentIndex { get; set; }
        public TodoItem TodoItem { get; set; }
        public string FullName
        {
            get
            {
                return String.Format("{0}.{1}", Schema, Name);
            }
        }
    }
}
