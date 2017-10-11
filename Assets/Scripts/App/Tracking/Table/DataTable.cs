using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Assets.Scripts.App.Tracking.Table {
    public class DataTable {

        public string Name { get; private set; }
        public List<DataProperty> Properties { get; private set; }
        
        public DataTable(string name) {
            Properties = new List<DataProperty>();
            Name = name;
        }

        /// <summary>
        /// Adds a property to the table.
        /// This method should be called before the creation of the table
        /// </summary>
        /// <param name="property"></param>
        public void AddProperty(DataProperty property) {
            Properties.Add(property);
        }

        /// <summary>
        /// Attempts to create the datatable as a physical table inside the database
        /// </summary>
        public void Create() {
            if (Properties.Count == 0) return;
            var builder = new StringBuilder();
            Properties.ForEach(property => {
                var name = property.Name;
                var type = Enum.GetName(typeof(DataProperty.DataPropertyType), property.Type);
                var size = property.Size != null ? "(" + property.Size.Value + ")" : "";
                builder.Append(",").Append(name + " "+type+size);
            });
            var query = builder.ToString().Substring(1);
            DataQuery.Query("CREATE TABLE IF NOT EXISTS " + Name + " (" + query + ")").Update();
        }

        /// <summary>
        /// Attempts to select data from the datatable based on the given clause
        /// </summary>
        /// <param name="select">The fields you want to select</param>
        /// <param name="clause">The conditions clause</param>
        /// <param name="callback">Callback that will have access to the returned data</param>
        public void Select(string select, string clause, Action<IDataReader> callback) {
            DataQuery.Query("SELECT "+select+" FROM " + Name + " " + clause).
                Read(callback);
        }

        /// <summary>
        /// Attempts to update the datatable with the given clause
        /// </summary>
        /// <param name="clause">The conditions clause</param>
        /// <param name="callback">Optional callback when the query is complete</param>
        public void Update(DataParams parameters, string clause, Action callback = null) {
            StringBuilder builder = new StringBuilder();
            parameters.Parameters.ForEach(pair => {
                builder.Append(",")
                    .Append(pair.Key + " = ")
                    .Append(pair.Value is string ? "'" + pair.Value + "'" : pair.Value.ToString());
            });
            DataQuery.Query("UPDATE " + Name + " SET " + builder.ToString().Substring(1) + " " + clause).
                Update(callback);
        }

        /// <summary>
        /// Attempts to delete data from the datatable
        /// </summary>
        /// <param name="clause">The conditions clause</param>
        /// <param name="callback">Optional callback when the query is complete</param>
        public void Delete(string clause, Action callback = null) {
            DataQuery.Query("DELETE FROM " + Name + " " + clause).
                Update(callback);
        }
        
        /// <summary>
        /// Attempts to insert data into the datatable
        /// </summary>
        /// <param name="parameters">The data parameters</param>
        /// <param name="callback">Optional callback when they query is complete</param>
        public void Insert(DataParams parameters, Action callback=null) {
            var fields = new StringBuilder();
            var data = new StringBuilder();
            parameters.Parameters.ForEach(pair => {
                fields.Append(",").Append(pair.Key);
                data.Append(",").Append(pair.Value is string ? "'"+pair.Value+"'" : pair.Value.ToString());
            });
            DataQuery.Query("INSERT INTO " + Name +
                " (" + fields.ToString().Substring(1) + ") VALUES" +
                " (" + data.ToString().Substring(1) + ")")
                .Update(callback);
        }

        /// <summary>
        /// Drops the table (deletes it!)
        /// </summary>
        /// <param name="callback">Optional callback when the query is complete</param>
        public void Drop(Action callback = null) {
            DataQuery.Query("DROP TABLE " + Name).Update(callback);
        }

    }
}
