using System;
using DamnTools.SqlTodoExplorer.Presentation;
using DamnTools.SqlTodoExplorer.Presentation.Model;
using DamnTools.SqlTodoExplorer.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DamnTools.SqlTodoExplorer.Tests.Presentation
{
    [TestClass]
    public class When_navigate_todo_item
    {
        [TestMethod]
        public void Should_script_the_object_as_alter()
        {
            var mockView = new Mock<ISqlTodoExplorerView>();
            var mockDataService = new Mock<IPresentationDataService>();
            var presenter = new SqlTodoExplorerPresenter(mockView.Object, mockDataService.Object);
            var todoItem = new TodoItem();
            var metaData = new TreeNodeMetaData
            {
                TodoItem = todoItem
            };

            mockView.Raise(x => x.NodeDoubleClicked += null, metaData);

            mockDataService.Verify(x => x.NavigateTo(It.Is<TodoItem>(t => t.Equals(todoItem))), Times.Once);
        }

        [TestMethod]
        public void Passing_null_as_todo_item_Should_return_without_errors()
        {
            var mockView = new Mock<ISqlTodoExplorerView>();
            var mockDataService = new Mock<IPresentationDataService>();
            var presenter = new SqlTodoExplorerPresenter(mockView.Object, mockDataService.Object);
            var metaData = new TreeNodeMetaData
            {
                TodoItem = null
            };

            mockView.Raise(x => x.NodeDoubleClicked += null, metaData);

            mockDataService.Verify(x => x.NavigateTo(It.IsAny<TodoItem>()), Times.Never);
        }

        [TestMethod]
        public void Passing_null_as_argument_Should_return_without_errors()
        {
            var mockView = new Mock<ISqlTodoExplorerView>();
            var mockDataService = new Mock<IPresentationDataService>();
            var presenter = new SqlTodoExplorerPresenter(mockView.Object, mockDataService.Object);
            TreeNodeMetaData metaData = null;

            mockView.Raise(x => x.NodeDoubleClicked += null, metaData);

            mockDataService.Verify(x => x.NavigateTo(It.IsAny<TodoItem>()), Times.Never);
        }
    }
}
