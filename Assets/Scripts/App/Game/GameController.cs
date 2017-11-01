using Assets.Scripts.App.Tracking.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.App.Game {
    public abstract class GameController : MonoBehaviour{

        protected DataTable _table;

        protected abstract void BeforeLoad();
        protected abstract void OnLoad();
        protected abstract void Update();

        public void Initialize() {

        }

        protected void SetDataSource(DataTable table) {
            _table = table;
        }

        private void Start() {
            BeforeLoad();
            OnLoad();
        }

    }
}
