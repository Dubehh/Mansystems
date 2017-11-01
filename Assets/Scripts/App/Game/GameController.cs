using Assets.Scripts.App.Tracking.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.App.Game {
    public abstract class GameController : MonoBehaviour{

        public DataTable DataSource { get; private set; }

        protected abstract void BeforeLoad();
        protected abstract void OnLoad();
        protected abstract void Update();

        public void Build() {
            if (DataSource != null)
                DataSource.Create();
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
