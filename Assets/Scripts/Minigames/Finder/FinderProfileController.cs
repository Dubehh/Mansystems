using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class FinderProfileController {

    private List<FinderProfile> _profiles;
    private int _currentProfileIndex;

    /// <summary>
    /// Gathers a list of Finder profile's from a UnityWebRequest and fills a list with them
    /// </summary>
    /// <param name="data">The UnityWebRequest to retrieve the data from</param>
    public void LoadProfiles(UnityWebRequest data) {
        _profiles = new List<FinderProfile>();

        var profiles = new JSONObject(data.downloadHandler.text);
        var random = new System.Random();

        for (int i = 0; i < profiles.Count; i++) {
            var profile = profiles[i];

            _profiles.Add(new FinderProfile(
                new List<Texture>(),
                profile["name"].str,
                profile["description"].str
            ));
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
