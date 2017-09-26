using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.UI {
    [Serializable]
    public class UIControl {

        [SerializeField]
        public bool Default;
        [SerializeField]
        public GameObject Parent;
        public bool Active { get; set; }

        public void Toggle(bool active) {
            Active = active;
            Parent.SetActive(active);
        }

        public string GetName() {
            return Parent.name;
        }

    }
}
