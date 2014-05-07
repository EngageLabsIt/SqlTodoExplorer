namespace DamnTools.SqlTodoExplorer.Services
{
    public class ServerConnectionInfo
    {
        public string ServerName { get; set; }
        public bool UseIntegratedSecurity { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("[ServerName:{0}],[UseIntegratedSecurity:{1}],[UserName:{2}],[Password:{3}]", this.ServerName, this.UseIntegratedSecurity, this.UserName, this.Password);
        }
    }
}
