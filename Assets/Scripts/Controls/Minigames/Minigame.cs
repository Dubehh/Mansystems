using Assets.Scripts.App;
using Assets.Scripts.App.Data_Management;
using Assets.Scripts.App.Game;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Minigame {

    [SerializeField]
    public Texture Icon;

    [SerializeField]
    public string Title;

    [SerializeField]
    public string Description;

    [SerializeField]
    public string Scene;

    [SerializeField]
    public bool LandscapeMode;

    [SerializeField]
    public bool RequiresConnection;

    /// <summary>
    /// Method that is called by the onclick event of the minigame's button
    /// </summary>
    public void StartGame() {
        if (RequiresConnection)
            Handshake.Validate(() => {
                AppData.Instance().Game.Load(Scene);
            });
        else
            AppData.Instance().Game.Load(Scene);
    }
}
