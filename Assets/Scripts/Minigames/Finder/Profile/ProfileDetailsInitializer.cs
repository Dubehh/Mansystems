using UnityEngine;
using UnityEngine.UI;

public class ProfileDetailsInitializer : MonoBehaviour {

    public FinderProfile Profile { get; set; }

    [SerializeField] public RawImage Picture;
    [SerializeField] public Text Header;
    [SerializeField] public Text City;
    [SerializeField] public Text PhoneNumber;
    [SerializeField] public Text FavMovie;
    [SerializeField] public Text FavMusic;
    [SerializeField] public Text FavFood;
    [SerializeField] public Text FavSport;
    [SerializeField] public Text FavGame;
    [SerializeField] public Text FavVacation;

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
