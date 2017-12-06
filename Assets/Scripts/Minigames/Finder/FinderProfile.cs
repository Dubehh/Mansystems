﻿using System.Collections.Generic;
using UnityEngine;

public class FinderProfile {
    public List<Texture> Pictures { get; set; }
    public string Name;
    public string Description;

    private int _currentPictureIndex;

    public FinderProfile(List<Texture> pictures, string name, string description) {
        Pictures = pictures;
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Returns another picture from the Pictures list
    /// </summary>
    /// <param name="next">bool which indicates if the previous or the next picture from the list should be returned</param>
    public Texture GetPicture(bool next) {
        if (next)
            _currentPictureIndex = (_currentPictureIndex >= Pictures.Count - 1) ? 0 : _currentPictureIndex + 1;
        else
            _currentPictureIndex = (_currentPictureIndex <= 0) ? Pictures.Count - 1 : _currentPictureIndex - 1;
        
        return Pictures.Count > 0 ? Pictures[_currentPictureIndex] : null;
    }

    /// <summary>
    /// Returns the current picture from the Pictures list
    /// </summary>
    public Texture GetCurrentPicture() {
        return Pictures.Count > 0 ? Pictures[_currentPictureIndex] : null;
    }
}
