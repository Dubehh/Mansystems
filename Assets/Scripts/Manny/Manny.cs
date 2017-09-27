using UnityEngine;

public class Manny : MonoBehaviour {
    private MannyBrain _brain;
    private MannyAttribute _attribute;
    private MannyLeveling _leveling;

    // DEBUG
    public bool DeleteAttributes;

    // Use this for initialization
    private void Start() {
        _brain = new MannyBrain(this);
        _attribute = new MannyAttribute();
        _leveling = new MannyLeveling(this);
        _leveling.Test();
    }

    // Update is called once per frame
    private void Update() {
        _brain.Update();     
    }
    
    /// <summary>
    /// Once the application quits all attributes are saved for the next session
    /// </summary>
    private void OnApplicationQuit() {
        _attribute.Save();
        if (DeleteAttributes) PlayerPrefs.DeleteAll();
    }
}
