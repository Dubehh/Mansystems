using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MinigamePrefab : MonoBehaviour {

    public Minigame Minigame { get; set; }

    [SerializeField]
    public RawImage Icon;

    [SerializeField]
    public Text Title;

    [SerializeField]
    public Text Description;

    /// <summary>
    /// Fill the prefab with the information from the minigame
    /// </summary>
    public void Init() {
        Icon.texture = Minigame.Icon;
        Title.text = Minigame.Title;
        Description.text = Minigame.Description;
    }

    /// <summary>
    /// OnClick event for the minigame's buy button
    /// </summary>
    public void OnClick() {
        if(Minigame.LandscapeMode)
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        Minigame.StartGame();
    }
}
