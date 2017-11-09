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
            var table = new DataTable("temp");
            table.AddProperty(new DataProperty("value_integer", DataProperty.DataPropertyType.INT));
            table.AddProperty(new DataProperty("value_string", DataProperty.DataPropertyType.VARCHAR, 100));
            SetDataSource(table);
        }

        protected override void OnLoad() {
            DataSource.Insert(DataParams.Build("value_integer", 13).Append("value_string", "abc"));
            base.Tracking.RequestSend();
        }

        protected override void Update() {
            
        }
    }
}
