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
        Leveling = new MannyLeveling(this);
        Debug.Log("Lol");
    }

    // Update is called once per frame
    private void Update() {
        _brain.Update();     
    }
    
    /// <summary>
    /// Once the application quits all attributes are saved for the next session
    /// </summary>
    private void OnApplicationQuit() {
        Attribute.Save();
        if (DeleteAttributes) PlayerPrefs.DeleteAll();
    }
}
