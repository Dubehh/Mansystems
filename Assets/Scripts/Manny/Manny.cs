using UnityEngine;

public class Manny : MonoBehaviour {
    private MannyBrain _brain;
    private MannyAttribute _attribute;
    private MannyLeveling _leveling;

    // Use this for initialization
    private void Start() {
        _brain = new MannyBrain(this);
        _attribute = new MannyAttribute();
        _leveling = new MannyLeveling(this);
    }

    // Update is called once per frame
    private void Update() {
        _brain.Update();
        Debug.Log(_leveling.GetLevel(_attribute.GetAttribute(Attribute.Experience)));
        _attribute.SetAttribute(Attribute.Experience, _attribute.GetAttribute(Attribute.Experience) + 10);        
    }

    private void OnApplicationQuit() {
        _attribute.Save();
    }
}
