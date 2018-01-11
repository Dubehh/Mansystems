using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace Assets.Scripts.App.Data_Management.Handshakes {
    public enum Protocol {
        Tracking,
        Data,
        Upload,
        Download
    }

    public abstract class HandshakeProtocol<T> {
        private const string _handshakeID = "streamType";

        protected const string
            _webReference = "https://acc-cibap.mansystems.nl/app/",
            _webController = "handshake";

        protected readonly List<IMultipartFormSection> _params;
        protected readonly Protocol _protocol;
        protected Action<string> _error;

        /// <summary>
        ///     Instantiates a new Handshake that may be used to communicate with the defined webhost
        /// </summary>
        /// <param name="protocol">The type of the handshake</param>
        protected HandshakeProtocol(Protocol protocol) {
            _protocol = protocol;
            _params = new List<IMultipartFormSection>();
            AddParameter(_handshakeID, protocol.ToString());
        }

        /// <summary>
        ///     Sets the error callback that is fired when the handshake returned an error
        /// </summary>
        /// <param name="callback">Action callback</param>
        public HandshakeProtocol<T> OnError(Action<string> error) {
            _error = error;
            return this;
        }

        /// <summary>
        ///     Adds a data parameter to the handshake
        /// </summary>
        /// <param name="key">string key</param>
        /// <param name="value">string value</param>
        /// <returns>The handshake instance; builder pattern principle</returns>
        public HandshakeProtocol<T> AddParameter(string key, string value) {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(key, "Value is empty");
            _params.Add(new MultipartFormDataSection(key, value));
            return this;
        }

        /// <summary>
        ///     Sends the protocol
        /// </summary>
        /// <param name="onComplete">Action callback</param>
        public abstract void Send(Action<T> onComplete = null);
    }
}