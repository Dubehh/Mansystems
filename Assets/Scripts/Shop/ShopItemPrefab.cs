using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopItemPrefab : MonoBehaviour {

    private Manny Manny;
    public ShopItem Item;

    [SerializeField]
    public RawImage Icon;

    [SerializeField]
    public Text Name;

    [SerializeField]
    public Text Description;

    [SerializeField]
    public Text Gain;

    [SerializeField]
    public Text Cost;

    public void Init() {
        Manny = FindObjectOfType<Manny>();
        Icon.texture = Item.Icon;
        Name.text = Item.Name;
        Description.text = Item.Description;
        Gain.text = "+" + Item.Value + " " + Enum.GetName(typeof(Attribute), Item.Attribute);
        Cost.text += Item.Cost;
    }

    public void OnClick() {
        Item.Buy(Manny);
    }
}
