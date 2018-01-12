using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine;

namespace Assets.Scripts.App.Data_Management {
    /// <summary>
    ///     Class that manages the raw connection to the SQLite database.
    ///     This class is singleton and therefore accessible from anywhere.
    /// </summary>
    public class DataSource {
        private const string _db = "TrackingMaster";

        private static DataSource _instance;
        private readonly IDbCommand _command;
        private readonly IDbConnection _connection;

        private DataSource() {
            _connection = new SqliteConnection("URI=file:" + Application.persistentDataPath + "/" + _db + ".db");
            _connection.Open();
            _command = _connection.CreateCommand();
        }

        /// <summary>
        ///     Returns the connection instance
        /// </summary>
        public IDbConnection GetConnection() {
            return _connection;
        }

        /// <summary>
        ///     Returns the command interface
        /// </summary>
        public IDbCommand GetCommand() {
            return _command;
        }

        /// <summary>
        ///     Returns the singleton instance
        /// </summary>
        /// <returns>DataSource instance</returns>
        public static DataSource GetInstance() {
            if (_instance == null)
                _instance = new DataSource();
            return _instance;
        }
    }
}