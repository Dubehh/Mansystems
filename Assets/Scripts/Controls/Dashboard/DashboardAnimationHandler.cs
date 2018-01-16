using UnityEngine;

namespace Assets.Scripts.Controls.Dashboard {
    public class DashboardAnimationHandler {
        private Animator _animator;

        public void SetAnimator(GameObject manny) {
            _animator = manny.GetComponent<Animator>();
        }

        /// <summary>
        ///     Looks if input from the player should start an event
        /// </summary>
        public void ScanInput() {
            if (_animator == null) return;
        }
    }
}