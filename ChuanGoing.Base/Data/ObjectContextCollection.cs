using System.Collections.Concurrent;

namespace ChuanGoing.Base.Data
{
    public class ObjectContextCollection : ConcurrentDictionary<string, ObjectContext>
    {
        public ObjectContextCollection()
        {

        }
    }
}
