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
            _main   = "GameManagementTesting",
            _folder = "Minigames";

        private HashSet<string> _sceneRegister;
        private GameController _current;

        public GameManager() {
            _current = null;
            _sceneRegister = new HashSet<string>();
            Initialize();
        }

        private void Initialize() {
            var dir = new DirectoryInfo(_scenes+"/"+_folder);
            var scenes = dir.GetFiles("*.unity").Select(f=>f.Name.Split('.')[0]);
            foreach (var scene in scenes)
                _sceneRegister.Add(scene);
        }

        public void Load(string scene) {
            var foundScene = _sceneRegister.Where(s => s.ToLower() == scene.ToLower()).FirstOrDefault();
            if(foundScene != null)
                SceneManager.LoadScene(foundScene);
        }

        public void Unload() {
            if (_current == null) return;
            _current.OnUnload();
            SceneManager.LoadScene(_main);
            //TODO add loading thing somewhere
        }

        public void Inform(GameController controller) {
            _current = controller;
        }
    }
}
