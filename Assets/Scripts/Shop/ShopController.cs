using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopController : MonoBehaviour {

    private const float _yStart = 340f;
    private const byte _transparencyStart = 255;
    private RectTransform _scrollRect;
    private Manny _manny;

    [SerializeField]
    public Text CoinsIndicator;

    [SerializeField]
    public ShopItem[] Items;

    [SerializeField]
    public GameObject Item;

    // Use this for initialization
    void Start() {
        var y = _yStart;
        var transparency = _transparencyStart;

        _scrollRect = GetComponent<RectTransform>();
        _manny = FindObjectOfType<Manny>();

        UpdateCoins();

        // Loop through the ShopItem array and create a ShopItemPrefab with each one of them
        foreach (var item in Items) {
            if (transparency < 140) transparency = _transparencyStart;

            var obj = Instantiate(Item, transform).GetComponent<ShopItemPrefab>();
            obj.Item = item;
            obj.Init();

            obj.GetComponent<Image>().color = item.Attribute == Attribute.Food
                ? new Color32(18, 178, 112, transparency)
                : new Color32(26, 118, 175, transparency);

            obj.transform.localPosition = new Vector2(0, y);
            y -= obj.GetComponent<RectTransform>().rect.height;
            transparency -= 30;
        }
    }

    /// <summary>
    /// Updates the coins in the indicator panel on the top of the shop once the player buys an item or opens the shop
    /// </summary>
    public void UpdateCoins() {
        CoinsIndicator.text = "Jouw coins:\n" + _manny.Attribute.GetAttribute(Attribute.Coins);
    }

    /// <summary>
    /// This method makes sure that the user cannot scroll outside of the content of the panel
    /// </summary>
    /// <param name="vec">The 2D vector of the current location of the scroll content</param>
    public void OnValueChanged(Vector2 vec) {
        var minY = -7.5f;
        var maxY = 442.45f - minY;
        if (_scrollRect.anchoredPosition.y < minY) _scrollRect.anchoredPosition = new Vector2(0, minY);
        if (_scrollRect.anchoredPosition.y > maxY) _scrollRect.anchoredPosition = new Vector2(0, maxY);
    }
}
