using System.Linq;
using Assets.Scripts.Manny;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Controls.DNA {
    public class DNAController : MonoBehaviour {
        [SerializeField] public Text DisplayText;

        [SerializeField] public DNAItem[] Items;

        [SerializeField] public Manny.Manny Manny;
        [SerializeField] public GameObject MoreInfo;

        [SerializeField] public ParticleSystem ParticleSystem;

        private void Start() {
            DisplayText.text = Manny.Attribute.GetAttribute(Attribute.Skillpoints) + "";
            foreach (var item in Items) {
                item.SetInstance(Manny);
                item.Update();
            }
        }

        private void Update() {
            if (MoreInfo.activeSelf && Input.touchCount > 0) MoreInfo.SetActive(false);
        }

        public void OnClickNavigation() {
            MoreInfo.SetActive(true);
        }

        /// <summary>
        ///     Fires when the button next to the DNAItem sliders are being clicked
        /// </summary>
        public void OnClickButton() {
            var obj = EventSystem.current.currentSelectedGameObject;
            var slider = obj.GetComponentInParent<Slider>();
            var item = Items.Where(x => x.Slider == slider).FirstOrDefault();
            if (item != null) {
                if (Manny.Attribute.GetAttribute(Attribute.Skillpoints) <= 0 || slider.value >= 5) return;
                Manny.Attribute.IncrementAttribute(Attribute.Skillpoints, -1);
                Manny.Attribute.IncrementAttribute(item.Attribute, 1);
                item.Update();
                ParticleSystem.Play();
                DisplayText.text = Manny.Attribute.GetAttribute(Attribute.Skillpoints) + "";
            }
        }
    }
}