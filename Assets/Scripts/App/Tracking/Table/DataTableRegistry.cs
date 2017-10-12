using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.App.Tracking.Table {
    public class DataTableRegistry {

        public Dictionary<string, DataTable> Tables { get; private set; }

        public DataTableRegistry() {
            Tables = new Dictionary<string, DataTable>();
        }

        /// <summary>
        /// Registers a new datatable and creates it
        /// </summary>
        /// <param name="table"></param>
        public void Register(DataTable table) {
            Tables[table.Name.ToLower()] = table;
            table.Create();
        }

        /// <summary>
        /// Fetches the datatable with the given name, returns null otherwise
        /// </summary>
        /// <param name="name">The name of the datatable</param>
        public DataTable Fetch(string name) {
            return Tables.ContainsKey(name.ToLower()) ? Tables[name.ToLower()] : null;
        }

        /// <summary>
        /// Attempts to upload the database to the webserver, if set-up
        /// </summary>
        public void Upload() {
            //upload to webserver
        }
    }
}
