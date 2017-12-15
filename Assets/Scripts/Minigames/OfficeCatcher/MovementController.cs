using UnityEngine;

public class MovementController : MonoBehaviour {
    public Camera _cam;
    private float _maxWidth;

    /// <summary>
    ///     Sets up variables used during the update
    /// </summary>
    private void Start() {
        _cam = Camera.main;

        var upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        var targetWidth = _cam.ScreenToWorldPoint(upperCorner);
        var MannyWidth = GetComponent<Renderer>().bounds.extents.x;
        _maxWidth = targetWidth.x - MannyWidth;
    }

    /// <summary>
    ///     Prevents Manny from leaving the screen
    /// </summary>
    private void FixedUpdate() {
        transform.Translate(new Vector2(Input.acceleration.x * (Time.deltaTime * 15), 0));
        Vector2 targetPosition = transform.position;
        var targetWidth = Mathf.Clamp(targetPosition.x, -_maxWidth, _maxWidth);
        targetPosition = new Vector2(targetWidth, targetPosition.y);
        transform.position = targetPosition;
    }
}