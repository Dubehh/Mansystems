using System.Collections.Generic;

namespace Assets.Scripts.App.Tracking.Table {
    public class DataParams {
        private DataParams() {
            Parameters = new List<KeyValuePair<string, object>>();
        }

        public List<KeyValuePair<string, object>> Parameters { get; private set; }

        /// <summary>
        ///     Uses the builder pattern to create a DataParams instance
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataParams Build(string key, object value) {
            return new DataParams().Append(key, value);
        }

        /// <summary>
        ///     Uses the builder pattern to create a DataParams instance
        /// </summary>
        /// <returns>Dataparams instance</returns>
        public static DataParams Build() {
            return new DataParams();
        }

        /// <summary>
        ///     Adds a new key/value pair to the parameters
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