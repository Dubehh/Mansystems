using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopController : MonoBehaviour {

    private const float yStart = 315f;

    [SerializeField]
    public ShopItem[] Items;

    [SerializeField]
    public GameObject Item;

    // Use this for initialization
    void Start() {
        var y = yStart;
        byte transparency = 255;
        foreach (var item in Items) {
            if (transparency < 140) transparency = 255;

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
}
