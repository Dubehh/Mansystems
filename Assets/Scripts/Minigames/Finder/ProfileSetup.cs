using System;
using System.IO.Ports;
using System.Linq;
using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Tracking.Table;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSetup : MonoBehaviour {

    [SerializeField] private InputField _nameInput;
    [SerializeField] private InputField _descriptionInput;

    [SerializeField]
    private GameObject[] _steps;
    private int _currentStepIndex;

    [SerializeField]
    private RawImage _camera;

    private WebCamTexture _cameraTexture;
    private Texture2D _picture;
    private DataTable _finderProfile;

    private void Awake() {
        _finderProfile = new DataTable("FinderProfile");
        _finderProfile.Drop();
        _finderProfile.AddProperty(new DataProperty("Name", DataProperty.DataPropertyType.VARCHAR));
        _finderProfile.AddProperty(new DataProperty("Description", DataProperty.DataPropertyType.INT));
        AppData.Instance().Registry.Register(_finderProfile);

        if (_finderProfile.Exists("name"))
            gameObject.SetActive(false);
    }

    private void Update() {
        if (_currentStepIndex == 2 && _cameraTexture != null) {
            var ratio = (float) _cameraTexture.width / (float) _cameraTexture.height;
            GetComponentInChildren<AspectRatioFitter>().aspectRatio = ratio;

            var scaleY = _cameraTexture.videoVerticallyMirrored ? -1f : 1f;
            _camera.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            var orientation = -_cameraTexture.videoRotationAngle;
            _camera.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
        }
    }

    /// <summary>
    /// Disables the current panel and activates the next panel from the _steps list
    /// </summary>
    public void NextStep() {
        switch (_currentStepIndex) {
            case 2:
                //TODO: Check if JPEG or PNG
                new FileProtocol(Protocol.Upload, this).Put("image", "profilePicture.png", ContentType.Png, _picture.EncodeToPNG()).Send(
                    www => {
                        Debug.Log(www.text);
                    });
                break;
            case 3:
                gameObject.SetActive(false);

                _finderProfile.Insert(DataParams.
                    Build("Name", _nameInput.text).
                    Append("Description", _descriptionInput.text));

                var handshake = new InformationProtocol(Protocol.Insert)
                    .AddParameter("targetTable", "module_finder")
                    .AddParameter("uid", PlayerPrefs.GetString("uid"))
                    .AddParameter("Name", _nameInput.text).
                    AddParameter("Description", _descriptionInput.text);

                handshake.Send();
                return;
        }

        _steps[_currentStepIndex].SetActive(false);
        _currentStepIndex += 1;
        _steps[_currentStepIndex].SetActive(true);
    }

    public void AddPicture(bool useCamera) {
        if (useCamera) {
            var device = WebCamTexture.devices.First(x => x.isFrontFacing);
            _cameraTexture = new WebCamTexture(device.name, 550, 550);
            _cameraTexture.Play();
            _camera.texture = _cameraTexture;
        } else {
            
        }

    }

    public void TakePicture() {
        Color[] data = _cameraTexture.GetPixels();
        _picture = new Texture2D(_cameraTexture.width, _cameraTexture.height);
        _picture.SetPixels(data);
        _cameraTexture.Stop();
    }
}
