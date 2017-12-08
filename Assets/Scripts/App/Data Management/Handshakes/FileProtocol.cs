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

        private FileData _data;
        private readonly MonoBehaviour _caller;

        public FileProtocol(Protocol protocol, MonoBehaviour caller) : base(protocol) {
            _caller = caller;
        }

        public FileProtocol Put(string key, string filename, string contentType, byte[] data) {
            _data = new FileData {
                Data = data,
                Name = filename,
                Type = contentType,
                Key = key
            };
            return this;
        }

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
            form.AddField("UUID", PlayerPrefs.GetString("uid"));
            _caller.StartCoroutine(Request(form, onComplete));
        }

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
