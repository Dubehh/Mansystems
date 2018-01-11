using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.App;
using Assets.Scripts.App.Data_Management.Handshakes;
using Assets.Scripts.App.Data_Management.Table;
using Assets.Scripts.Minigames.Finder.Profile;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Minigames.Finder {
    [Serializable]
    public class FinderController : MonoBehaviour {
        public const string LikeTable = "FinderLikes";
        public const string ProfileTable = "FinderProfile";

        private GameObject _currentView;

        [SerializeField] public Texture DefaultPicture;
        [SerializeField] public ProfileDetailsInitializer ProfileDetailsInitializer;

        [SerializeField] public List<GameObject> Views;

        public List<string> LikedProfileIDs { get; set; }
        public FinderProfileController FinderProfileController { get; private set; }

        private void Awake() {
            ChangeView("Main");
            FinderProfileController = new FinderProfileController();
            if (LikedProfileIDs == null) LikedProfileIDs = new List<string>();
            var instance = this;
            new InformationProtocol(Protocol.Data)
                .SetHandler("finderProfiles", InformationProtocol.HandlerType.Fetch)
                .AddParameter("uuid", PlayerPrefs.GetString("uid"))
                .Send(request => {
                    FinderProfileController.LoadProfiles(request, LikedProfileIDs);
                    foreach (var likedProfile in FinderProfileController.LikedProfiles)
                        if (likedProfile == FinderProfileController.LikedProfiles.Last())
                            likedProfile.LoadPictures(instance);
                        else likedProfile.LoadPictures(instance);
                    FinderProfileController.PersonalProfile.LoadPictures(instance);
                    UpdateUI();
                });
        }

        /// <summary>
        ///     Updates the screen with the currently selected profile
        /// </summary>
        public void UpdateUI() {
            var current = FinderProfileController.GetCurrentProfile();

            if (current == null) {
                ChangeView("EndScreen");
            }
            else {
                if (current.ImageNames.Count > 0)
                    current.LoadPictures(this, queue => { InitializeProfile(current); });
                else InitializeProfile(current);
            }
        }

        /// <summary>
        ///     Fills the profile details object with all the current profile's information
        /// </summary>
        /// <param name="current"></param>
        public void InitializeProfile(FinderProfile current) {
            ProfileDetailsInitializer.Profile = current;
            ProfileDetailsInitializer.Init();
            ProfileDetailsInitializer.GetComponentInChildren<ScrollRect>().normalizedPosition = new Vector2(0, 1);
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

        /// <summary>
        ///     Hides the current view and opens a new one
        /// </summary>
        /// <param name="name">The name of the view to display</param>
        public void ChangeView(string name) {
            if (_currentView != null)
                _currentView.SetActive(false);

            var view = Views.Find(x => x.name == name);
            _currentView = view ?? _currentView;

            if (_currentView != null)
                _currentView.SetActive(true);
        }

        /// <summary>
        ///     Returns the player to Manny
        /// </summary>
        public void Quit() {
            SceneManager.LoadScene(0);
        }
    }
}