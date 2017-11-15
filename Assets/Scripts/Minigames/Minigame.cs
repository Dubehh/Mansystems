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

    /// <summary>
    /// Method that is called by the onclick event of the minigame's button
    /// </summary>
    public void StartGame() {
        SceneManager.LoadScene("Title");
    }
}
