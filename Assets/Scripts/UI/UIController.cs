using Assets.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    private GameObject _navigation;
    [SerializeField]
    private GameObject _footer;

    private UIControl _current;
    private Animator _animator;
    private bool _navigationVisible;

    [SerializeField]
    public Text ControlHeader;
    [SerializeField]
    public UIControl[] Controls;

    /// <summary>
    /// Fires when the application launches (awakes), this is called before the Start()
    /// </summary>
    private void Awake() {
        _navigationVisible = false;
        _animator = _navigation.GetComponent<Animator>();
        Screen.sleepTimeout = 0;
        LoadDefault();
        foreach (var ctrl in Controls)
            if (!ctrl.Active && ctrl.Parent.activeSelf)
                ctrl.Toggle(false);
    }

    public bool IsFirstTime() {
        return !PlayerPrefs.HasKey("name");
    }

    /// <summary>
    /// Loads the default control as the current view
    /// </summary>
    public void LoadDefault() {
        var ctrl = IsFirstTime() ? Get("WelcomeControl") : Controls.Where(x => x.Default).FirstOrDefault();
        if (ctrl != null)
            View(ctrl);
    }

    /// <summary>
    /// Opens the control with the given name as the new view, the current control will be toggled off
    /// </summary>
    /// <param name="control">The name of the control</param>
    public void View(string control) {
        var ctrl = Get(control);
        if (ctrl.Equals(default(UIControl)))
            LoadDefault();
        else View(ctrl);
        OnNavigationInteract();
    }

    /// <summary>
    /// Opens the given control as the new view, the current control will be toggled off
    /// </summary>
    /// <param name="control">The UIControl that you want to view</param>
    public void View(UIControl control) {
        if (_current != null)
            _current.Toggle(false);
        control.Toggle(true);
        OnControllerChange(control);
        _current = control;
    }

    /// <summary>
    /// Returns the UIControl that corresponds with the given name
    /// </summary>
    /// <param name="name">The name of the control</param>
    private UIControl Get(string name){
        return Controls.Where(ctrl => ctrl.GetName() == name).FirstOrDefault();
    }

    /// <summary>
    /// Event that fires when the current UI control changes
    /// </summary>
    /// <param name="ctrl">The new UIControl</param>
    private void OnControllerChange(UIControl ctrl) {
        ControlHeader.text = ctrl.Title;
    }

    /// <summary>
    /// Fires when the navigation button is pressed and toggles the navigation menu
    /// </summary>
    public void OnNavigationInteract() {
        _navigationVisible = !_navigationVisible;
        if (_animator != null)
            _animator.Play(_navigationVisible ? "slideOpen" : "slideClose");
    }

    public GameObject GetNavigationComponent() {
        return _navigation;
    }

    public GameObject GetFooterComponent() {
        return _footer;
    }
}
