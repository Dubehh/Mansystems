using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LikePrefab : MonoBehaviour {

    public FinderProfile Profile;

    [SerializeField] private RawImage _picture;
    [SerializeField] private Text _name;

    public void Init() {
        _name.text = Profile.ProfileInfo.Name;
    }
    public void DetailsClick() {
        // Open profile with Profile.ID
    }
}
