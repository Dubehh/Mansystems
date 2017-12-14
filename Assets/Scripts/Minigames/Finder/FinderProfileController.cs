using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

public class FinderProfileController {

    private List<FinderProfile> _profiles;
    private int _currentProfileIndex;

    /// <summary>
    /// Gathers a list of Finder profile's from a UnityWebRequest and fills a list with them
    /// </summary>
    /// <param name="data">The UnityWebRequest to retrieve the data from</param>
    public List<FinderProfile> LoadProfiles(UnityWebRequest data, List<string> likes) {
        var likedProfiles = new List<FinderProfile>();
        _profiles = new List<FinderProfile>();

        var profiles = new JSONObject(data.downloadHandler.text);
        var random = new System.Random();

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
                likedProfiles.Add(newProfile);
            else _profiles.Add(newProfile);
        }

        _profiles = _profiles.OrderBy(x => random.Next()).ToList();
        return likedProfiles;
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
        _currentProfileIndex++;
    }
}
