using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    private CatcherController _controller;

    private void Start() {
        _controller = FindObjectOfType<CatcherController>();
    }

    /// <summary>
    ///     Destroys gameobject when it collides with collider
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        var gameObj = other.gameObject;
        var obj = _controller.ObjectRegister[gameObj];
        Destroy(gameObj);
        _controller.GameScore = _controller.GameScore + obj.ObjectScore;
        _controller.Experience = _controller.Experience + _controller.GameScore * 30 / 1080;

        if (obj.IsBroken) _controller.Updatelife();
        if (obj.IsLogo) _controller.LogosCaught++;
        if (obj.IsFakeLogo) _controller.FakeLogosCaught++;
        _controller.ObjectRegister.Remove(gameObj);
        _controller.UpdateScore();
    }
}