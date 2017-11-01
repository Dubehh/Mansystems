using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.App.Game {
    public class GameManager {

        private const string _scenes = "Assets/Scenes/Minigames";
        private HashSet<string> _sceneRegister;

        public GameManager() {
            _sceneRegister = new HashSet<string>();
            Initialize();
        }

        private void Initialize() {
            var dir = new DirectoryInfo(_scenes);
            var files = dir.GetFiles("*.unity").Select(f=>f.Name);
            foreach (var file in files)
                Debug.Log(file);
            
        }
    }
}
