﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Util {
    public static class SliderUtil {
        /// <summary>
        ///     Changes the background color of a given slider to a lighter version
        /// </summary>
        /// <param name="slider">The slider to be given a new background color</param>
        /// <returns>The updated slider</returns>
        public static Slider SetSliderBackground(Slider slider) {
            var baseColor = new Color();
            Image background = null;
            foreach (var image in slider.GetComponentsInChildren<Image>()) {
                switch (image.name) {
                    case "Fill":
                        baseColor = image.color;
                        break;
                    case "Background":
                        background = image;
                        break;
                }
            }

            baseColor.a /= 1.8f;
            if (background != null) background.color = baseColor;

            return slider;
        }

        /// <summary>
        ///     Gradually fills the slider to a given value and plays the particle system
        /// </summary>
        /// <param name="slider">The slider to update</param>
        /// <param name="amount">The goal value to reach</param>
        public static void GradualFill(Slider slider, float amount) {
            var particles = slider.GetComponentInChildren<ParticleSystem>();
            slider.value = Mathf.Lerp(slider.value, amount, 2 * Time.deltaTime);

            if (particles == null) return;
            if (!particles.isPlaying) particles.Play();
            else if (amount - slider.value < 3) particles.Stop();
        }
    }
}