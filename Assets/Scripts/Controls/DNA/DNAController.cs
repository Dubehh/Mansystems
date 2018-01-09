using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DNAController : MonoBehaviour {

    [SerializeField] public GameObject CompletionScreen;
    [SerializeField] public GameObject MoreInfo;
    [SerializeField] public Text DisplayText;
    [SerializeField] public DNAItem[] Items;
    [SerializeField] public Manny Manny;
    [SerializeField] public ParticleSystem ParticleSystem;

    private void Start() {
        DisplayText.text = Manny.Attribute.GetAttribute(Attribute.Skillpoints) + "";
        foreach (var item in Items) {
            item.SetInstance(Manny);
            item.Update();
        }
    }

    private void ValidateCompletion() {
        if (Items.Any(item => Manny.Attribute.GetAttribute(item.Attribute) < 5))
            return;
        CompletionScreen.SetActive(true);
        
    }

    private void Update() {
        if (Input.touchCount <= 0) return;
        if(MoreInfo.activeSelf) MoreInfo.SetActive(false);
        if(CompletionScreen.activeSelf) CompletionScreen.SetActive(false);
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
        var item = Items.FirstOrDefault(x => x.Slider == slider);
        if (item == null) return;
        if (Manny.Attribute.GetAttribute(Attribute.Skillpoints) <= 0 || slider.value >= 5) return;
        Manny.Attribute.IncrementAttribute(Attribute.Skillpoints, -1);
        Manny.Attribute.IncrementAttribute(item.Attribute, 1);
        item.Update();
        ValidateCompletion();
        ParticleSystem.Play();
        DisplayText.text = Manny.Attribute.GetAttribute(Attribute.Skillpoints) + "";
    }
}