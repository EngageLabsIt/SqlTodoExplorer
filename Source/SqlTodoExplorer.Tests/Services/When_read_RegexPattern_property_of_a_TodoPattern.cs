using DamnTools.SqlTodoExplorer.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DamnTools.SqlTodoExplorer.Tests.Services
{
    [TestClass]
    public class When_read_RegexPattern_property_of_a_TodoPattern
    {
        [TestMethod]
        public void Should_return_null_if_SearchPattern_is_null()
        {
            var todoPattern = new TodoPattern
            {
                SearchPattern = null
            };

            var regex = todoPattern.PatternRegex;

            regex.Should().BeNull();
        }

        [TestMethod]
        public void Should_return_null_if_SearchPattern_is_empty()
        {
            var todoPattern = new TodoPattern
            {
                SearchPattern = string.Empty
            };

            var regex = todoPattern.PatternRegex;

            regex.Should().BeNull();
        }

        [TestMethod]
        public void Should_return_the_regex_of_a_valid_SearchPattern()
        {
            var searchPattern = @"Valid_SearchPattern\d+";
            var todoPattern = new TodoPattern
            {
                SearchPattern = searchPattern
            };

            var regex = todoPattern.PatternRegex;

            regex.Should().NotBeNull();
            regex.ToString().Should().Be(searchPattern);
        }

        [TestMethod]
        public void Should_return_the_same_regex_reference_when_reading_twice()
        {
            var searchPattern = @"Valid_SearchPattern\d+";
            var todoPattern = new TodoPattern
            {
                SearchPattern = searchPattern
            };

            var firstRead = todoPattern.PatternRegex;
            var secondRead = todoPattern.PatternRegex;

            firstRead.Should().BeSameAs(secondRead);
        }

        [TestMethod]
        public void Should_return_the_updated_regex_when_changing_the_SearchPattern()
        {
            var todoPattern = new TodoPattern();

            var originalSearchPattern = @"Original_SearchPattern\d+";
            todoPattern.SearchPattern = originalSearchPattern;
            var originalRegex = todoPattern.PatternRegex;

            // change the search pattern
            var newSearchPattern = @"Changed_SearchPattern\d+";
            todoPattern.SearchPattern = newSearchPattern;
            var newRegex = todoPattern.PatternRegex;
            
            originalRegex.Should().NotBeNull();
            originalRegex.ToString().Should().Be(originalSearchPattern);

            newRegex.Should().NotBeNull();
            newRegex.ToString().Should().Be(newSearchPattern);

            originalRegex.Should().NotBeSameAs(newRegex);
        }
    }
}
