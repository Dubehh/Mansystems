using Assets.Scripts.App.Data_Management;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FinderController : MonoBehaviour {

    [SerializeField]
    private RawImage _picture;

    [SerializeField]
    private Text _name;

    [SerializeField]
    private Text _description;

    private FinderProfileController _finderProfileController;

    private void Start() {
        _finderProfileController = new FinderProfileController();

        new Handshake(HandshakeProtocol.Fetch).AddParameter("responseHandler", "finder").Shake((request) => {
            // Make sure that the player doesn't see his own profile
            _finderProfileController.LoadProfiles(request);
            UpdateUI();
        });
    }

    /// <summary>
    /// Updates the screen with the currently selected profile
    /// </summary>
    public void UpdateUI() {
        var current = _finderProfileController.GetCurrentProfile();

        _picture.texture = current.GetCurrentPicture();
        _name.text = current.Name;
        _description.text = current.Description;
    }

    /// <summary>
    /// OnClick event for the picture changing buttons
    /// </summary>
    /// <param name="next"></param>
    public void SwitchPicture(bool next) {
        _picture.texture = _finderProfileController.GetCurrentProfile().GetPicture(next);
    }

    /// <summary>
    /// OnClick event for the like/pass buttons
    /// </summary>
    /// <param name="like"></param>
    public void NextProfile(bool like) {
        if (like) { } //Add to matches
        _finderProfileController.NextProfile();

        UpdateUI();
    }
}
