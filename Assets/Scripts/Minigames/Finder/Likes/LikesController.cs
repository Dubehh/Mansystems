using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikesController : MonoBehaviour {

    public FinderController FinderController;
    public GameObject Like;
    
	// Use this for initialization
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
