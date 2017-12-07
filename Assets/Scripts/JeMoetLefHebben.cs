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

        //get image
        var imageData = _sprite.texture.GetRawTextureData();
        var data = new List<IMultipartFormSection> {
            new MultipartFormDataSection("foo", "bar"),
            new MultipartFormFileSection("myImage", imageData, "test.png", "image/png")
        };

        //init handshake
        var handshake = UnityWebRequest.Post("http://mypage/uload.php", data);
        var request = handshake.SendWebRequest();


        //response
        request.completed += (action) => {
            if (!handshake.isHttpError && !handshake.isNetworkError) {
                Debug.Log(handshake.downloadHandler.text);
            }
        };
    }
}
