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

    [SerializeField]
    public RawImage Dialog;

    private UIStatIndicator _indicator;

    // Use this for initialization
    private void Start() {
        _indicator = new UIStatIndicator(ExperienceIndicator, Attribute.Experience, Manny);
        SetExperienceGoal();
        UpdateIndicators();
    }

    // Update is called once per frame
    public void UpdateIndicators() {
        _indicator.Update();

        if (ExperienceIndicator.value >= ExperienceIndicator.maxValue) {
            Manny.Attribute.IncrementAttribute(Attribute.Level, 1);
            SetExperienceGoal();
        }

        if (Input.touchCount > 0) DialogActive(false);
    }

    /// <summary>
    /// Gets the needed experience for the next level and passes it to the slider
    /// </summary>
    private void SetExperienceGoal() {
        int level = (int)Manny.Attribute.GetAttribute(Attribute.Level);
        ExperienceIndicator.minValue = Manny.Attribute.GetAttribute(Attribute.Experience);
        LevelIndicator.text = "Level " + level;
        _indicator.SetMax((int)Manny.Leveling.GetRequiredExperience(level + 1));
    }

    public void DisplayDialog(Attribute attribute, string message) {
        Color color = new Color32();

        switch (attribute) {
            case Attribute.Food:
                color = new Color32(7, 175, 106, 255);
                break;
            case Attribute.Thirst:
                color = new Color32(38, 116, 168, 255);
                break;
            case Attribute.Coins:
                color = new Color32(247, 148, 30, 255);
                break;
        }

        DialogActive(true);

        Dialog.color = color;
        Dialog.GetComponentInChildren<Text>().text = message;
    }

    public void DialogActive(bool active) {
        Dialog.gameObject.SetActive(active);
    }
}
