using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DNAController : MonoBehaviour {

    [SerializeField]
    public Text DisplayText;
    [SerializeField]
    public Manny Manny;
    [SerializeField]
    public DNAItem[] Items;

    private void Start() {
        DisplayText.text = Manny.Attribute.GetAttribute(Attribute.Skillpoints) + "";
        foreach (var item in Items) {
            item.SetInstance(Manny);
            item.Update();
        }
    }

    public void OnClickNavigation() {
    }

    /// <summary>
    /// Fires when the button next to the DNAItem sliders are being clicked
    /// </summary>
    public void OnClickButton() {
        var obj = EventSystem.current.currentSelectedGameObject;
        var slider = obj.GetComponentInParent<Slider>();
        var item = Items.Where(x => x.Slider == slider).FirstOrDefault();
        if (item != null) {
            if (Manny.Attribute.GetAttribute(Attribute.Skillpoints) <= 0) return;
            Manny.Attribute.IncrementAttribute(Attribute.Skillpoints, -1);
            Manny.Attribute.IncrementAttribute(item.Attribute, 1);
            item.Update();
            DisplayText.text = Manny.Attribute.GetAttribute(Attribute.Skillpoints)+"";
        }
    }

}
