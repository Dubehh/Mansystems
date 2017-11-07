using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Dashboard {

    [Serializable]
    public class DashboardBackground {

        [SerializeField]
        public GameObject Manny;
        [SerializeField]
        public GameObject Background;
        [SerializeField]
        public BackgroundTime Time;
    }
    
    [Serializable]
    public struct BackgroundTime {
        [SerializeField]
        public int Min;
        [SerializeField]
        public int Max;
    }
}
