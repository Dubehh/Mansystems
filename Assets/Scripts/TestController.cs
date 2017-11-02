using Assets.Scripts.App.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    public class TestController : GameController {
        public override void OnUnload() {
            Debug.Log("Game Unloaded");
        }

        protected override void BeforeLoad() {
            Debug.Log("Preloading important stuff");
        }

        protected override void OnLoad() {
            Debug.Log("Game fully loaded and ready for play");
        }

        protected override void Update() {
            if (Input.GetKeyDown(KeyCode.A))
                App.Game.Unload();
        }
    }
}
