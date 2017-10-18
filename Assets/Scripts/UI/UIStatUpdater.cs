using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Manny))]
public class UIStatUpdater : MonoBehaviour {
    [SerializeField]
    public Manny Manny;

    [SerializeField]
    public Slider FoodIndicator, ThirstIndicator;

    private Dictionary<Attribute, UIStatIndicator> _sliders { get; set; }

    private void Awake() {
        _sliders = new Dictionary<Attribute, UIStatIndicator>();
    }

    private void Start() {
        _sliders.Add(Attribute.Food, new UIStatIndicator(FoodIndicator, Attribute.Food, Manny));
        _sliders.Add(Attribute.Thirst, new UIStatIndicator(ThirstIndicator, Attribute.Thirst, Manny));
    }

    public void UpdateSliders() {
        foreach (var slider in _sliders)
            slider.Value.Update();
    }
}
