using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Minigames.Finder.Likes {
    public class LikesController : MonoBehaviour {
        private bool _active;
        private Animator _animator;
        [SerializeField] public FinderController FinderController;
        [SerializeField] public GameObject Like;

        private void Awake() {
            _animator = GetComponentInParent<Animator>();
        }

        // Fills the likes screen with Like prefabs according to the LikeProfiles list from the FinderController
        public void Init() {
            if (!_active) {
                if (transform.childCount > 0)
                    GetComponentsInChildren<LikePrefab>().ToList().ForEach(x => Destroy(x.gameObject));

                var y = 506f;

                foreach (var profile in FinderController.FinderProfileController.LikedProfiles) {
                    var like = Instantiate(Like, transform).GetComponent<LikePrefab>();
                    like.Profile = profile;
                    like.Init();
                    like.transform.localPosition = new Vector2(0, y);
                    y -= like.GetComponent<RectTransform>().rect.height + 2;
                }
            }

            _active = !_active;
            _animator.Play(_active ? "SlideOpen" : "SlideClose");
        }
    }
}