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

        public void Append(string key, string value) {
            _params.Add(new KeyValuePair<string, string>(key, value));
        }

        public List<IMultipartFormSection> Parse() {
            var rtn = new List<IMultipartFormSection>();
            foreach (var pair in _params) 
                rtn.Add(new MultipartFormDataSection(pair.Key, pair.Value));
            return rtn;
        }
    }
}
