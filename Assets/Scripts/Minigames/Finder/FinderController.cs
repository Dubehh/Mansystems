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
    [SerializeField] public Text Description;

    [SerializeField] public List<GameObject> Views;
    [SerializeField] public Text Name;
    [SerializeField] public RawImage Picture;

    private GameObject _current;

    public List<string> LikedProfileIDs { get; set; }
    public FinderProfileController FinderProfileController { get; private set; }

    private void Start() {
        _current = ChangeView("Main");
        FinderProfileController = new FinderProfileController();
        if (LikedProfileIDs == null) LikedProfileIDs = new List<string>();
        var a = this;
        new InformationProtocol(Protocol.Fetch)
            .AddParameter("uuid", PlayerPrefs.GetString("uid"))
            .AddParameter("responseHandler", "finder")
            .Send(request => {
                FinderProfileController.LoadProfiles(request, LikedProfileIDs);
                foreach (var likedProfile in FinderProfileController.LikedProfiles)
                    if (likedProfile == FinderProfileController.LikedProfiles.Last())
                        likedProfile.LoadPictures(a, queue => { UpdateUI(); });
                    else likedProfile.LoadPictures(a);
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
                Debug.Log(queue.Queue.Count + " committed");
                Picture.texture = current.GetCurrentPicture() != null ? current.GetCurrentPicture() : new Texture();
                Name.text = current.ProfileInfo.Name + " (" + current.ProfileInfo.Age + ")";
                Description.text = current.ProfileInfo.City;
            });
    }

    /// <summary>
    ///     OnClick event for the picture changing buttons
    /// </summary>
    /// <param name="next"></param>
    public void SwitchPicture(bool next) {
        Picture.texture = FinderProfileController.GetCurrentProfile().GetPicture(next);
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

    public GameObject ChangeView(string name) {
        if (_current != null)
            _current.SetActive(false);

        var view = Views.Find(x => x.name == name);
        if (view == null) return _current;
        _current = view;
        _current.SetActive(true);
        return _current;
    }
}