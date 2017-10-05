using UnityEngine;

public class Manny : MonoBehaviour {

    private MannyBrain _brain;
    public MannyLeveling Leveling { get; set; }
    public MannyAttribute Attribute { get; set; }

    // DEBUG
    public bool DeleteAttributes;
    
    private void Awake() {
        _brain = new MannyBrain(this);
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
    private void OnApplicationPause() {
        Attribute.Save();
        if (DeleteAttributes) PlayerPrefs.DeleteAll();
    }
}
