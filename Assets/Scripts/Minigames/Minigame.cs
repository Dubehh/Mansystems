using Assets.Scripts.App;
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

    /// <summary>
    /// Method that is called by the onclick event of the minigame's button
    /// </summary>
    public void StartGame() {
        AppData.Instance().Game.Load(Scene);
    }
}
