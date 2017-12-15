using System.Collections.Generic;

namespace Assets.Scripts.App.Tracking.Table {
    public class DataTableRegistry {
        public DataTableRegistry() {
            Tables = new Dictionary<string, DataTable>();
        }

        public Dictionary<string, DataTable> Tables { get; private set; }

        /// <summary>
        ///     Registers a new datatable and creates it
        /// </summary>
        /// <param name="table"></param>
        public void Register(DataTable table, bool create = true) {
            Tables[table.Name.ToLower()] = table;
            if (create)
                table.Create();
        }

        /// <summary>
        ///     Fetches the datatable with the given name, returns null otherwise
        /// </summary>
        /// <param name="name">The name of the datatable</param>
        public DataTable Fetch(string name) {
            if (Tables.ContainsKey(name.ToLower())) {
                return Tables[name.ToLower()];
            }
            var table = new DataTable(name);
            if (table.Exists()) {
                Register(table, false);
                return table;
            }
            return null;
        }
    }
}