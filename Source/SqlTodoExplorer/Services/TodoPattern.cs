using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace DamnTools.SqlTodoExplorer.Services
{
    public class TodoPattern : ITodoPattern
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string SearchPattern { get; set; }

        private volatile Regex _patternRegex;
        private readonly object _patternRegexSync = new object();

        [XmlIgnore]
        public Regex PatternRegex
        {
            get
            {
                if (string.IsNullOrEmpty(this.SearchPattern)) return null;
                if (_patternRegex == null)
                {
                    lock (_patternRegexSync)
                    {
                        if (_patternRegex == null)
                        {
                            _patternRegex = new Regex(this.SearchPattern, RegexOptions.IgnoreCase);
                        }
                    }
                }
                else
                {
                    if (_patternRegex.ToString() != this.SearchPattern)
                    {
                        lock (_patternRegexSync)
                        {
                            if (_patternRegex == null || _patternRegex.ToString() != this.SearchPattern)
                            {
                                _patternRegex = new Regex(this.SearchPattern, RegexOptions.IgnoreCase);
                            }
                        }
                    }
                }
                return _patternRegex;
            }
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}