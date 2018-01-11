using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopController : MonoBehaviour {

    private Manny _manny;
    private float _prefabHeight;

    [SerializeField] public Sprite BtnFood;
    [SerializeField] public Sprite BtnDrink;
    [SerializeField] public Text CoinsIndicator;
    [SerializeField] public GameObject Item;
    [SerializeField] public ShopItem[] Items;

    // Use this for initialization
    private void Start() {
        var y = -75f;

        _manny = FindObjectOfType<Manny>();
        UpdateCoins();

        // Loop through the ShopItem array and create a ShopItemPrefab with each one of them
        foreach (var item in Items) {
            var obj = Instantiate(Item, transform).GetComponent<ShopItemPrefab>();
            obj.Item = item;
            obj.GetComponentInChildren<Button>().GetComponent<Image>().sprite = item.Attribute == Attribute.Food ? BtnFood : BtnDrink;
            obj.Init();

            var rect = obj.GetComponent<RectTransform>().rect;

            _prefabHeight = rect.height;

            obj.transform.localPosition = new Vector2(rect.width / 2, y);
            y -= _prefabHeight;
        }
    }

    /// <summary>
    ///     Updates the coins in the indicator panel on the top of the shop once the player buys an item or opens the shop
    /// </summary>
    public void UpdateCoins() {
        CoinsIndicator.text = _manny.Attribute.GetAttribute(Attribute.Coins).ToString();
    }

    /// <summary>
    ///     This method makes sure that the user cannot scroll outside of the content of the panel
    /// </summary>
    public void OnValueChanged() {
        var scrollView = transform.parent.parent;
        var capacity = Math.Floor(scrollView.GetComponent<RectTransform>().rect.height / _prefabHeight);
        var scrollRect = GetComponent<RectTransform>();

        if (capacity < Items.Length) {
            var maxY = (float)((Items.Length - capacity) * _prefabHeight) - 50f;
            if (scrollRect.anchoredPosition.y < 0) scrollRect.anchoredPosition = new Vector2();
            if (scrollRect.anchoredPosition.y > maxY) scrollRect.anchoredPosition = new Vector2(0, maxY);
        } else {
            scrollRect.anchoredPosition = new Vector2();
        }
    }
}