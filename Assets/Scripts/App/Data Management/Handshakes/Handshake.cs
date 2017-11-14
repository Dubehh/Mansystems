using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace Assets.Scripts.App.Data_Management {
    public class Handshake {

        private const string
            _webReference = "http://localhost/",
            _webController = "upload";

        private const string _handshakeID = "requestType";
        private List<IMultipartFormSection> _params;
        private HandshakeProtocol _protocol;

        public Handshake(HandshakeProtocol protocol) {
            _protocol = protocol;
            _params = new List<IMultipartFormSection>();
            AddParameter(_handshakeID, _protocol.ToString());
        }

        public Handshake AddParameter(string key, string value) {
            _params.Add(new MultipartFormDataSection(key, value));
            return this;
        }

        public void Shake(Action<UnityWebRequest> complete = null) {
            var handshake = UnityWebRequest.Post(_webReference + _webController, _params);
            var request = handshake.SendWebRequest();
            if (complete == null) return;
            request.completed += (action) => {
                complete.Invoke(handshake);
            };
        }

        
    }
}
