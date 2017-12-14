using System.Collections.Generic;
using System;
using System.Linq;
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
    public FinderProfileController FinderProfileController { get; private set; }

    private void Start() {
        FinderProfileController = new FinderProfileController();
        if (LikedProfileIDs == null) LikedProfileIDs = new List<string>();
        var a = this;
        new InformationProtocol(Protocol.Fetch)
            .AddParameter("uuid", PlayerPrefs.GetString("uid"))
            .AddParameter("responseHandler", "finder")
            .Send(request => {
                FinderProfileController.LoadProfiles(request, LikedProfileIDs);
                foreach (var likedProfile in FinderProfileController.LikedProfiles) {
                    if (likedProfile == FinderProfileController.LikedProfiles.Last())
                        likedProfile.LoadPictures(a, queue => { UpdateUI(); });
                    else likedProfile.LoadPictures(a);
                }
            });
    }

    /// <summary>
    /// Updates the screen with the currently selected profile
    /// </summary>
    public void UpdateUI() {
        var current = FinderProfileController.GetCurrentProfile();

        if (current == null) End();
        else
            current.LoadPictures(this, queue => {
                Debug.Log(queue.Queue.Count + " committed");
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
        Picture.texture = FinderProfileController.GetCurrentProfile().GetPicture(next);
    }

    /// <summary>
    /// OnClick event for the hasLiked/pass buttons
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

    /// <summary>
    /// Is called when no profiles are available anymore
    /// Opens a screen that displays a warning
    /// </summary>
    private void End() {
        EndScreen.SetActive(true);
        gameObject.SetActive(false);
    }

}
