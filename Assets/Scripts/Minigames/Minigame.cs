using System;
using UnityEngine;

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
        // feature/Minigames/Transition
    }
}
