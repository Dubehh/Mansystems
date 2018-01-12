using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Data_Management.Table;
using UnityEngine;

namespace Assets.Scripts.App.Data_Management.Tracking {
    public class TrackingController {
        private readonly DataTable _source;

        public TrackingController(DataTable source) {
            _source = source;
        }

        /// <summary>
        ///     Attempts to send the tracking update request
        ///     Performs the instantiated  request using a HTTP POST request to serialize the data.
        /// </summary>
        public void RequestSend() {
            var handshake = new InformationProtocol(Protocol.Tracking)
                .AddParameter("creation_query", _source.GenerateBuildQuery())
                .AddParameter("table_name", _source.Name)
                .AddParameter("player_name", PlayerPrefs.GetString("name"))
                .AddParameter("player_uuid", PlayerPrefs.GetString("uid"));
            _source.Select("*", "", reader => {
                while (reader.Read())
                    _source.Properties.ForEach(property => {
                        handshake.AddParameter(property.Name + "[]", reader[property.Name].ToString());
                    });
                handshake.Send(callback => { _source.Drop(); });
            });
        }
    }
}