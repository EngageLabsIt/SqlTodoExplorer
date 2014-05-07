using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DamnTools.SqlTodoExplorer.Services
{
    public class SmoServerGateway : IServerGateway
    {
        private readonly ServerConnection _serverConnection;
        private readonly Server _server;

        public SmoServerGateway(ServerConnectionInfo info)
        {
            _serverConnection = info.UseIntegratedSecurity
                                    ? new ServerConnection(info.ServerName)
                                    : new ServerConnection(info.ServerName, info.UserName, info.Password);

            _server = new Server(_serverConnection);
        }

        public IReadOnlyList<string> GetDatabases()
        {
            _server.Databases.Refresh();
            var databases = _server.Databases.Cast<Database>().Select(t => t.Name).ToList();
            return databases;
        }

        public IReadOnlyList<Routine> GetRoutines(IEnumerable<string> databases = null)
        {
            if (databases == null) databases = GetDatabases();
            var routines = new List<Routine>();
            foreach (var database in databases)
            {
                try
                {
                    var sqlRoutinesCommandText = string.Format(Resources.SQL_Routines, database);
                    using (var reader = _serverConnection.ExecuteReader(sqlRoutinesCommandText))
                    {
                        routines.AddRange(GetRoutinesFromReader(reader, database));
                    }
                }
                catch (ExecutionFailureException e)
                {
                    Trace.TraceError(e.ToString());
                }
            }
            return routines;
        }

        public Routine GetRoutine(string database, int routineId)
        {
            if (string.IsNullOrWhiteSpace(database)) throw new ArgumentNullException("database");

            var sqlRoutinesCommandText = string.Format(Resources.SQL_Routine, database, routineId);
            using (var reader = _serverConnection.ExecuteReader(sqlRoutinesCommandText))
            {
                return GetRoutinesFromReader(reader, database).FirstOrDefault();
            }
        }

        public List<Routine> GetRoutinesFromReader(SqlDataReader reader, string database)
        {
            var records = reader.Cast<IDataRecord>()
                                .Select(t => new
                                {
                                    Id = reader.Get<int?>("ROUTINE_OBJECTID"),
                                    Schema = reader.Get<string>("ROUTINE_SCHEMA"),
                                    Name = reader.Get<string>("ROUTINE_NAME"),
                                    FullName = reader.Get<string>("ROUTINE_FULLNAME"),
                                    Type = DbEnumTypeHelper.GetRoutineType(reader.Get<string>("ROUTINE_TYPE")),
                                    Definition = reader.Get<string>("ROUTINE_DEFINITION")
                                });

            var routines = (from record in records
                            group record by record.Id
                            into groupedRoutines
                            let routineData = groupedRoutines.First()
                            let hasRoutineDefinition = groupedRoutines.Any(t => t.Definition != null)
                            select new Routine
                            {
                                Id = routineData.Id,
                                Database = database,
                                Schema = routineData.Schema,
                                Name = routineData.Name,
                                FullName = routineData.FullName,
                                Type = routineData.Type,
                                Definition = hasRoutineDefinition ? string.Concat(groupedRoutines.Where(t => t.Definition != null).SelectMany(t => t.Definition)) : null
                            }).ToList();

            return routines;
        }
    }
}