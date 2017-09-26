using UnityEngine;

public class Manny : MonoBehaviour {
    private MannyBrain _mannyBrain;
    private MannyAttribute _mannyAttribute;

    // Use this for initialization
    private void Start() {
        _mannyAttribute = new MannyAttribute();
        _mannyBrain = new MannyBrain(_mannyAttribute);
    }

    // Update is called once per frame
    private void Update() {
        _mannyBrain.Update();
    }

    private void OnApplicationQuit() {
        _mannyAttribute.Save();
    }
}
