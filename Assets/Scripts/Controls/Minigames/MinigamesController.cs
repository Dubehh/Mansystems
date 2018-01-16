using System;
using Assets.Scripts.App.Data_Management.Handshakes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controls.Minigames {
    public class MinigamesController : MonoBehaviour {
        private float _prefabHeight;

        [SerializeField] public GameObject Loading;
        [SerializeField] public GameObject Minigame;
        [SerializeField] public Minigame[] Minigames;

        private void Awake() {
            Loading.SetActive(true);
            InformationProtocol.Validate(() => { Initialize(true); }, () => { Initialize(false); });
        }

        // Use this for initialization
        private void Initialize(bool hasConnection) {
            var y = -75f;
            Loading.SetActive(false);
            // Loop through the Minigame array and create a MinigamePrefab with each one of them
            foreach (var game in Minigames) {
                var obj = Instantiate(Minigame, transform).GetComponent<MinigamePrefab>();
                obj.Minigame = game;
                obj.Init(hasConnection);
                if (game.RequiresConnection && !hasConnection)
                    obj.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);

                var rect = obj.GetComponent<RectTransform>().rect;

                _prefabHeight = rect.height;

                obj.transform.localPosition = new Vector2(rect.width / 2, y);
                y -= _prefabHeight;
            }
        }

        /// <summary>
        ///     This method makes sure that the user cannot scroll outside of the content of the panel
        /// </summary>
        public void OnValueChanged() {
            var scrollView = transform.parent.parent;
            var capacity = Math.Floor(scrollView.GetComponent<RectTransform>().rect.height / _prefabHeight);
            var scrollRect = GetComponent<RectTransform>();

            if (capacity < Minigames.Length) {
                var maxY = (float)((Minigames.Length - capacity) * _prefabHeight) - 15f;
                if (scrollRect.anchoredPosition.y < 0) scrollRect.anchoredPosition = new Vector2();
                else if (scrollRect.anchoredPosition.y > maxY) scrollRect.anchoredPosition = new Vector2(0, maxY);
            } else
                scrollRect.anchoredPosition = new Vector2();
        }
    }
}