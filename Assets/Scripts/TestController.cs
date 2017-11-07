using Assets.Scripts.App.Game;
using Assets.Scripts.App.Tracking.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    public class TestController : GameController {

        public override void OnUnload() {
            
        }

        protected override void BeforeLoad() {
            var table = new DataTable("test");
            table.AddProperty(new DataProperty("id", DataProperty.DataPropertyType.INT));
            table.AddProperty(new DataProperty("name", DataProperty.DataPropertyType.VARCHAR, 100));
            SetDataSource(table);
        }

        protected override void OnLoad() {
            DataSource.Insert(DataParams.Build("id", 1).Append("name", "James"));
            base.Tracking.RequestSend();
        }

        protected override void Update() {
            
        }
    }
}
