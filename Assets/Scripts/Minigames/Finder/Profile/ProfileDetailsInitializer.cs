using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Minigames.Finder.Profile {
    public class ProfileDetailsInitializer : MonoBehaviour {
        [SerializeField] public Text City;
        [SerializeField] public Text FavFood;
        [SerializeField] public Text FavGame;
        [SerializeField] public Text FavMovie;
        [SerializeField] public Text FavMusic;
        [SerializeField] public Text FavSport;
        [SerializeField] public Text FavVacation;
        [SerializeField] public Text Header;
        [SerializeField] public Text PhoneNumber;
        [SerializeField] public RawImage Picture;

        public FinderProfile Profile { get; set; }

        /// <summary>
        ///     Fills the UI elements with the profile's information
        /// </summary>
        public void Init() {
            var info = Profile.ProfileInfo;
            Picture.texture = Profile.GetCurrentPicture();
            Header.text = info.Name + " (" + info.Age + ")";
            City.text = info.City;
            PhoneNumber.text = info.PhoneNumber;
            FavMovie.text = info.FavMovie;
            FavMusic.text = info.FavMusic;
            FavFood.text = info.FavFood;
            FavSport.text = info.FavSport;
            FavGame.text = info.FavGame;
            FavVacation.text = info.FavVacation;
        }

        /// <summary>
        ///     OnClick event for the picture changing buttons
        /// </summary>
        /// <param name="next"></param>
        public void SwitchPicture(bool next) {
            Picture.texture = Profile.GetPicture(next);
        }
    }
}