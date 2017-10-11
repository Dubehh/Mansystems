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

    [SerializeField]
    public Text CoinsIndicator;

    private Dictionary<Attribute, UIStatIndicator> _sliders { get; set; }

    private void Awake() {
        _sliders = new Dictionary<Attribute, UIStatIndicator>();
    }

    private void Start() {
        _sliders.Add(Attribute.Food, new UIStatIndicator(FoodIndicator, Attribute.Food, Manny));
        _sliders.Add(Attribute.Thirst, new UIStatIndicator(ThirstIndicator, Attribute.Thirst, Manny));
        _sliders.Add(Attribute.Experience, new UIStatIndicator(ExperienceIndicator, Attribute.Experience, Manny));
        
        SetExperienceGoal();
    }

    private void Update() {
        foreach (var slider in _sliders)
            slider.Value.Update();

        CoinsIndicator.text = "€" + Manny.Attribute.GetAttribute(Attribute.Coins);
    }

    /// <summary>
    /// Gets the needed experience for the next level and passes it to the slider
    /// </summary>
    public void SetExperienceGoal() {
        int level = (int)Manny.Attribute.GetAttribute(Attribute.Level) + 1;
        _sliders[Attribute.Experience].SetMax((int)Manny.Leveling.GetRequiredExperience(level));
    }
}
