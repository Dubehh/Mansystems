using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour {

    [SerializeField]
    public UIControl[] Controls;
    private UIControl _current;

    private void Awake() {
        LoadDefault();
        foreach (var ctrl in Controls)
            if (!ctrl.Active && ctrl.Parent.activeSelf) {
                ctrl.Toggle(false);
            }
    }

    private void LoadDefault() {
        var ctrl = Controls.Where(x => x.Default).FirstOrDefault();
        if (ctrl != null) {
            _current = ctrl;
            ctrl.Toggle(true);
        }
    }

    public void View(string control) {
        var ctrl = Get(control);
        if (ctrl.Equals(default(UIControl))) LoadDefault();
        else View(ctrl);
    }

    public void View(UIControl control) {
        if (_current != null)
            _current.Toggle(false);
        control.Toggle(true);
        OnControllerChange(control);
        _current = control;
    }

    private UIControl Get(string name){
        return Controls.Where(ctrl => ctrl.GetName() == name).FirstOrDefault();
    }
 
	private void Update () {
		
	}

    private void OnControllerChange(UIControl ctrl) {
        //fired when the controller changed
    }

    public void OnClick() {
        View("ShopControl");
    }
}
