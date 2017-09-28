using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Manny {
    public class MannyConditionStatus {

        public float Minimum { get; set; }
        public float Decrease { get; set; }
        public bool Weak { get; set; }
        public Attribute Attribute { get; set; }
        public string Message { get; set; }

        public void SetWeak(bool weak) {
            Weak = weak;
        }
    }
}
