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
	void Start () {
        if (_sprite == null) return;

        var texture = _sprite.texture;
        var file = new HandshakeFile("test.png", texture.GetRawTextureData(), "image/png");
        new Handshake(HandshakeProtocol.Upload).AddFile("file", file).Shake((request) => {
            Debug.Log(request.downloadHandler.text);
        });
	}
	
	// Update is called once per frame
	void Update () {

    }
}
