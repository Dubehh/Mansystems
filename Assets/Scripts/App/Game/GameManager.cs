using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.App.Game {
    public class GameManager {

        private const string 
            _scenes = "Assets/Scenes",
            _main   = "0",
            _folder = "Minigames";

        private HashSet<string> _sceneRegister;
        private GameController _current;

        public GameManager() {
            _current = null;
            _sceneRegister = new HashSet<string>();
            Initialize();
        }

        /// <summary>
        /// Initializes the gamemanager and registers all minigames
        /// using reflection
        /// </summary>
        private void Initialize() {
            var dir = new DirectoryInfo(_scenes+"/"+_folder);
            var scenes = dir.GetFiles("*.unity").Select(f=>f.Name.Split('.')[0]);
            foreach (var scene in scenes)
                _sceneRegister.Add(scene);
        }

        /// <summary>
        /// Loads the scene with the given name
        /// </summary>
        /// <param name="scene">the name of the scene</param>
        public void Load(string scene) {
            var foundScene = _sceneRegister.Where(s => s.ToLower() == scene.ToLower()).FirstOrDefault();
            if(foundScene != null)
                SceneManager.LoadScene(Int32.Parse(foundScene));
        }

        /// <summary>
        /// Unloads the current minigame if there is a current one
        /// </summary>
        public void Unload() {
            if (_current == null) return;
            _current.OnUnload();
            SceneManager.LoadSceneAsync(0);
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
