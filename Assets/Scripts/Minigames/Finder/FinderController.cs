﻿using Assets.Scripts.App.Data_Management;
using System.Collections.Generic;
using System;
using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Tracking.Table;
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

    public List<string> LikedProfileIDs;
    private List<FinderProfile> _likedProfiles;

    private void Start() {
        _finderProfileController = new FinderProfileController();
        _likedProfiles = new List<FinderProfile>();

        new InformationProtocol(Protocol.Fetch)
            .AddParameter("uuid", PlayerPrefs.GetString("uid"))
            .AddParameter("responseHandler", "finder")
            .Send(request => {
                _finderProfileController.LoadProfiles(request, LikedProfileIDs);
                UpdateUI();
            });
    }

    /// <summary>
    /// Updates the screen with the currently selected profile
    /// </summary>
    public void UpdateUI() {
        var current = _finderProfileController.GetCurrentProfile();
        current.LoadPictures(this, () => {
            _picture.texture = current.GetCurrentPicture();
            _name.text = current.ProfileInfo.Name;
            _description.text = current.ProfileInfo.City;
        });
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
        if (like && !_likedProfiles.Contains(_finderProfileController.GetCurrentProfile())) {
            _likedProfiles.Add(_finderProfileController.GetCurrentProfile());
            AppData.Instance().Registry.Fetch("FinderLikes").Insert(DataParams.Build("ProfileID", _finderProfileController.GetCurrentProfile().ProfileInfo.PlayerUID),
                () => {
                    Debug.Log("ID added");
                });
        }

        _finderProfileController.NextProfile();
        UpdateUI();
    }

}
