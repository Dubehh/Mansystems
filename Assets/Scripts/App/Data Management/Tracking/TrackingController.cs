using Assets.Scripts.App.Data_Management;
using Assets.Scripts.App.Game;
using Assets.Scripts.App.Tracking.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.App.Data_Management.Handshakes;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.App.Tracking {
    public class TrackingController {

        private DataTable _source;

        public TrackingController(DataTable source) {
            _source = source;
        }

        /// <summary>
        /// Attempts to send the tracking update request
        /// Performs the instantiated  request using a HTTP POST request to serialize the data.
        /// </summary>
        public void RequestSend() {
            var handshake = new InformationProtocol(Protocol.Update)
                .AddParameter("creation_query", _source.GenerateBuildQuery())
                .AddParameter("table_name", _source.Name)
                .AddParameter("player_name", PlayerPrefs.GetString("name"))
                .AddParameter("player_uuid", PlayerPrefs.GetString("uid"));
            _source.Select("*", "", reader => {
                while (reader.Read()) {
                    _source.Properties.ForEach(property => {
                        handshake.AddParameter(property.Name + "[]", reader[property.Name].ToString());
                    });
                }
                handshake.Send((callback) => { _source.Drop(); });
            });
        }
    }
}
