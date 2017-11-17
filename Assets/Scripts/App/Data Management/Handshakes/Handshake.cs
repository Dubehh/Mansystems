﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace Assets.Scripts.App.Data_Management {
    public class Handshake {

        private const string
            _webReference = "http://localhost/app/",
            _webController = "handshake";

        private const string _handshakeID = "streamType";
        private List<IMultipartFormSection> _params;
        private HandshakeProtocol _protocol;

        /// <summary>
        /// Instantiates a new Handshake that may be used to communicate with the defined webhost
        /// </summary>
        /// <param name="protocol">The type of the handshake</param>
        public Handshake(HandshakeProtocol protocol) {
            _protocol = protocol;
            _params = new List<IMultipartFormSection>();
            AddParameter(_handshakeID, _protocol.ToString());
        }

        /// <summary>
        /// Adds a data parameter to the handshake
        /// </summary>
        /// <param name="key">string key</param>
        /// <param name="value">string value</param>
        /// <returns>The handshake instance; builder pattern principle</returns>
        public Handshake AddParameter(string key, string value) {
            _params.Add(new MultipartFormDataSection(key, value));
            return this;
        }

        /// <summary>
        /// Attempts to send a async handshake to the defined webaddress.
        /// Uses a callback to respond with the result of the request
        /// </summary>
        /// <param name="complete">Callback that fires upon completion of the request</param>
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