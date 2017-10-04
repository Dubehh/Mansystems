using System;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Data {
    public class DataStorage {

        private DataController _controller;
        private DatabaseReference _database;
        private FirebaseApp _app;
        private const string
            _instance = "manny-serious-gaming",
            _ref = "firebaseio";

        public DataStorage(DataController controller) {
            _controller = controller;
            _app = FirebaseApp.DefaultInstance;
            _app.SetEditorDatabaseUrl("https://" + _instance + "." + _ref + ".com/");
            if (_app.Options.DatabaseUrl != null)
                _app.SetEditorDatabaseUrl(_app.Options.DatabaseUrl);
            _database = FirebaseDatabase.DefaultInstance.RootReference;
        }
    }
}
