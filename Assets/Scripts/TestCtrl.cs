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
            StartCoroutine(Save());
        }

        private System.Collections.IEnumerator Save() {
            yield return new WaitForSeconds(2f);
            Debug.Log("Testing first handshake...");
            new Handshake(HandshakeProtocol.Response).AddParameter("moduleReference", "millionaireHandler").Shake((request) => {
                if (request.isHttpError || request.isNetworkError) {
                    Debug.Log("Error");
                } else {
                    var a = new JSONObject(request.downloadHandler.text);
                    Debug.Log("Handshake successful, response: " + a["a"]["c"].i);
                }
            });

            yield return new WaitForSeconds(2f);
            Debug.Log("Adding some data to the database..");
            DataSource.Insert(DataParams.Build("Won", 1).Append("Enemies_Killed", 8));
            DataSource.Insert(DataParams.Build("Won", 1).Append("Enemies_Killed", 3));
            DataSource.Insert(DataParams.Build("Won", 0).Append("Enemies_Killed", 4));
            yield return new WaitForSeconds(2f);

            Debug.Log("Starting to save tracking in 5 seconds..");
            yield return new WaitForSeconds(5f);
            Debug.Log("Tracking request has beent sent...");
            Tracking.RequestSend();
        }

        protected override void Update() {}
    }
}

