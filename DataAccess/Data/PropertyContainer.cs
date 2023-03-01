using DataAccess.Models;

namespace DataAccess.Data.ShipmentHeader;

public partial class DataBaseTable<Model>
    where Model : IModel
{
    private class PropertyContainer
    {
        private readonly Dictionary<string, object> _ids;
        private readonly Dictionary<string, object> _values;

        internal PropertyContainer()
        {
            _ids = new Dictionary<string, object>();
            _values = new Dictionary<string, object>();
        }

        internal IEnumerable<string> IdNames
        {
            get { return _ids.Keys; }
        }

        internal IEnumerable<string> ValueNames
        {
            get { return _values.Keys; }
        }

        internal IEnumerable<string> AllNames
        {
            get { return _ids.Keys.Union(_values.Keys); }
        }

        internal IDictionary<string, object> IdPairs
        {
            get { return _ids; }
        }

        internal IDictionary<string, object> ValuePairs
        {
            get { return _values; }
        }

        internal IEnumerable<KeyValuePair<string, object>> AllPairs
        {
            get { return _ids.Concat(_values); }
        }

        internal void AddId(string name, object value)
        {
            _ids.Add(name, value);
        }

        internal void AddValue(string name, object value)
        {
            _values.Add(name, value);
        }
    }
}
