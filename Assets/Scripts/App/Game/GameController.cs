using Assets.Scripts.App.Tracking.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.App.Game {
    public abstract class GameController : MonoBehaviour{

        public AppData App { get; private set; }
        public DataTable DataSource { get; private set; }

        protected abstract void BeforeLoad();
        protected abstract void OnLoad();
        public abstract void OnUnload();
        protected abstract void Update();

        private void Build() {
            App = AppData.Instance();
            if (App == null) 
                return;
            else if (DataSource != null)
                App.Registry.Register(DataSource);
            App.Game.Inform(this);
        }

        protected void SetDataSource(DataTable table) {
            DataSource = table;
        }

        private void Start() {
            BeforeLoad();
            Build();
            OnLoad();
        }
    }
}
