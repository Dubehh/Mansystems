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

        /// <summary>
        /// Toggles the visibility of the control
        /// </summary>
        /// <param name="active"></param>
        public void Toggle(bool active) {
            Active = active;
            Parent.SetActive(active);
        }

        /// <summary>
        /// Returns the name of the control
        /// </summary>
        public string GetName() {
            return Parent.name;
        }

    }
}
