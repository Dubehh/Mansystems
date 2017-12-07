using System.Collections;
using System.Collections.Generic;
using Mono.Data.SqliteClient;
using System.Data;
using UnityEngine;

/// <summary>
/// Class that manages the raw connection to the SQLite database.
/// This class is singleton and therefore accessible from anywhere.
/// </summary>
public class DataSource {

    private static DataSource _instance;
    private const string
        _db = "TrackingMaster",
        _construct = "URI=file:"+_db+".db";

    private IDbConnection _connection;
    private IDbCommand _command;

    private DataSource() {
        _connection = new SqliteConnection(_construct);
        _connection.Open();
        _command = _connection.CreateCommand();
    }

    /// <summary>
    /// Returns the connection instance
    /// </summary>
    public IDbConnection GetConnection() {
        return _connection;
    }

    /// <summary>
    /// Returns the command interface
    /// </summary>
    public IDbCommand GetCommand() {
        return _command;
    }

    /// <summary>
    /// Returns the singleton instance
    /// </summary>
    /// <returns>DataSource instance</returns>
    public static DataSource GetInstance() {
        if (_instance == null)
            _instance = new DataSource();
        return _instance;
    }


	
}
