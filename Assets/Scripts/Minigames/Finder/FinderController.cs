using Assets.Scripts.App.Data_Management;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FinderController : MonoBehaviour {

    private FinderProfile _finderProfile;

    private List<FinderProfile> _profiles;
    private int _currentProfileIndex;

    [SerializeField]
    private RawImage _picture;

    [SerializeField]
    private Text _name;

    [SerializeField]
    private Text _description;

    [SerializeField]
    private List<Texture> _textures;

    private void Start() {
        _profiles = new List<FinderProfile>();

        new Handshake(HandshakeProtocol.Response).AddParameter("responseHandler", "finder").Shake((request) => {
            var profiles = new JSONObject(request.downloadHandler.text);
            var random = new System.Random();

            for (int i = 0; i < profiles.Count; i++) {
                var profile = profiles[i];

                _profiles.Add(new FinderProfile(
                    _textures,
                    profile["name"].str,
                    profile["description"].str
                ));
            }

        });

        Debug.Log(_profiles[0].Description);

        UpdateUI();
    }

    public void SwitchPicture(bool next) {
        _picture.texture = _finderProfile.GetPicture(next);
    }

    public void UpdateUI() {
        var current = _profiles[_currentProfileIndex];

        _picture.texture = current.GetCurrentPicture();
        _name.text = current.Name;
        _description.text = current.Description;
    }

    public void NextProfile(bool like) {
        if (like) { } //Add to matches
        _currentProfileIndex++;
        UpdateUI();
    }
}
