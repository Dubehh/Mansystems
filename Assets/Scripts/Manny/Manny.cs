using System;
using UnityEngine;

public class Manny : MonoBehaviour {

    private MannyBrain _brain;
    private MannyNotification _notification;
    public MannyLeveling Leveling { get; set; }
    public MannyAttribute Attribute { get; set; }

    // DEBUG
    public bool DeleteAttributes;
    
    private void Awake() {
        _brain = new MannyBrain(this);
        _notification = new MannyNotification(this);
        Attribute = new MannyAttribute();
        Leveling = new MannyLeveling();
        _brain.Initialize();
    }

    private void Update() {
        _brain.Update();     
    }
    
    /// <summary>
    /// Once the application pauses all attributes are saved for the next session
    /// </summary>
    private void OnApplicationQuit() { OnExit(); }
    private void OnApplicationPause() { OnExit(); }

    private void OnExit() {
        Attribute.Save();
        _notification.Send();
        if (DeleteAttributes) PlayerPrefs.DeleteAll();
    }
}
