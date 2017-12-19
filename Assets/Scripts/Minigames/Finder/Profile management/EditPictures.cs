using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.App.Data_Management.Handshakes;
using UnityEngine;
using UnityEngine.UI;

public class EditPictures : MonoBehaviour {

    [SerializeField] public RawImage Picture;

    private FinderProfile _personalProfile;

    private void Start() {
        var finderController = FindObjectOfType<FinderController>().FinderProfileController;
        _personalProfile = finderController.PersonalProfile ??
                           finderController.LikedProfiles[0];

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

    public void RemovePicture() {
        // Remove image from server with _personalProfile.GetCurrentPictureName()
        _personalProfile.Pictures.Remove(_personalProfile.GetCurrentPicture());
        Picture.texture = _personalProfile.GetCurrentPicture();
    }
}
