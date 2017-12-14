using UnityEngine;

public class LikesController : MonoBehaviour {

    public FinderController FinderController;
    public GameObject Like;
    
	// Fills the likes screen with Like prefabs according to the LikeProfiles list from the FinderController
    public void Init() {
        var y = 540f;

        foreach (var profile in FinderController.LikedProfiles) {
            var like = Instantiate(Like, transform).GetComponent<LikePrefab>();
            like.Profile = profile;
            like.Init();
            like.transform.localPosition = new Vector2(0, y);
            y -= like.GetComponent<RectTransform>().rect.height;
        }
    }
}
