using System;
using UnityEngine;
using Attribute = Assets.Scripts.Manny.Attribute;

namespace Assets.Scripts.Controls.Shop {
    [Serializable]
    public class ShopItem {
        [SerializeField] public Attribute Attribute;
        [SerializeField] public int Cost;
        [SerializeField] public string Description;
        [SerializeField] public Texture Icon;
        [SerializeField] public string Name;
        [SerializeField] public float Value;

        /// <summary>
        ///     This function is called when the player buys an item.
        ///     It updates the specific attribute en decreases the player's money.
        /// </summary>
        /// <param name="manny">The manny object of the game</param>
        public void Buy(Manny.Manny manny) {
            if (manny.Attribute.GetAttribute(Attribute) > 100 - Value)
                manny.Attribute.SetAttribute(Attribute, 101);
            else
                manny.Attribute.IncrementAttribute(Attribute, Value);
            manny.Attribute.IncrementAttribute(Attribute.Coins, -Cost);
        }
    }
}