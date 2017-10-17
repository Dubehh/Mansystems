using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Manny))]
public class DashboardController : MonoBehaviour {

    [SerializeField]
    public Manny Manny;

    [SerializeField]
    public Slider ExperienceIndicator;

    [SerializeField]
    public Text LevelIndicator;

    public UIStatIndicator Indicator;

    // Use this for initialization
    void Start () {
        Indicator = new UIStatIndicator(ExperienceIndicator, Attribute.Experience, Manny);
        SetExperienceGoal();
    }

    // Update is called once per frame
    void Update () {
        Indicator.Update();
        Manny.Attribute.IncrementAttribute(Attribute.Experience, 1);
        if (ExperienceIndicator.value >= ExperienceIndicator.maxValue) {
            Manny.Attribute.IncrementAttribute(Attribute.Level, 1);
            SetExperienceGoal();
        }
	}

    /// <summary>
    /// Gets the needed experience for the next level and passes it to the slider
    /// </summary>
    public void SetExperienceGoal() {
        int level = (int)Manny.Attribute.GetAttribute(Attribute.Level);
        ExperienceIndicator.minValue = Manny.Attribute.GetAttribute(Attribute.Experience);
        LevelIndicator.text = "Level " + level;
        Indicator.SetMax((int)Manny.Leveling.GetRequiredExperience(level + 1));
    }
}
