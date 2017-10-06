using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a DNA attribute that can be changed
/// </summary>
[Serializable]
public class DNAItem {

    [SerializeField]
    public Attribute Attribute;
    [SerializeField]
    public Slider Slider;
    private Manny _manny;

    /// <summary>
    /// Makes the reference to the Manny instance
    /// </summary>
    /// <param name="manny">Manny main instance</param>
    public void SetInstance(Manny manny) {
        _manny = manny;
        Slider.maxValue = 5;
    }

    /// <summary>
    /// Updates the slider
    /// </summary>
    public void Update() {
        Slider.value = _manny.Attribute.GetAttribute(Attribute);
        Slider.GetComponentInChildren<Text>().text = (int)Slider.value + " / " + Slider.maxValue;
    }
}

