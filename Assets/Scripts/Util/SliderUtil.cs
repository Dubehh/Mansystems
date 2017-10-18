using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SliderUtil {
    /// <summary>
    /// Changes the background color of a given slider to a lighter version
    /// </summary>
    /// <param name="slider">The slider to be given a new background color</param>
    /// <returns>The updated slider</returns>
    public static Slider GetSliderBackground(Slider slider) {
        var baseColor = new Color();
        Image background = null;
        foreach (var image in slider.GetComponentsInChildren<Image>()) {
            if (image.name == "Fill") baseColor = image.color;
            if (image.name == "Background") background = image;
        }

        baseColor.a /= 1.8f;
        background.color = baseColor;

        return slider;
    }

    public static void GradualFill(Slider slider, float amount) {
        slider.value = Mathf.Lerp(slider.value, amount, 4 * Time.deltaTime);
    }
}
