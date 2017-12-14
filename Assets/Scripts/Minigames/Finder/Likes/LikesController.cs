using System.Linq;
using UnityEngine;

public class LikesController : MonoBehaviour {

    public FinderController FinderController;
    public GameObject Like;

    private Animator _animator;
    private bool _active;

    private void Awake() {
        _animator = GetComponentInParent<Animator>();
    }

    // Fills the likes screen with Like prefabs according to the LikeProfiles list from the FinderController
    public void Init() {
        if(transform.childCount > 0) GetComponentsInChildren<LikePrefab>().ToList().ForEach(x => Destroy(x.gameObject));       

        var y = 506f;

        foreach (var profile in FinderController.FinderProfileController.LikedProfiles) {
            var like = Instantiate(Like, transform).GetComponent<LikePrefab>();
            like.Profile = profile;
            like.Init();
            like.transform.localPosition = new Vector2(0, y);
            y -= like.GetComponent<RectTransform>().rect.height + 2;
        }

        _active = !_active;
        _animator.Play(_active ? "SlideOpen" : "SlideClose");
    }
}
