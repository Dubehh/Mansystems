using System.Collections.Generic;
using UnityEngine;

public class FinderProfile {

    private int _currentPictureIndex;

    public List<Texture> Pictures { get; set; }
    public string Name;
    public string Description;

    public FinderProfile(List<Texture> pictures, string name, string description) {
        Pictures = pictures;
        Name = name;
        Description = description;
    }

    public Texture GetPicture(bool next) {
        if (next) {
            if (_currentPictureIndex >= Pictures.Count - 1) _currentPictureIndex = 0;
            else _currentPictureIndex += 1;
        } else {
            if (_currentPictureIndex <= 0) _currentPictureIndex = Pictures.Count - 1;
            else _currentPictureIndex -= 1;
        }

        return Pictures[_currentPictureIndex];
    }

    public Texture GetCurrentPicture() {
        return Pictures[_currentPictureIndex];
    }
}
