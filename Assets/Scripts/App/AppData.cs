using Assets.Scripts.App.Game;
using Assets.Scripts.App.Tracking.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.App {
    public class AppData {

        private static AppData _instance = null;
        public GameManager Game { get; private set; }
        public DataTableRegistry Registry { get; private set; }

        private AppData() {
            Game = new GameManager();
            Registry = new DataTableRegistry();
        }

        public static AppData Instance() {
            if (_instance == null)
                _instance = new AppData();
            return _instance;
        }
    }
}
