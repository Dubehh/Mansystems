using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DNAItem {

    [SerializeField]
    public Attribute Attribute;
    [SerializeField]
    public Slider Slider;
    private Manny _manny;

    public void SetInstance(Manny manny) {
        _manny = manny;
        Slider.maxValue = 5;
    }

    public void Update() {
        Slider.value = _manny.Attribute.GetAttribute(Attribute);
        Slider.GetComponentInChildren<Text>().text = (int)Slider.value + " / " + Slider.maxValue;
    }
}

