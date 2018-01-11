using Assets.Scripts.Controls.Dashboard;
using UnityEngine;

namespace Assets.Scripts.Manny {
    public class Manny : MonoBehaviour {
        private MannyBrain _brain;
        private MannyNotification _notification;

        [SerializeField] public DashboardController Dashboard;

        // DEBUG
        public bool DeleteAttributes;

        public MannyLeveling Leveling { get; set; }
        public MannyAttribute Attribute { get; set; }

        private void Awake() {
            _brain = new MannyBrain(this);
            _notification = new MannyNotification(this);
            Attribute = new MannyAttribute();
            Leveling = new MannyLeveling();
            _brain.Initialize();
        }

        private void Update() {
            if (HasDied() && Dashboard.GetCurrentBackground() != Dashboard.BackgroundDead) {
                Dashboard.InvalidateBackground();
                return;
            }
            _brain.Update();
        }

        /// <summary>
        ///     Once the application pauses all attributes are saved for the next session
        /// </summary>
        private void OnApplicationQuit() {
            OnExit();
        }

        private void OnApplicationPause() {
            OnExit();
        }

        private void OnExit() {
            Attribute.Save();
            _notification.Send();
            if (!DeleteAttributes) return;
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        ///     Returns true if manny is considered dead
        /// </summary>
        public bool HasDied() {
            return Attribute.GetAttribute(Scripts.Manny.Attribute.Food) <= 0 &&
                   Attribute.GetAttribute(Scripts.Manny.Attribute.Thirst) <= 0;
        }
    }
}