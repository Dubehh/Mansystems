using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.App.Tracking.Table {

    /// <summary>
    /// Represents a property inside the datatable
    /// </summary>
    public struct DataProperty {

        public enum DataPropertyType { VARCHAR, INT, DATETIME, TINYINT}
        public string Name { get; private set; }
        public DataPropertyType Type { get; private set; }
        public int? Size { get; private set; }

        public DataProperty(string name, DataPropertyType type, int? size = null) {
            Name = name;
            Type = type;
            Size = size;
        }
         
    }
}
