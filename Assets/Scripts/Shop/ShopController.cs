using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopController : MonoBehaviour {

    private const float _yStart = 373f;
    private const byte _transparencyStart = 255;
    private RectTransform _scrollRect;

    [SerializeField]
    public ShopItem[] Items;

    [SerializeField]
    public GameObject Item;

    // Use this for initialization
    void Start() {
        _scrollRect = GetComponent<RectTransform>();

        var y = _yStart;
        var transparency = _transparencyStart;

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
    /// This method makes sure that the user cannot scroll outside of the content of the panel
    /// </summary>
    /// <param name="vec">The 2D vector of the current location of the scroll content</param>
    public void OnValueChanged(Vector2 vec) {
        var maxY = 455;
        if (vec.y > 0) _scrollRect.anchoredPosition = new Vector2(0, 0);
        if (_scrollRect.anchoredPosition.y > maxY) _scrollRect.anchoredPosition = new Vector2(0, maxY);
    }
}
