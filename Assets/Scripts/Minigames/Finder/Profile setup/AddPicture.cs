using System.Linq;
using Assets.Scripts.App.Data_Management.Handshakes;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class AddPicture : MonoBehaviour {
    private WebCamTexture _cameraTexture;
    private Texture2D _picture;

    [SerializeField] public RawImage Camera;

    // Use this for initialization
    private void Awake() {
        var device = WebCamTexture.devices.First(x => x.isFrontFacing);
        _cameraTexture = new WebCamTexture(device.name, 550, 550);
        PlayCamera();
        Camera.texture = _cameraTexture;
    }

    public void PlayCamera() {
        _cameraTexture.Play();
    }

    // Update is called once per frame
    private void Update() {
        var ratio = _cameraTexture.width / (float) _cameraTexture.height;
        GetComponentInChildren<AspectRatioFitter>().aspectRatio = ratio;

        var scaleY = _cameraTexture.videoVerticallyMirrored ? -1f : 1f;
        Camera.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        var orientation = -_cameraTexture.videoRotationAngle;
        Camera.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    /// <summary>
    ///     Function that uploads the taken picture to the webserver
    /// </summary>
    public void UploadPicture([CanBeNull] FinderController controller) {
        var fp = new FileProtocol(Protocol.Upload, this);
        fp.AddParameter("targetFolder", "finder");
        fp.Put("file", "profilePicture.jpeg", ContentType.Jpeg, _picture.EncodeToJPG()).Send(www => {
            if (controller != null) {
                controller.FinderProfileController.PersonalProfile.ImageNames.Add(www.text);
                FindObjectOfType<ProfileManagement>().OpenView();
            }
        });
    }

    /// <summary>
    /// Event for uploading a picture without adding the picture's name to the personal profile
    /// </summary>
    public void UploadPicture() {
        UploadPicture(null);
    }

    /// <summary>
    ///     Stops the camera and saves the picture into a texture
    /// </summary>
    public void TakePicture() {
        var data = _cameraTexture.GetPixels();
        _picture = new Texture2D(_cameraTexture.width, _cameraTexture.height);
        _picture.SetPixels(data);
        _cameraTexture.Stop();
    }
}