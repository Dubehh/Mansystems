using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Tracking.Table;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FinderController : MonoBehaviour {
    public const string LikeTable = "FinderLikes";
    public const string ProfileTable = "FinderProfile";

    [SerializeField] public List<GameObject> Views;
    [SerializeField] public ProfileDetailsInitializer ProfileDetailsInitializer;

    public List<string> LikedProfileIDs { get; set; }
    public FinderProfileController FinderProfileController { get; private set; }

    private GameObject _currentView;

    private void Awake() {
        ChangeView("Main");
        FinderProfileController = new FinderProfileController();
        if (LikedProfileIDs == null) LikedProfileIDs = new List<string>();
        var a = this;
        new InformationProtocol(Protocol.Data)
            .SetHandler("finderProfiles", InformationProtocol.HandlerType.Fetch)
            .AddParameter("uuid", PlayerPrefs.GetString("uid"))
            .Send(request => {
                FinderProfileController.LoadProfiles(request, LikedProfileIDs);
                foreach (var likedProfile in FinderProfileController.LikedProfiles)
                    if (likedProfile == FinderProfileController.LikedProfiles.Last())
                        likedProfile.LoadPictures(a);
                    else likedProfile.LoadPictures(a);
                FinderProfileController.PersonalProfile.LoadPictures(a);
                UpdateUI();
            });
    }

    /// <summary>
    ///     Updates the screen with the currently selected profile
    /// </summary>
    public void UpdateUI() {
        var current = FinderProfileController.GetCurrentProfile();

        if (current == null)
            ChangeView("EndScreen");
        else
            current.LoadPictures(this, queue => {
                ProfileDetailsInitializer.Profile = current;
                ProfileDetailsInitializer.Init();
            });
    }

    /// <summary>
    ///     OnClick event for the hasLiked/pass buttons
    /// </summary>
    /// <param name="hasLiked"></param>
    public void NextProfile(bool hasLiked) {
        var likedProfiles = FinderProfileController.LikedProfiles;
        var currentProfile = FinderProfileController.GetCurrentProfile();
        if (hasLiked && !likedProfiles.Contains(currentProfile)) {
            likedProfiles.Add(currentProfile);
            AppData.Instance().Registry.Fetch(LikeTable)
                .Insert(DataParams.Build("ProfileID", currentProfile.ProfileInfo.PlayerUID));
        }
        FinderProfileController.NextProfile();
        UpdateUI();
    }

    public void ChangeView(string name) {
        if (_currentView != null)
            _currentView.SetActive(false);

        var view = Views.Find(x => x.name == name);
        _currentView = view ?? _currentView;

        if (_currentView != null)
            _currentView.SetActive(true);
    }
}