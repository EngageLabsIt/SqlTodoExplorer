using DamnTools.SqlTodoExplorer.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DamnTools.SqlTodoExplorer.Tests.Services
{
    [TestClass]
    public class TodoPatternServiceTests
    {
        [TestMethod]
        public void Should_be_able_to_get_the_list_of_todo_patterns()
        {
            var service = new TodoPatternService();

            var todoPatterns = service.GetTodoPatterns();

            todoPatterns.Should().NotBeNull().And.NotBeEmpty();
            todoPatterns.Should().Contain(t => t.Title == "Todo" && t.Id == 1);
        }
    }
}
