using System;
using System.Data;

namespace Assets.Scripts.App.Tracking {
    public class DataQuery {
        private readonly IDbCommand _command;

        private DataQuery(string query) {
            _command = DataSource.GetInstance().GetCommand();
            _command.CommandText = query;
        }

        /// <summary>
        ///     Creates an instance used to query to the datasource.
        ///     The <see cref="DataSource" /> contains the connection interface.
        /// </summary>
        public static DataQuery Query(string query) {
            return new DataQuery(query);
        }

        /// <summary>
        ///     Attempts to read the result from the query
        /// </summary>
        /// <param name="callback">Used to react upon the result</param>
        public void Read(Action<IDataReader> callback) {
            var reader = _command.ExecuteReader();
            callback(reader);
        }

        /// <summary>
        ///     Fire the given query to create an update call.
        /// </summary>
        /// <param name="callback">Used to react on a succesful query, may be null</param>
        public void Update(Action callback = null) {
            _command.ExecuteNonQuery();
            if (callback != null)
                callback();
        }
    }
}