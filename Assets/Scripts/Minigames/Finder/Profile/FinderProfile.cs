using System;
using System.Collections.Generic;
using Assets.Scripts.App.Data_Management.Handshakes;
using UnityEngine;

public class FinderProfile {
    public List<Texture> Pictures { get; set; }
    public FinderProfileInfo ProfileInfo { get; set; }
    public string[] ImageNames { get; private set; }

    private int _currentPictureIndex;

    public FinderProfile(FinderProfileInfo info, string[] imageNames) {
        Pictures = new List<Texture>();
        ProfileInfo = info;
        ImageNames = imageNames;
    }

    /// <summary>
    /// Loads the users pictures from the webserver
    /// </summary>
    /// <param name="controller">The controller which Monobehaviour's used in the request</param>
    /// <param name="onComplete">The method to fire when the loading is done</param>
    public void LoadPictures(MonoBehaviour controller, Action<FileProtocolQueue> onComplete = null) {
        var fileQueue = new FileProtocolQueue(onComplete, www => Pictures.Add(www.texture));
        foreach (var imageName in ImageNames) {
            fileQueue.Attach(new FileProtocol(Protocol.Download, controller)
                .Target("finder")
                .For(ProfileInfo.PlayerUID)
                .AddParameter("name", imageName) as FileProtocol);
        }
        fileQueue.Commit();
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
