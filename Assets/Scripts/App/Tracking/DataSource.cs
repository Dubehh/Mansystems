using System.Collections;
using System.Collections.Generic;
using Mono.Data.SqliteClient;
using System.Data;
using UnityEngine;

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

    public IDbConnection GetConnection() {
        return _connection;
    }

    public IDbCommand GetCommand() {
        return _command;
    }

    public static DataSource GetInstance() {
        if (_instance == null)
            _instance = new DataSource();
        return _instance;
    }


	
}
