using System.Collections.Generic;
using System;
using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Tracking.Table;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FinderController : MonoBehaviour {

    public const string LikeTable = "FinderLikes";
    public const string ProfileTable = "FinderProfile";

    [SerializeField] public RawImage Picture;
    [SerializeField] public Text Name;
    [SerializeField] public Text Description;
    [SerializeField] public GameObject EndScreen;

    public List<string> LikedProfileIDs { get; set; }
    public List<FinderProfile> LikedProfiles { get; private set; }

    private FinderProfileController _finderProfileController;

    private void Start() {
        _finderProfileController = new FinderProfileController();
        LikedProfiles = new List<FinderProfile>();
        new InformationProtocol(Protocol.Fetch)
            .AddParameter("uuid", PlayerPrefs.GetString("uid"))
            .AddParameter("responseHandler", "finder")
            .Send(request => {
                LikedProfiles = _finderProfileController.LoadProfiles(request, LikedProfileIDs);
                UpdateUI();
            });
    }

    /// <summary>
    /// Updates the screen with the currently selected profile
    /// </summary>
    public void UpdateUI() {
        var current = _finderProfileController.GetCurrentProfile();

        if (current == null) End();
        else
            current.LoadPictures(this, () => {
                Picture.texture = current.GetCurrentPicture() != null ? current.GetCurrentPicture() : new Texture();
                Name.text = current.ProfileInfo.Name + " (" + current.ProfileInfo.Age + ")";
                Description.text = current.ProfileInfo.City;
            });
    }

    /// <summary>
    /// OnClick event for the picture changing buttons
    /// </summary>
    /// <param name="next"></param>
    public void SwitchPicture(bool next) {
        Picture.texture = _finderProfileController.GetCurrentProfile().GetPicture(next);
    }

    /// <summary>
    /// OnClick event for the like/pass buttons
    /// </summary>
    /// <param name="like"></param>
    public void NextProfile(bool like) {
        var currentProfile = _finderProfileController.GetCurrentProfile();
        if (like && !LikedProfiles.Contains(currentProfile)) {
            LikedProfiles.Add(currentProfile);
            AppData.Instance().Registry.
                Fetch(LikeTable).
                Insert(DataParams.Build("ProfileID", currentProfile.ProfileInfo.PlayerUID));
        }

        _finderProfileController.NextProfile();
        UpdateUI();
    }

    /// <summary>
    /// Is called when no profiles are available anymore
    /// Opens a screen that displays a warning
    /// </summary>
    private void End() {
        EndScreen.SetActive(true);
        gameObject.SetActive(false);
    }

}
