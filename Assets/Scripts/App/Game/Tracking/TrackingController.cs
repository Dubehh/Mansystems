using Assets.Scripts.App.Game;
using Assets.Scripts.App.Game.Tracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.App.Tracking {
    public class TrackingController {

        private const string
            _webReference = "http://localhost/",
            _webController = "upload";
        private GameController _controller;

        public TrackingController(GameController controller) {
            _controller = controller;
        }
        
        public void RequestSend() {
            _controller.StartCoroutine(RequestPerform());
        }

        private IEnumerator RequestPerform() {
            var parameters = new TrackingUpdateParams();
            parameters.Append("creation_query", _controller.DataSource.GenerateBuildQuery());
            parameters.Append("player_id", "Erik");

            _controller.DataSource.Select("*", "", reader => {
                while (reader.Read()) {
                    _controller.DataSource.Properties.ForEach(property => {
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
