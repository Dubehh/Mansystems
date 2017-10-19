using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Manny))]
public class DashboardController : MonoBehaviour {

    [SerializeField]
    public Manny Manny;

    [SerializeField]
    public Slider ExperienceIndicator;

    [SerializeField]
    public Text LevelIndicator;

    [SerializeField]
    public RawImage Dialog;

    private UIStatIndicator _indicator;

    // Use this for initialization
    private void Start() {
        _indicator = new UIStatIndicator(ExperienceIndicator, Attribute.Experience, Manny);
        SetExperienceGoal();
        UpdateIndicators();
    }

    /// <summary>
    /// Updates the experience indicator and level if the player reaches a new level
    /// and contains the functionality for the dialog to vanish on touch
    /// </summary>
    public void UpdateIndicators() {
        _indicator.Update();

        if (ExperienceIndicator.value >= ExperienceIndicator.maxValue) {
            Manny.Attribute.IncrementAttribute(Attribute.Level, 1);
            SetExperienceGoal();
        }

        if (Input.touchCount > 0) 
            Dialog.gameObject.SetActive(false);
        
    }

    /// <summary>
    /// Gets the needed experience for the next level and passes it to the slider
    /// </summary>
    private void SetExperienceGoal() {
        var level = (int)Manny.Attribute.GetAttribute(Attribute.Level);
        ExperienceIndicator.minValue = Manny.Attribute.GetAttribute(Attribute.Experience);
        LevelIndicator.text = "Level " + level;
        _indicator.SetMax((int)Manny.Leveling.GetRequiredExperience(level + 1));
    }

    /// <summary>
    /// Displays a dialog on the screen with a message
    /// </summary>
    /// <param name="message">The message to be displayed in the dialog</param>
    public void DisplayDialog(string message) {
        Dialog.gameObject.SetActive(true);
        Dialog.GetComponentInChildren<Text>().text = message;
    }
}
