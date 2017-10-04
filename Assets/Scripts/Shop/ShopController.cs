using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopController : MonoBehaviour {

    private const float _yStart = 315f;
    private const byte _transparencyStart = 255;

    [SerializeField]
    public ShopItem[] Items;

    [SerializeField]
    public GameObject Item;

    private RectTransform ScrollRect;

    // Use this for initialization
    void Start() {
        ScrollRect = GetComponent<RectTransform>();

        var y = _yStart;
        var transparency = _transparencyStart;
        foreach (var item in Items) {
            if (transparency < 140) transparency = _transparencyStart;

            var obj = Instantiate(Item, transform).GetComponent<ShopItemPrefab>();
            obj.Item = item;
            obj.Init();

            obj.GetComponent<Image>().color = item.Attribute == Attribute.Food 
                ? new Color32(18, 178, 112, transparency)
                : new Color32(26, 118, 175, transparency);

            obj.transform.localPosition = new Vector2(0, y);
            y -= obj.GetComponent<RectTransform>().rect.height - 2;
            transparency -= 30;
        }
    }

    public void OnValueChanged(Vector2 vec) {
        var maxY = 540;
        if (vec.y > 0) ScrollRect.anchoredPosition = new Vector2(0, 0);
        if (ScrollRect.anchoredPosition.y > maxY) ScrollRect.anchoredPosition = new Vector2(0, maxY);
    }
}
