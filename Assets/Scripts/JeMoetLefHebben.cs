using Assets.Scripts.App.Data_Management;
using Assets.Scripts.App.Data_Management.Handshakes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JeMoetLefHebben : MonoBehaviour {

    [SerializeField]
    private Sprite _sprite;

    // Use this for initialization
    private void Start() {
        if (_sprite == null) return;

        new FileProtocol(Protocol.Upload, this).Put("file", "eelco.png", ContentType.Png, _sprite.texture.EncodeToPNG())
            .Send(
                (response) => {
                    Debug.Log(response.text);
                });
    }



    // Update is called once per frame
    private void Update() {

    }
}
