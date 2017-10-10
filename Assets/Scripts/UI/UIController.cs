using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    public Text ControlHeader;
    [SerializeField]
    public UIControl[] Controls;
    private UIControl _current;

    /// <summary>
    /// Fires when the application launches (awakes), this is called before the Start()
    /// </summary>
    private void Awake() {
        Screen.sleepTimeout = 0;
        LoadDefault();
        foreach (var ctrl in Controls)
            if (!ctrl.Active && ctrl.Parent.activeSelf) {
                ctrl.Toggle(false);
            }
    }

    /// <summary>
    /// Loads the default control as the current view
    /// </summary>
    private void LoadDefault() {
        var ctrl = Controls.Where(x => x.Default).FirstOrDefault();
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
 
	private void Update () {
		
	}

    /// <summary>
    /// Event that fires when the current UI control changes
    /// </summary>
    /// <param name="ctrl">The new UIControl</param>
    private void OnControllerChange(UIControl ctrl) {
        ControlHeader.text = ctrl.Title;
    }
}
