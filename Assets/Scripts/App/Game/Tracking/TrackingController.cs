using Assets.Scripts.App.Game;
using Assets.Scripts.App.Game.Tracking;
using Assets.Scripts.App.Tracking.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.App.Tracking {
    public class TrackingController {

        private const string
            _webReference = "http://localhost/",
            _webController = "upload";
        private MonoBehaviour _behaviour;
        private DataTable _source;

        public TrackingController(MonoBehaviour behaviour, DataTable source) {
            _behaviour = behaviour;
            _source = source;
        }
        
        /// <summary>
        /// Attempts to send the tracking update request
        /// </summary>
        public void RequestSend() {
            _behaviour.StartCoroutine(RequestPerform());
        }

        /// <summary>
        /// Performs the instantiated  request inside a coroutine.
        /// Uses a HTTP POST request to serialize the data.
        /// </summary>
        /// <returns>An IEnumerator instance</returns>
        private IEnumerator RequestPerform() {
            var parameters = new TrackingUpdateParams();
            parameters.Append("creation_query", _source.GenerateBuildQuery());
            parameters.Append("table_name", _source.Name);
            parameters.Append("player_name", PlayerPrefs.GetString("name"));
            parameters.Append("player_uuid", PlayerPrefs.GetString("uid"));

            _source.Select("*", "", reader => {
                while (reader.Read()) {
                    _source.Properties.ForEach(property => {
                        parameters.Append(property.Name+"[]", reader[property.Name].ToString());
                    });
                }
            });
            
            var request = UnityWebRequest.Post(_webReference + "/" + _webController, parameters.Parse());
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) yield break;

            Debug.Log(request.downloadHandler.text);
        }
    }
}
