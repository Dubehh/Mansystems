using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.App.Game {
    public class GameManager {

        private const string _main = "0";
        private GameController _current;

        public GameManager() {
            _current = null;
        }

        /// <summary>
        /// Loads the scene with the given name
        /// </summary>
        /// <param name="scene">the name of the scene</param>
        public void Load(string scene) {
            SceneManager.LoadScene(scene.ToLower()+"_minigame");
        }

        /// <summary>
        /// Unloads the current minigame if there is a current one
        /// </summary>
        public void Unload() {
            if (_current == null) return;
            _current.Tracking.RequestSend();
            _current.OnUnload();
            SceneManager.LoadScene(_main);
            //TODO add loading thing somewhere
        }

        /// <summary>
        /// Used to ping back and sets the current controller to the given one
        /// </summary>
        /// <param name="controller">GameController current</param>
        public void Inform(GameController controller) {
            _current = controller;
        }
    }
}
