﻿using System;
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

    /// <summary>
    /// This function is called when the player buys an item. 
    /// It updates the specific attribute en decreases the player's money.
    /// </summary>
    /// <param name="manny">The manny object of the game</param>
    public void Buy(Manny manny) {
        if (manny.Attribute.GetAttribute(Attribute) > (100 - Value))
            manny.Attribute.SetAttribute(Attribute, 100);
        else
            manny.Attribute.IncrementAttribute(Attribute, Value);
        manny.Attribute.IncrementAttribute(Attribute.Coins, -Cost);
    }
}