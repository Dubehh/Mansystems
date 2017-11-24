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

    [SerializeField]
    public RawImage Connection;

    [SerializeField]
    public Texture NoConnectionIcon;

    private bool _canAccess;
    /// <summary>
    /// Fill the prefab with the information from the minigame
    /// </summary>
    public void Init(bool hasConnection) {
        Icon.texture = Minigame.Icon;
        Title.text = Minigame.Title;
        Description.text = Minigame.Description;
        if (!Minigame.RequiresConnection) Connection.gameObject.SetActive(false);
        else if (hasConnection == false) Connection.texture = NoConnectionIcon;
        _canAccess = hasConnection;
    }

    /// <summary>
    /// OnClick event for the minigame's buy button
    /// </summary>
    public void OnClick() {
        if (Minigame.RequiresConnection && !_canAccess) return;
        if(Minigame.LandscapeMode)
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        Minigame.StartGame();
    }
}
