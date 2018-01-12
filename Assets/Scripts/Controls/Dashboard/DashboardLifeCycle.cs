using System;
using Assets.Scripts.App;
using Assets.Scripts.Manny;
using Assets.Scripts.UI;
using UnityEngine;
using Attribute = Assets.Scripts.Manny.Attribute;

namespace Assets.Scripts.Controls.Dashboard {
    [RequireComponent(typeof(Manny.Manny))]
    public class DashboardLifeCycle : MonoBehaviour {
        private MannyAttribute _attributes;
        private UIController _controller;

        private DashboardController _dashboard;

        private void Awake() {
            _controller = FindObjectOfType<UIController>();
            _dashboard = FindObjectOfType<DashboardController>();
            _attributes = AppData.Instance().MannyAttribute;
        }

        private void OnEnable() {
            _dashboard.Dialog.gameObject.SetActive(false);
            _controller.Footer.SetActive(false);
            _controller.Navigation.SetActive(false);
        }

        /// <summary>
        ///     Fired upon restarting hitting the restart button
        /// </summary>
        public void OnRestartClick() {
            foreach (Attribute attr in Enum.GetValues(typeof(Attribute)))
                _attributes.SetAttribute(attr, _attributes.GetDefault(attr));
            _attributes.Save();
            _controller.Footer.SetActive(true);
            _controller.Navigation.SetActive(true);
            _dashboard.InvalidateBackground();
        }
    }
}