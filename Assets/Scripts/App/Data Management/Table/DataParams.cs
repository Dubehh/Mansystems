using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.App.Tracking.Table {
    public class DataParams {

        public List<KeyValuePair<string, object>> Parameters { get; private set; }

        private DataParams() {
            Parameters = new List<KeyValuePair<string, object>>();
        }

        /// <summary>
        /// Uses the builder pattern to create a DataParams instance
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataParams Build(string key, object value) {
            return new DataParams().Append(key, value);
        }

        /// <summary>
        /// Adds a new key/value pair to the parameters
        /// </summary>
        /// <param name="key">the name of the key</param>
        /// <param name="value">the object value</param>
        /// <returns>The dataparams instance</returns>
        public DataParams Append(string key, object value) {
            Parameters.Add(new KeyValuePair<string, object>(key, value));
            return this;
        }

    }
}
