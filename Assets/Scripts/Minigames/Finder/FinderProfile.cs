using System;
using System.Collections.Generic;
using Assets.Scripts.App.Data_Management.Handshakes;
using UnityEngine;

public struct FinderProfileInfo {
    public string PlayerUID;
    public string Name;
    public int Age;
    public string City;
    public int PhoneNumber;
    public string FavMovie, FavMusic, FavFood, FavSport, FavGame, FavVacation;
}

public class FinderProfile {
    public List<Texture> Pictures { get; set; }
    public FinderProfileInfo ProfileInfo { get; set; }

    private string[] _imageNames;

    private int _currentPictureIndex;

    public FinderProfile(FinderProfileInfo info, string[] imageNames) {
        Pictures = new List<Texture>();
        ProfileInfo = info;
        _imageNames = imageNames;
    }

    public void LoadPictures(FinderController controller, Action onComplete) {
        foreach (var imageName in _imageNames) {
            new FileProtocol(Protocol.Download, controller)
                .Target("finder")
                .For(ProfileInfo.PlayerUID)
                .AddParameter("name", imageName)
                .Send(www => {
                    Pictures.Add(www.texture);
                    onComplete();
                });
        }
    }

    /// <summary>
    /// Returns another picture from the Pictures list
    /// </summary>
    /// <param name="next">bool which indicates if the previous or the next picture from the list should be returned</param>
    public Texture GetPicture(bool next) {
        if (next)
            _currentPictureIndex = _currentPictureIndex >= Pictures.Count - 1 ? 0 : _currentPictureIndex + 1;
        else
            _currentPictureIndex = _currentPictureIndex <= 0 ? Pictures.Count - 1 : _currentPictureIndex - 1;

        return GetCurrentPicture();
    }

    /// <summary>
    /// Returns the current picture from the Pictures list
    /// </summary>
    public Texture GetCurrentPicture() {
        return Pictures.Count > 0 ? Pictures[_currentPictureIndex] : null;
    }
}
