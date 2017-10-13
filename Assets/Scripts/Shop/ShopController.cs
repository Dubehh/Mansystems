using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopController : MonoBehaviour {

    private const byte _transparencyStart = 255;
    private Manny _manny;

    private float _prefabHeight;

    [SerializeField]
    public Text CoinsIndicator;

    [SerializeField]
    public ShopItem[] Items;

    [SerializeField]
    public GameObject Item;

    // Use this for initialization
    void Start() {
        var transparency = _transparencyStart;
        var y = -75f;

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

            var rect = obj.GetComponent<RectTransform>().rect;

            _prefabHeight = rect.height;

            obj.transform.localPosition = new Vector2(rect.width / 2, y);
            y -= _prefabHeight;
            transparency -= 30;
        }
    }

    /// <summary>
    /// Updates the coins in the indicator panel on the top of the shop once the player buys an item or opens the shop
    /// </summary>
    public void UpdateCoins() {
        CoinsIndicator.text = _manny.Attribute.GetAttribute(Attribute.Coins).ToString();
    }

    /// <summary>
    /// This method makes sure that the user cannot scroll outside of the content of the panel
    /// </summary>
    public void OnValueChanged() {
        var scrollView = transform.parent.parent;
        var capacity = Math.Floor(scrollView.GetComponent<RectTransform>().rect.height / _prefabHeight);
        var scrollRect = GetComponent<RectTransform>();

        if (capacity < Items.Length) {
            var maxY = (float)((Items.Length - capacity) * _prefabHeight) - 75f;
            if (scrollRect.anchoredPosition.y < 0) scrollRect.anchoredPosition = new Vector2();
            if (scrollRect.anchoredPosition.y > maxY) scrollRect.anchoredPosition = new Vector2(0, maxY);
        } else {
            scrollRect.anchoredPosition = new Vector2();
        }
    }
}
