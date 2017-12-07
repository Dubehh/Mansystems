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

    private Texture2D _profilePicture;

    private DataTable _finderProfile;
    
    private void Awake() {
        _finderProfile = new DataTable("FinderProfile");

        if (_finderProfile.Exists("name"))
            gameObject.SetActive(false);

        _finderProfile.AddProperty(new DataProperty("Name", DataProperty.DataPropertyType.VARCHAR));
        _finderProfile.AddProperty(new DataProperty("Description", DataProperty.DataPropertyType.INT));
        AppData.Instance().Registry.Register(_finderProfile);
    }

    /// <summary>
    /// Disables the current panel and activates the next panel from the _steps list
    /// </summary>
    public void NextStep() {
        switch (_currentStepIndex) {
            case 2:
                //TODO: Check if JPEG or PNG
                new FileProtocol(Protocol.Upload, this).Put("image", "profilePicture.png", ContentType.Png, _profilePicture.EncodeToPNG()).Send(
                    www => {
                        Debug.Log(www.text);
                    });
                break;
            case 3:
                gameObject.SetActive(false);

                _finderProfile.Insert(DataParams.
                    Build("Name", _nameInput.text).
                    Append("Description", _descriptionInput.text));
                return;
        }

        _steps[_currentStepIndex].SetActive(false);
        _currentStepIndex += 1;
        _steps[_currentStepIndex].SetActive(true);
    }


}
