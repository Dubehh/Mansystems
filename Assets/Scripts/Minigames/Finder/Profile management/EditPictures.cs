using UnityEngine;
using UnityEngine.UI;

public class EditPictures : MonoBehaviour {

    [SerializeField] public RawImage Picture;
    private FinderProfile _personalProfile;

    private void Awake() {
        var finderController = FindObjectOfType<FinderController>().FinderProfileController;
        _personalProfile = finderController.PersonalProfile;
    }

    /// <summary>
    /// Loads all the profile's pictures
    /// </summary>
    public void Init() {
        _personalProfile.LoadPictures(this, queue => {
            Picture.texture = _personalProfile.GetCurrentPicture();
        });
    }

    /// <summary>
    ///     OnClick event for the picture changing buttons
    /// </summary>
    /// <param name="next"></param>
    public void SwitchPicture(bool next) {
        Picture.texture = _personalProfile.GetPicture(next);
    }

    /// <summary>
    /// OnClick event for removing the current picture
    /// </summary>
    public void RemovePicture() {
        _personalProfile.RemovePicture();
        Picture.texture = _personalProfile.GetCurrentPicture();
    }
}
