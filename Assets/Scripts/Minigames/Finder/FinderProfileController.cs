using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.App.Data_Management.Handshakes;
using UnityEngine;
using UnityEngine.Networking;

public class FinderProfileController {

    private List<FinderProfile> _profiles;
    private int _currentProfileIndex;

    /// <summary>
    /// Gathers a list of Finder profile's from a UnityWebRequest and fills a list with them
    /// </summary>
    /// <param name="data">The UnityWebRequest to retrieve the data from</param>
    public void LoadProfiles(UnityWebRequest data, List<string> likes) {
        _profiles = new List<FinderProfile>();

        var profiles = new JSONObject(data.downloadHandler.text);
        var random = new System.Random();

        for (var i = 0; i < profiles.Count; i++) {
            var profile = profiles[i];

            if (likes.Contains(profile["uuid"].str)) break;
            _profiles.Add(new FinderProfile(new FinderProfileInfo() {
                PlayerUID = profile["uuid"].str,
                Name = profile["Name"].str,
                Age = (int)profile["Age"].i,
                City = profile["City"].str,
                PhoneNumber =  (int)profile["PhoneNumber"].i,
                FavMovie = profile["FavMovie"].str,
                FavMusic = profile["FavMusic"].str,
                FavFood = profile["FavFood"].str,
                FavSport = profile["FavSport"].str,
                FavGame = profile["FavGame"].str,
                FavVacation = profile["FavVacation"].str
            }, profile["pictures"].list.Select(x=> x.str).ToArray()));
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
    }
}
