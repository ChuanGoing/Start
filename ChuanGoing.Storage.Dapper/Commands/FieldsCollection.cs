using System.Collections;
using System.Collections.Generic;

namespace ChuanGoing.Storage.Dapper.Commands
{
    public class FieldsCollection : IEnumerable<Field>
    {
        private List<Field> _fields;
        public Field this[int index] => _fields[index];

        public FieldsCollection()
        {
            _fields = new List<Field>();
        }

        public int Count => _fields.Count;

        public void Add(Field field)
        {
            _fields.Add(field);
        }

        public void Add(IEnumerable<Field> fields)
        {
            _fields.AddRange(fields);
        }

        public IEnumerable<Field> GetFields()
        {
            return _fields;
        }

        public IEnumerator<Field> GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _fields.GetEnumerator();
        }
    }
}
