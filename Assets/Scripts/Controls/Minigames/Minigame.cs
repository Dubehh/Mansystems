using System;
using Assets.Scripts.App;
using UnityEngine;

namespace Assets.Scripts.Controls.Minigames {
    [Serializable]
    public class Minigame {
        [SerializeField] public string Description;
        [SerializeField] public Texture Icon;
        [SerializeField] public bool LandscapeMode;
        [SerializeField] public bool RequiresConnection;
        [SerializeField] public string Scene;
        [SerializeField] public string Title;

        /// <summary>
        ///     Method that is called by the onclick event of the minigame's button
        /// </summary>
        public void StartGame() {
            AppData.Instance().Game.Load(Scene);
        }
    }
}