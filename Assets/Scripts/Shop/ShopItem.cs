using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItem {
    [SerializeField]
    public string Name;

    [SerializeField]
    public Texture Icon;

    [SerializeField]
    public string Description;

    [SerializeField]
    public int Cost;

    [SerializeField]
    public Attribute Attribute;

    [SerializeField]
    public float Value;

    public void Buy(Manny manny) {
        if (manny.Attribute.GetAttribute(Attribute.Coins) >= Cost) {
            manny.Attribute.IncrementAttribute(Attribute, Value);
            manny.Attribute.IncrementAttribute(Attribute.Coins, -Cost);
        }
    }
}
