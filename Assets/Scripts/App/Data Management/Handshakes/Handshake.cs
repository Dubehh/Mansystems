using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.App.Data_Management {
    public class Handshake {

        private const string
            _webReference = "http://localhost/app/",
            _webController = "handshake";

        private const string _handshakeID = "streamType";
        private Action _errorCallback;
        private readonly List<IMultipartFormSection> _params;

        /// <summary>
        /// Instantiates a new Handshake that may be used to communicate with the defined webhost
        /// </summary>
        /// <param name="protocol">The type of the handshake</param>
        public Handshake(HandshakeProtocol protocol) {
            _params = new List<IMultipartFormSection>();
            AddParameter(_handshakeID, protocol.ToString());
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
        /// Adds binary file data to the handshake
        /// </summary>
        /// <param name="key">string key</param>
        /// <param name="fileName">string file name</param>
        /// <param name="fileData">byte[] data</param>
        /// <returns>The handshake instance; builder pattern principle</returns>
        public Handshake AddFile(string key, string fileName, byte[] fileData) {
            _params.Add(new MultipartFormFileSection(key, fileData, fileName, "image/png"));
            return this;
        }

        /// <summary>
        /// Sets the given callback as the error callback
        /// </summary>
        /// <param name="callback">Action callback</param>
        public void SetErrorHandler(Action callback) {
            _errorCallback = callback;
        }

        /// <summary>
        /// Attempts to send a async handshake to the defined webaddress.
        /// Uses a callback to respond with the result of the request
        /// </summary>
        /// <param name="complete">Callback that fires upon completion of the request</param>
        public void Shake(Action<UnityWebRequest> complete = null) {
            var handshake = UnityWebRequest.Post(_webReference + _webController + ".php", _params);
            var request = handshake.SendWebRequest();
            request.completed += (action) => {
                if (handshake.isHttpError || handshake.isNetworkError) {
                    if (_errorCallback != null)
                        _errorCallback.Invoke();
                } else if (complete != null)
                    complete.Invoke(handshake);

            };
        }

        /// <summary>
        /// Validates the connection to the server
        /// </summary>
        /// <param name="onValidateSuccess">Callback to invoke if there is a connection</param>
        /// <param name="onValidateError">Callback to invoke if there is no connection</param>
        public static void Validate(Action onValidateSuccess = null, Action onValidateError = null) {
            var handshake = new Handshake(HandshakeProtocol.TrackingUpdate);
            handshake.SetErrorHandler(() => {
                if (onValidateError != null)
                    onValidateError.Invoke();
            });
            handshake.Shake((request) => {
                if (onValidateSuccess != null)
                    onValidateSuccess.Invoke();
            });
        }
    }
}
