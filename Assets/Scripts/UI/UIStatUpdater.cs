using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Manny))]
public class UIStatUpdater : MonoBehaviour {
    [SerializeField]
    public Manny Manny;

    [SerializeField]
    public Slider FoodIndicator, ThirstIndicator, ExperienceIndicator;

    public Dictionary<Attribute, UIStatIndicator> Sliders { get; set; }

    private void Awake() {
        Sliders = new Dictionary<Attribute, UIStatIndicator>();
    }

    private void Start() {
        Sliders.Add(Attribute.Food, new UIStatIndicator(FoodIndicator, Attribute.Food, Manny));
        Sliders.Add(Attribute.Thirst, new UIStatIndicator(ThirstIndicator, Attribute.Thirst, Manny));
        Sliders.Add(Attribute.Experience, new UIStatIndicator(ExperienceIndicator, Attribute.Experience, Manny));

        SetExperienceGoal();
    }

    private void Update() {
        foreach (var slider in Sliders)
            slider.Value.Update();
    }

    /// <summary>
    /// Gets the needed experience for the next level and passes it to the slider
    /// </summary>
    private void SetExperienceGoal() {
        int level = (int)Manny.Attribute.GetAttribute(Attribute.Level) + 1;
        Sliders[Attribute.Experience].SetMax((int)Manny.Leveling.GetRequiredExperience(level));
    }
}
