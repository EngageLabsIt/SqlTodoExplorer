namespace DamnTools.SqlTodoExplorer.Services
{
    public class TodoItem
    {
        public string Tag { get; set; }
        public string Text { get; set; }
        public int TextIndex { get; set; }
        public int TextLength { get; set; }
        public int TextOccurrence { get; set; }
        public ITodoPattern Pattern { get; set; }
        public string Database { get; set; }
        public int RoutineId { get; set; }
        public string RoutineSchema { get; set; }
        public string RoutineName { get; set; }
        public RoutineType RoutineType { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.RoutineId, this.Text);
        }
    }
}