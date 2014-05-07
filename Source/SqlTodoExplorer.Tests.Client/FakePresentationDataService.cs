using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DamnTools.SqlTodoExplorer.Presentation.Model;
using DamnTools.SqlTodoExplorer.Presentation.Model.Data;
using DamnTools.SqlTodoExplorer.Services;
using RoutineType = DamnTools.SqlTodoExplorer.Presentation.Model.Data.RoutineType;

namespace DamnTools.SqlTodoExplorer.Tests.Client
{
    public class FakePresentationDataService : IPresentationDataService
    {
        public List<RoutineDataRecord> GetDataRecords(string databaseName, ITodoPattern todoPattern)
        {
            var dataRecords = new List<RoutineDataRecord>
            {
                new RoutineDataRecord
                {
                    Id = 1000,
                    Schema = "HumanResources",
                    Name = "proc_Employees_List",
                    Definition = "--TODO: change here!",
                    Tag = new TodoPattern
                                {
                                     SearchPattern = @"(?<=\W|^)(?<TAG>TODO)(.*)",
                                     Title = "TODO",
                                     Id = 1
                                },
                    CommentIndex = 1,
                    Type = RoutineType.StoredProcedure
                },
                new RoutineDataRecord
                {
                    Id = 2000,
                    Schema = "HumanResources",
                    Name = "proc_Employees_Get",
                    Definition = "--BUG: and here!",
                    Tag = new TodoPattern
                                {
                                     SearchPattern = @"(?<=\W|^)(?<TAG>BUG)(.*)",
                                     Title = "BUG",
                                     Id = 4
                                },
                    CommentIndex = 2,
                    Type = RoutineType.StoredProcedure
                },
                new RoutineDataRecord
                {
                    Id = 2000,
                    Schema = "HumanResources",
                    Name = "proc_Employees_Get",
                    Definition = "--TODO: change the where condition with a better key.",
                    Tag = new TodoPattern
                                {
                                     SearchPattern = @"(?<=\W|^)(?<TAG>TODO)(.*)",
                                     Title = "TODO",
                                     Id = 1
                                },
                    CommentIndex = 3,
                    Type = RoutineType.StoredProcedure
                },
                new RoutineDataRecord
                {
                    Id = 4000,
                    Schema = "Sales",
                    Name = "proc_Orders_Add",
                    Definition = "--HACK: this is a workaround and it should be changed in next release.",
                    Tag = new TodoPattern
                                {
                                     SearchPattern = @"(?<=\W|^)(?<TAG>HACK)(.*)",
                                     Title = "HACK",
                                     Id = 2
                                },
                    CommentIndex = 4,
                    Type = RoutineType.StoredProcedure
                },
                new RoutineDataRecord
                {
                    Id = 5000,
                    Schema = "Sales",
                    Name = "proc_OrderDetails_ListByOrderId",
                    Definition = "--TODO: missing business logic.",
                    Tag = new TodoPattern
                                {
                                     SearchPattern = @"(?<=\W|^)(?<TAG>TODO)(.*)",
                                     Title = "TODO",
                                     Id = 1
                                },
                    CommentIndex = 5,
                    Type = RoutineType.StoredProcedure
                },
                new RoutineDataRecord
                {
                    Id = 14000,
                    Schema = "HumanResources",
                    Name = "udf_Employees_Get",
                    Definition = "--TODO: Add that information.",
                    Tag = new TodoPattern
                                {
                                     SearchPattern = @"(?<=\W|^)(?<TAG>TODO)(.*)",
                                     Title = "TODO",
                                     Id = 1
                                },
                    CommentIndex = 6,
                    Type = RoutineType.Function
                },
                new RoutineDataRecord
                {
                    Id = 15000,
                    Schema = "Sales",
                    Name = "udf_Orders_Recompute",
                    Definition = "--HACK: Added a hardcoded constraint. This should be improved.",
                    Tag = new TodoPattern
                                {
                                     SearchPattern = @"(?<=\W|^)(?<TAG>HACK)(.*)",
                                     Title = "HACK",
                                     Id = 2
                                },
                    CommentIndex = 7,
                    Type = RoutineType.Function
                },
                new RoutineDataRecord
                {
                    Id = 15000,
                    Schema = "Sales",
                    Name = "udf_Orders_Recompute",
                    Definition = "--ASK: What to do.. What to do?",
                    Tag = new TodoPattern
                                {
                                     SearchPattern = @"(?<=\W|^)(?<TAG>ASK)(.*)",
                                     Title = "ASK",
                                     Id = 3
                                },
                    CommentIndex = 8,
                    Type = RoutineType.Function
                },
            };

            if (todoPattern != null)
                return dataRecords.Where(t => t.Tag.Id == todoPattern.Id).ToList();

            return dataRecords;
        }

        public List<DatabaseDataRecord> GetDatabases()
        {
            var dataRecords = new List<DatabaseDataRecord>
            {
                new DatabaseDataRecord
                {
                    Id = 1000,
                    Name = "AdventureWorks"
                },
                new DatabaseDataRecord
                {
                    Id = 2000,
                    Name = "tempDB"
                },
                new DatabaseDataRecord
                {
                    Id = 3000,
                    Name = "Northwind"
                }
            };

            return dataRecords;
        }

        public IReadOnlyList<ITodoPattern> GetPatterns()
        {
            using (var reader = new StringReader(Services.Resources.DefaultTodoPatterns))
            {
                var serializer = new XmlSerializer(typeof(List<TodoPattern>));
                var todoPatterns = (List<TodoPattern>)serializer.Deserialize(reader);
                return todoPatterns;
            }
        }

        public void RefreshCurrentConnection()
        {
            Trace.TraceInformation("Current connection refreshed");
        }

        public void NavigateTo(TodoItem todoItem)
        {
            Trace.TraceInformation("Navigate to {0}", todoItem);
        }
    }
}