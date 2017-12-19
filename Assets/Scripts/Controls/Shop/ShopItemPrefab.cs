using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopItemPrefab : MonoBehaviour {
    private Manny _manny;
    private ShopController _shop;

    [SerializeField] public Text Cost;
    [SerializeField] public Text Description;
    [SerializeField] public Text Gain;
    [SerializeField] public RawImage Icon;
    [SerializeField] public Text Name;
    [SerializeField] public ParticleSystem ParticleSystem;

    public ShopItem Item { get; set; } 

    /// <summary>
    ///     Fill the prefab with the information from the shopitem
    /// </summary>
    public void Init() {
        _manny = FindObjectOfType<Manny>();
        _shop = FindObjectOfType<ShopController>();
        Icon.texture = Item.Icon;
        Icon.color = Item.Attribute == Attribute.Food
            ? new Color32(18, 178, 112, 255)
            : new Color32(26, 118, 175, 255);

        Name.text = Item.Name;
        Description.text = Item.Description;

        Gain.text = "+" + Item.Value + " " + (Item.Attribute == Attribute.Food ? "Eten" : "Drinken");

        Cost.text = Item.Cost.ToString();
        var emis = ParticleSystem.emission;
        emis.SetBurst(0, new ParticleSystem.Burst(0.0f, Item.Cost));
    }

    /// <summary>
    ///     OnClick event for the item's buy button
    /// </summary>
    public void OnClick() {
        if (_manny.Attribute.GetAttribute(Attribute.Coins) >= Item.Cost) {
            ParticleSystem.Play();
            Item.Buy(_manny);
            _shop.UpdateCoins();
        }
    }
}