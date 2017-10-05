using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopItemPrefab : MonoBehaviour {

    private Manny _manny;

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

    /// <summary>
    /// Fill the prefab with the information from the shopitem
    /// </summary>
    public void Init() {
        _manny = FindObjectOfType<Manny>();
        Icon.texture = Item.Icon;
        Name.text = Item.Name;
        Description.text = Item.Description;
        Gain.text = "+" + Item.Value + " " + Enum.GetName(typeof(Attribute), Item.Attribute);
        Cost.text += Item.Cost;
    }

    /// <summary>
    /// OnClick event for the item's buy button
    /// </summary>
    public void OnClick() {
        Item.Buy(_manny);
    }
}
