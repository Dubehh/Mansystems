using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.App.Data_Management.Handshakes {
    public class HandshakeFile {

        public const string Image = "image/png";

        public string Name { get; private set; } 
        public string Type { get; private set; }
        public byte[] Data { get; private set; }
        public HandshakeFile(string filename, byte[] data, string type) {
            Name = filename;
            Type = type;
            Data = data;
        }
    }
}
