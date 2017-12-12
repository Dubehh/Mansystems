using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.App.Data_Management.Handshakes;
using UnityEngine;
using UnityEngine.Networking;

public class FinderProfileController {

    private List<FinderProfile> _profiles;
    private int _currentProfileIndex;
    private FinderController _finderController;

    /// <summary>
    /// Gathers a list of Finder profile's from a UnityWebRequest and fills a list with them
    /// </summary>
    /// <param name="data">The UnityWebRequest to retrieve the data from</param>
    public void LoadProfiles(UnityWebRequest data, FinderController controller) {
        _finderController = controller;
        _profiles = new List<FinderProfile>();

        var profiles = new JSONObject(data.downloadHandler.text);
        var random = new System.Random();

        for (int i = 0; i < profiles.Count; i++) {
            var profile = profiles[i];

            _profiles.Add(new FinderProfile(new FinderProfileInfo() {
                PlayerID = profile["playerID"].str,
                Name = profile["name"].str,
                Age = (int)profile["age"].i,
                City = profile["city"].str,
                PhoneNumber =  (int)profile["phonenumber"].i,
                FavMovie = profile["favmovie"].str,
                FavMusic = profile["favmusic"].str,
                FavFood = profile["favfood"].str,
                FavSport = profile["favsport"].str,
                FavGame = profile["favgame"].str,
                FavVacation = profile["favvacation"].str
            }));
        }

        _profiles = _profiles.OrderBy(x => random.Next()).ToList();
    }

    /// <summary>
    /// Returns the current profile from the list
    /// </summary>
    public FinderProfile GetCurrentProfile() {
        return _currentProfileIndex > _profiles.Count - 1 ? null : _profiles[_currentProfileIndex];
    }

    /// <summary>
    /// Goes to the next profile in the list
    /// </summary>
    public void NextProfile() {
        if (_currentProfileIndex >= _profiles.Count - 1) return;
        _currentProfileIndex++;

        if (GetCurrentProfile() == null) return;

        var fp = new FileProtocol(Protocol.Download, _finderController);
        fp.AddParameter("targetFolder", GetCurrentProfile().ProfileInfo.PlayerID);
        //fp.Put("file", )
    }
}
