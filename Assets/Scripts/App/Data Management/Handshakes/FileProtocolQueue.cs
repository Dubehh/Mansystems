using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.App.Data_Management.Handshakes {
    public class FileProtocolQueue {

        private readonly Action<FileProtocolQueue> _queueComplete;
        private readonly Action<WWW> _requestComplete;

        /// <summary>
        ///     Creates a new file protocol queue
        /// </summary>
        /// <param name="onComplete">Callback oncomplete</param>
        public FileProtocolQueue(Action<FileProtocolQueue> queueComplete, Action<WWW> requestComplete = null) {
            _queueComplete = queueComplete;
            _requestComplete = requestComplete;
            Count = 0;
            Queue = new HashSet<FileProtocol>();
        }

        public int Count { get; private set; }
        public HashSet<FileProtocol> Queue { get; private set; }

        /// <summary>
        ///     Attaches a handshake protocol to the queue
        /// </summary>
        /// <param name="protocol">File protocol instance</param>
        /// <returns>Queue instance</returns>
        public FileProtocolQueue Attach(FileProtocol protocol) {
            Count++;
            Queue.Add(protocol);
            return this;
        }

        /// <summary>
        ///     Commit the queue and send the requests
        /// </summary>
        public void Commit() {
            foreach (var protocol in Queue)
                protocol.Send(www => {
                    if (_requestComplete != null)
                        _requestComplete.Invoke(www);
                    Notify();
                });
        }

        /// <summary>
        ///     Notifies the queue that a request is complete
        /// </summary>
        private void Notify() {
            if (--Count > 0 || _queueComplete == null) return;
            _queueComplete.Invoke(this);
        }
    }
}