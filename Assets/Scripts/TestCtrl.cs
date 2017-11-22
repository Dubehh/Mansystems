using Assets.Scripts.App.Data_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    public class TestCtrl : MonoBehaviour{

        private void Start() {
            new Handshake(HandshakeProtocol.Response).AddParameter("a", "b").Shake((request) => {
                if (request.isHttpError || request.isNetworkError) {
                    Debug.Log("Error");
                } else {
                    var a = new JSONObject(request.downloadHandler.text);
                    Debug.Log(a["a"]["c"].i);
                }
            });
        }
    }
}

