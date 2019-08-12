using System.Collections.Generic;

namespace ChuanGoing.Storage.Dapper.Commands
{
    public class QueryParameter
    {
        private List<Filter> _filters;
        private List<Sort> _sorts;

        public IEnumerable<Field> Fields { get; }
        public IEnumerable<Filter> Filters => _filters;
        public IEnumerable<Sort> Sorts => _sorts;

        public QueryParameter(FieldsCollection fields, IEnumerable<Filter> filters = null, IEnumerable<Sort> sorts = null)
        {
            Fields = fields.GetFields();
            _filters = new List<Filter>();
            if (filters != null)
            {
                _filters.AddRange(filters);
            }
            _sorts = new List<Sort>();
            if (sorts != null)
            {
                _sorts.AddRange(sorts);
            }
        }

        public void AddFilter(Filter filter)
        {
            _filters.Add(filter);
        }

        public void AddSort(Sort sort)
        {
            _sorts.Add(sort);
        }
    }
}
