using UnityEngine;
using UnityEngine.UI;

public class LikePrefab : MonoBehaviour {

    public FinderProfile Profile;

    [SerializeField] public RawImage Picture;
    [SerializeField] public Text Name;

    /// <summary>
    /// Initializes the prefab by filling it with the Profile's information
    /// </summary>
    public void Init() {
        Name.text = Profile.ProfileInfo.Name;
    }

    /// <summary>
    /// Event for when the user clicks the prefab's detail button
    /// </summary>
    public void DetailsClick() {
        // Open profile with Profile.ID
    }
}
