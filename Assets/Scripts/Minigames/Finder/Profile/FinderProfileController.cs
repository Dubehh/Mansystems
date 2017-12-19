using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public class FinderProfileController {
    private int _currentProfileIndex;

    private List<FinderProfile> _profiles;
    public List<FinderProfile> LikedProfiles { get; set; }
    public FinderProfile PersonalProfile;

    /// <summary>
    ///     Gathers a list of Finder profile's from a UnityWebRequest and fills a list with them
    /// </summary>
    /// <param name="data">The UnityWebRequest to retrieve the data from</param>
    /// <param name="likes">A list with UID's that the player has liked before</param>
    public void LoadProfiles(UnityWebRequest data, List<string> likes) {
        LikedProfiles = new List<FinderProfile>();
        _profiles = new List<FinderProfile>();

        var profiles = new JSONObject(data.downloadHandler.text);
        var random = new Random();

        for (var i = 0; i < profiles.Count; i++) {
            var profile = profiles[i];

            var newProfile = new FinderProfile(new FinderProfileInfo {
                PlayerUID = profile["uuid"].str,
                Name = profile["Name"].str,
                Age = (int)profile["Age"].i,
                City = profile["City"].str,
                PhoneNumber = (int)profile["PhoneNumber"].i,
                FavMovie = profile["FavMovie"].str,
                FavMusic = profile["FavMusic"].str,
                FavFood = profile["FavFood"].str,
                FavSport = profile["FavSport"].str,
                FavGame = profile["FavGame"].str,
                FavVacation = profile["FavVacation"].str
            }, profile["pictures"].list.Select(x => x.str).ToArray());

            if (likes.Contains(profile["uuid"].str))
                LikedProfiles.Add(newProfile);
            else if (profile["uuid"].str == PlayerPrefs.GetString("uid"))
                PersonalProfile = newProfile;
            else
                _profiles.Add(newProfile);

        }

        _profiles = _profiles.OrderBy(x => random.Next()).ToList();
    }

    /// <summary>
    ///     Returns the current profile from the list
    /// </summary>
    public FinderProfile GetCurrentProfile() {
        return _currentProfileIndex > _profiles.Count - 1 ? null : _profiles[_currentProfileIndex];
    }

    /// <summary>
    ///     Goes to the next profile in the list
    /// </summary>
    public void NextProfile() {
        _currentProfileIndex++;
    }
}