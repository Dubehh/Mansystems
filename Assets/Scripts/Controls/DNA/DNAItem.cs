using System;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;
using Attribute = Assets.Scripts.Manny.Attribute;

namespace Assets.Scripts.Controls.DNA {
    /// <summary>
    ///     Represents a DNA attribute that can be changed
    /// </summary>
    [Serializable]
    public class DNAItem {
        private Manny.Manny _manny;

        [SerializeField] public Attribute Attribute;
        [SerializeField] public Slider Slider;

        /// <summary>
        ///     Makes the reference to the Manny instance
        /// </summary>
        /// <param name="manny">Manny main instance</param>
        public void SetInstance(Manny.Manny manny) {
            _manny = manny;
            Slider = SliderUtil.SetSliderBackground(Slider);
            Slider.maxValue = 5;
        }

        /// <summary>
        ///     Updates the slider
        /// </summary>
        public void Update() {
            Slider.value = _manny.Attribute.GetAttribute(Attribute);
            Slider.GetComponentInChildren<Text>().text = (int) Slider.value + " / " + Slider.maxValue;
        }
    }
}