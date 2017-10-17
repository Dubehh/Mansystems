using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MinigamesController : MonoBehaviour {

    private float _prefabHeight;

    [SerializeField]
    public Minigame[] Minigames;

    [SerializeField]
    public GameObject Minigame;

    // Use this for initialization
    void Start() {
        var y = -75f;

        // Loop through the Minigame array and create a MinigamePrefab with each one of them
        foreach (var game in Minigames) {
            var obj = Instantiate(Minigame, transform).GetComponent<MinigamePrefab>();
            obj.Minigame = game;
            obj.Init();

            var rect = obj.GetComponent<RectTransform>().rect;

            _prefabHeight = rect.height;

            obj.transform.localPosition = new Vector2(rect.width / 2, y);
            y -= _prefabHeight;
        }
    }

    /// <summary>
    /// This method makes sure that the user cannot scroll outside of the content of the panel
    /// </summary>
    public void OnValueChanged() {
        var scrollView = transform.parent.parent;
        var capacity = Math.Floor(scrollView.GetComponent<RectTransform>().rect.height / _prefabHeight);
        var _scrollRect = GetComponent<RectTransform>();

        if (capacity < Minigames.Length) {
            var maxY = (float)((Minigames.Length - capacity) * _prefabHeight) - 70f;
            if (_scrollRect.anchoredPosition.y < 0) _scrollRect.anchoredPosition = new Vector2();
            if (_scrollRect.anchoredPosition.y > maxY) _scrollRect.anchoredPosition = new Vector2(0, maxY);
        } else {
            _scrollRect.anchoredPosition = new Vector2();
        }
    }
}
