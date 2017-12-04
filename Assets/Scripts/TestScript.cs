using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.App.Data_Management;
using UnityEngine;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	private void Start () {
	    StartCoroutine(Delayed());
	}

    internal IEnumerator Delayed() {
        yield return new WaitForSeconds(1);

        new Handshake(HandshakeProtocol.ImageUpload)
            .AddParameter("player", "eelco")
            //.AddFile("profile", "screenshot.png", )
            .Shake((response) => {
                Debug.Log(response.downloadHandler.text);
            });
    }

}
