using UnityEngine;
using UnityEngine.UI;

public class LikePrefab : MonoBehaviour {
    [SerializeField] public Text Name;

    [SerializeField] public RawImage Picture;

    public FinderProfile Profile;

    /// <summary>
    ///     Initializes the prefab by filling it with the Profile's information
    /// </summary>
    public void Init() {
        Name.text = Profile.ProfileInfo.Name;
        Picture.texture = Profile.GetCurrentPicture();
    }

    /// <summary>
    ///     Event for when the user clicks the prefab's detail button
    /// </summary>
    public void DetailsClick() {
        FindObjectOfType<FinderController>().ChangeView("ProfileDetails");
        var initializer = FindObjectOfType<ProfileDetailsInitializer>();
        initializer.Profile = Profile;
        initializer.Init();
    }
}