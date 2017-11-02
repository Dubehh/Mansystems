using Assets.Scripts.App.Game;
using Assets.Scripts.App.Tracking.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.App {
    /// <summary>
    /// Singleton class used to ensure single-initialization of instances.
    /// </summary>
    public class AppData {

        private static AppData _instance = null;
        public GameManager Game { get; private set; }
        public DataTableRegistry Registry { get; private set; }

        private AppData() {
            Game = new GameManager();
            Registry = new DataTableRegistry();
        }

        /// <summary>
        /// Returns the instance of the AppData class
        /// </summary>
        /// <returns>AppData instance</returns>
        public static AppData Instance() {
            if (_instance == null)
                _instance = new AppData();
            return _instance;
        }
    }
}
