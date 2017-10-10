using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SliderUtil {
    public static Slider GetSliderBackground(Slider slider) {
        var baseColor = new Color();
        Image background = null;
        foreach (var image in slider.GetComponentsInChildren<Image>()) {
            if (image.name == "Fill") baseColor = image.color;
            if (image.name == "Background") background = image;
        }

        baseColor.a /= 1.5f;
        background.color = baseColor;

        return slider;
    }
}
