using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.App.Data_Management.Handshakes {

    public struct ContentType {

        public const string Png = "image/png";
        public const string Jpeg = "image/jpeg";
    }

    public struct FileData {

        public string Key { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Data { get; set; }    
    }

    public class FileProtocol : HandshakeProtocol<WWW> {

        private string _uuid;
        private FileData _data;
        private readonly MonoBehaviour _caller;

        public FileProtocol(Protocol protocol, MonoBehaviour caller) : base(protocol) {
            _caller = caller;
            _uuid = null;
        }

        /// <summary>
        /// Puts a file inside the handshake
        /// </summary>
        /// <param name="key">string key</param>
        /// <param name="filename">string filename</param>
        /// <param name="contentType">string contenttype</param>
        /// <param name="data">byte[] file raw data</param>
        /// <returns>Fileprotocol instance</returns>
        public FileProtocol Put(string key, string filename, string contentType, byte[] data) {
            _data = new FileData {
                Data = data,
                Name = filename,
                Type = contentType,
                Key = key
            };
            return this;
        }

        /// <summary>
        /// Sets the target folder for the file protocol
        /// </summary>
        /// <param name="folder">string folder name</param>
        /// <returns>Fileprotocol instance</returns>
        public FileProtocol Target(string folder) {
            AddParameter("targetFolder", folder);
            return this;
        }

        /// <summary>
        /// Sets the uuid for the file protocol
        /// </summary>
        /// <param name="uuid">string uuid</param>
        /// <returns>Fileprotocol instance</returns>
        public FileProtocol For(string uuid) {
            _uuid = uuid;
            return this;
        }

        /// <summary>
        /// Sends the file protocol
        /// </summary>
        /// <param name="onComplete">callback that fires upon handshake completion</param>
        public override void Send(Action<WWW> onComplete = null) {
            var form = new WWWForm();
            if (_protocol == Protocol.Upload) {
                if (_data.Data == null)
                    throw new ArgumentNullException("_data.Data", "An upload protocol requires a file to have binary data.");
                form.AddBinaryData(_data.Key, _data.Data, _data.Name, _data.Type);
            }
            foreach (var pair in _params) {
                var key = pair.sectionName;
                var value = Encoding.Default.GetString(pair.sectionData);
                form.AddField(key, value);
            }
            form.AddField("UUID", _uuid ?? PlayerPrefs.GetString("uid"));
            _caller.StartCoroutine(Request(form, onComplete));
        }

        /// <summary>
        /// Starts the coroutine w/ request
        /// </summary>
        /// <param name="form">WWWForm data content</param>
        /// <param name="onComplete">callback response</param>
        /// <returns></returns>
        private IEnumerator Request(WWWForm form, Action<WWW> onComplete = null) {
            var handshake = new WWW(_webReference + _webController + ".php", form);
            yield return handshake;
            if(_error != null && !string.IsNullOrEmpty(handshake.error))
                _error.Invoke();
            else if(onComplete != null)
                onComplete.Invoke(handshake);
            handshake.Dispose();
            handshake = null;
        }
    }
}
