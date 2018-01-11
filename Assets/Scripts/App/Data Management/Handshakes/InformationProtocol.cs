using System;
using UnityEngine.Networking;

namespace Assets.Scripts.App.Data_Management.Handshakes {
    public class InformationProtocol : HandshakeProtocol<UnityWebRequest> {
        /// <summary>
        ///     Used to seperate the different handshake handler types
        /// </summary>
        public enum HandlerType {
            Update,
            Fetch
        }

        public InformationProtocol(Protocol protocol) : base(protocol) {
        }

        /// <summary>
        ///     Attempts to send a async handshake to the defined webaddress.
        ///     Uses a callback to respond with the result of the request
        /// </summary>
        /// <param name="complete">Callback that fires upon completion of the request</param>
        public override void Send(Action<UnityWebRequest> complete = null) {
            var handshake = UnityWebRequest.Post(_webReference + _webController + ".php", _params);
            var request = handshake.SendWebRequest();
            request.completed += action => {
                if (handshake.isHttpError || handshake.isNetworkError) {
                    if (_error != null)
                        _error.Invoke(handshake.error);
                }
                else if (complete != null) {
                    complete.Invoke(handshake);
                }
            };
        }

        /// <summary>
        ///     Sets the webreceiver handler
        /// </summary>
        /// <param name="handlerReference">string handler name</param>
        /// <param name="type">HandlerType type</param>
        /// <returns></returns>
        public InformationProtocol SetHandler(string handlerReference, HandlerType type) {
            AddParameter("handler", handlerReference);
            AddParameter("handlerType", type.ToString());
            return this;
        }

        /// <summary>
        ///     Validates the connection to the server
        /// </summary>
        /// <param name="onValidateSuccess">Callback to invoke if there is a connection</param>
        /// <param name="onValidateError">Callback to invoke if there is no connection</param>
        public static void Validate(Action onValidateSuccess = null, Action onValidateError = null) {
            var handshake = new InformationProtocol(Protocol.Data);
            handshake.OnError(error => {
                if (onValidateError != null)
                    onValidateError.Invoke();
            });
            handshake.Send(request => {
                if (onValidateSuccess != null)
                    onValidateSuccess.Invoke();
            });
        }
    }
}