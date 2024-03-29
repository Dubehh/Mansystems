﻿using Assets.Scripts.App.Data_Management.Table;
using Assets.Scripts.App.Data_Management.Tracking;
using UnityEngine;

namespace Assets.Scripts.App.Game {
    public abstract class GameController : MonoBehaviour {
        public TrackingController Tracking { get; private set; }
        public AppData App { get; private set; }
        public DataTable DataSource { get; private set; }

        /// <summary>
        ///     Called before the rest of the game is loaded.
        ///     This method is for initialization purposes
        /// </summary>
        protected abstract void BeforeLoad();

        /// <summary>
        ///     Called when the game loads and all initialization is done.
        /// </summary>
        protected abstract void OnLoad();

        /// <summary>
        ///     Called when the game is unloading
        ///     this method should be used for cleaning up and saving
        /// </summary>
        public abstract void OnUnload();

        /// <summary>
        ///     Called each tick
        /// </summary>
        protected abstract void Update();

        /// <summary>
        ///     Registers the datatable and pings the gamemanager
        /// </summary>
        private void Build() {
            App = AppData.Instance();
            if (App == null) return;
            if (DataSource != null) {
                App.Registry.Register(DataSource);
                Tracking = new TrackingController(DataSource);
            }
            App.Game.Inform(this);
        }

        /// <summary>
        ///     Sets the datasource for this controller to the given table
        /// </summary>
        /// <param name="table">DataTable table</param>
        protected void SetDataSource(DataTable table) {
            DataSource = table;
        }

        private void Start() {
            BeforeLoad();
            Build();
            //show loading screen
        }

        /// <summary>
        ///     Prepares the game itself.
        ///     Should be called upon init complete
        /// </summary>
        protected void Prepare() {
            OnLoad();
            //hide loading screen
        }
    }
}