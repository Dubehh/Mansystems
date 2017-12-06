﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace Assets.Scripts.App.Data_Management.Handshakes {

    public class InformationProtocol : HandshakeProtocol<UnityWebRequest>{

        public InformationProtocol(Protocol protocol) : base(protocol) { }

        /// <summary>
        /// Attempts to send a async handshake to the defined webaddress.
        /// Uses a callback to respond with the result of the request
        /// </summary>
        /// <param name="complete">Callback that fires upon completion of the request</param>
        public override void Send(Action<UnityWebRequest> complete = null) {
            var handshake = UnityWebRequest.Post(_webReference + _webController + ".php", _params);
            var request = handshake.SendWebRequest();
            request.completed += (action) => {
                if (handshake.isHttpError || handshake.isNetworkError) {
                    if (_error != null)
                        _error.Invoke();
                } else if (complete != null)
                    complete.Invoke(handshake);
            };
        }
    }
}
