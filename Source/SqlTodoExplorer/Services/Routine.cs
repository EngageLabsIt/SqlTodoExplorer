namespace DamnTools.SqlTodoExplorer.Services
{
    public class Routine
    {
        public int? Id { get; set; }
        public string Database { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public RoutineType Type { get; set; }
        public string Definition { get; set; }

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Database, this.FullName);
        }
    }
}