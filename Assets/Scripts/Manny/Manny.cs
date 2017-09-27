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
    }

    // Update is called once per frame
    private void Update() {
        _brain.Update();     
    }
    
    private void OnApplicationQuit() {
        _attribute.Save();
        if (DeleteAttributes) PlayerPrefs.DeleteAll();
    }
}
