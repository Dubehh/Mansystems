using Assets.Scripts.App.Tracking.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.App.Game.Tracking {
    public class TrackingUpdateParams {

        private List<KeyValuePair<string, string>> _params;

        public TrackingUpdateParams() {
            _params = new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// Appends a key and a value to the parameters
        /// </summary>
        /// <param name="key">string</param>
        /// <param name="value">string</param>
        public void Append(string key, string value) {
            _params.Add(new KeyValuePair<string, string>(key, value));
        }

        /// <summary>
        /// Parses the key/value collection to a list with IMultipartFormSection instances
        /// </summary>
        /// <returns>List collection</returns>
        public List<IMultipartFormSection> Parse() {
            var rtn = new List<IMultipartFormSection>();
            foreach (var pair in _params) 
                rtn.Add(new MultipartFormDataSection(pair.Key, pair.Value));
            return rtn;
        }
    }
}
