using UnityEngine;

namespace Assets.Scripts.Minigames.Finder.Profile_management {
    public class ProfileManagement : MonoBehaviour {
        private GameObject _currentView;

        [SerializeField] public GameObject Main;

        /// <summary>
        ///     Hides the current view and opens a new one
        /// </summary>
        /// <param name="view"></param>
        public void OpenView(GameObject view) {
            if (_currentView != null) _currentView.SetActive(false);
            _currentView = view;
            _currentView.SetActive(true);
        }

        /// <summary>
        ///     Opens the main view
        /// </summary>
        public void OpenView() {
            OpenView(Main);
        }
    }
}