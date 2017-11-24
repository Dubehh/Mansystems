using Assets.Scripts.App.Data_Management;
using Assets.Scripts.App.Game;
using Assets.Scripts.App.Tracking.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    public class TestCtrl : GameController{
        public override void OnUnload() {
            
        }

        protected override void BeforeLoad() {
            Debug.Log("Now setting up datatable...");
            
            var table = new DataTable("TestGame");
            table.AddProperty(new DataProperty("Won", DataProperty.DataPropertyType.INT));
            table.AddProperty(new DataProperty("Enemies_Killed", DataProperty.DataPropertyType.INT));
            SetDataSource(table);

            Debug.Log("Database setup done!");

        }

        protected override void OnLoad() {
            
        }

        protected override void Update() {}
    }
}

