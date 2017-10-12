using UnityEngine;
using UnityEngine.UI;

public class UIStatIndicator {

    private Slider _slider;
    private Attribute _attribute;
    private Text _text;
    private Manny _manny;

    public UIStatIndicator(Slider component, Attribute attribute, Manny manny) {
        _slider = SliderUtil.GetSliderBackground(component);
        _attribute = attribute;
        _text = _slider.GetComponentInChildren<Text>();
        _manny = manny; 
    }

    /// <summary>
    /// Sets a new max value for the slider
    /// </summary>
    /// <param name="max">The new max value for the slider</param>
    public void SetMax(int max) {
        _slider.maxValue = max;
    }

    public void Update() {
        _slider.value = _manny.Attribute.GetAttribute(_attribute);
        _text.text = (int)_slider.value + " / " + _slider.maxValue;
    }
}
