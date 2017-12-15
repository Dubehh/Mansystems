using System;
using UnityEngine;

namespace Assets.Scripts.Dashboard {
    [Serializable]
    public class DashboardBackground {
        [SerializeField] public GameObject Background;

        [SerializeField] public GameObject Manny;

        [SerializeField] public BackgroundTime Time;
    }

    [Serializable]
    public struct BackgroundTime {
        [SerializeField] public int Min;
        [SerializeField] public int Max;
    }
}