using Assets.Scripts.App;
using Assets.Scripts.App.Data_Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTestCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Starting test...");
        //StartCoroutine(Test());
        var a = new Handshake(HandshakeProtocol.Response).AddParameter("responseHandler", "millionaire");
        a.SetErrorHandler(()=> {
            Debug.Log("Error");
        });
        a.Shake((response) => {
            var text = response.downloadHandler.text;
            var obj = new JSONObject(text);
            Debug.Log(obj["eelco"].str);
        });

	}

    private IEnumerator Test() {
        Debug.Log("Loading test scene in 5 seconds..");
        yield return new WaitForSeconds(5);
        AppData.Instance().Game.Load("3");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
