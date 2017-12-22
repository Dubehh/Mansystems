using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[RequireComponent(typeof(Manny))]
public class DashboardLifeCycle : MonoBehaviour {

    private DashboardController _dashboard;
    private UIController _controller;
    private MannyAttribute _attributes;

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

