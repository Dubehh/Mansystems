using System.Linq;
using Assets.Scripts.App.Data_Management.Handshakes;
using UnityEngine;
using UnityEngine.UI;

public class AddPicture : MonoBehaviour {
    
    [SerializeField]
    private RawImage _camera;

    private WebCamTexture _cameraTexture;
    private Texture2D _picture;

    // Use this for initialization
    private void Start () {
        var device = WebCamTexture.devices.First(x => x.isFrontFacing);
        _cameraTexture = new WebCamTexture(device.name, 550, 550);
        _cameraTexture.Play();
        _camera.texture = _cameraTexture;
    }
	
	// Update is called once per frame
	private void Update () {
	    var ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
	    GetComponentInChildren<AspectRatioFitter>().aspectRatio = ratio;

	    var scaleY = _cameraTexture.videoVerticallyMirrored ? -1f : 1f;
	    _camera.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

	    var orientation = -_cameraTexture.videoRotationAngle;
	    _camera.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    public void UploadPicture() {
        new FileProtocol(Protocol.Upload, this).Put("image", "profilePicture.jpeg", ContentType.Jpeg, _picture.EncodeToJPG()).Send(
            www => {
                Debug.Log(www.text);
            });
    }

    public void TakePicture() {
        Color[] data = _cameraTexture.GetPixels();
        _picture = new Texture2D(_cameraTexture.width, _cameraTexture.height);
        _picture.SetPixels(data);
        _cameraTexture.Stop();
    }
}
