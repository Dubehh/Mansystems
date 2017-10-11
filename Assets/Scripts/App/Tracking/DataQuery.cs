using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Assets.Scripts.App.Tracking {
    public class DataQuery {

        private IDbCommand _command;
        public DataQuery(string query) {
            _command = DataSource.GetInstance().GetCommand();
            _command.CommandText = query;
        }

        public void Read(Action<IDataReader> callback) {
            var reader = _command.ExecuteReader();
            callback(reader);
        }

        public void Update(Action callback) {
            _command.ExecuteNonQuery();
            callback();
        }
    }
}
