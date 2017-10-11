using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.App {

    [Serializable]
    public struct AudioItem {

        [SerializeField]
        public AudioSource Source;

        [SerializeField]
        public string Key;
    }
}
