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
    [SerializeField]
    public ParticleSystem ParticleSystem;
    [SerializeField]
    private GameObject _moreInfo;

    private void Start() {
        DisplayText.text = Manny.Attribute.GetAttribute(Attribute.Skillpoints) + "";
        foreach (var item in Items) {
            item.SetInstance(Manny);
            item.Update();
        }
    }

    private void Update() {
        if (_moreInfo.activeSelf && Input.touchCount > 0) _moreInfo.SetActive(false);  
    }

    public void OnClickNavigation() {
        _moreInfo.SetActive(true);
    }

    /// <summary>
    /// Fires when the button next to the DNAItem sliders are being clicked
    /// </summary>
    public void OnClickButton() {
        var obj = EventSystem.current.currentSelectedGameObject;
        var slider = obj.GetComponentInParent<Slider>();
        var item = Items.FirstOrDefault(x => x.Slider == slider);
        if (item == null) return;
        if (Manny.Attribute.GetAttribute(Attribute.Skillpoints) <= 0 || slider.value >= 5) return;
        Manny.Attribute.IncrementAttribute(Attribute.Skillpoints, -1);
        Manny.Attribute.IncrementAttribute(item.Attribute, 1);
        item.Update();
        ParticleSystem.Play();
        DisplayText.text = Manny.Attribute.GetAttribute(Attribute.Skillpoints)+"";
    }

}
