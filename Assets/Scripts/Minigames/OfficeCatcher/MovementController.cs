using UnityEngine;

public class MovementController : MonoBehaviour {
    public Camera Cam;
    private float _maxWidth;

    /// <summary>
    ///     Sets up variables used during the update
    /// </summary>
    private void Start() {
        var upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        var targetWidth = Cam.ScreenToWorldPoint(upperCorner);
        var mannyWidth = GetComponent<Renderer>().bounds.extents.x;
        _maxWidth = targetWidth.x - mannyWidth;
    }

    /// <summary>
    ///     Prevents Manny from leaving the screen
    /// </summary>
    private void FixedUpdate() {
        Vector3 mousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = new Vector3(mousePos.x, 1.0f, 0.0f);
        float f = Mathf.Clamp(targetPosition.x, _maxWidth, _maxWidth);
        targetPosition = new Vector3(f, targetPosition.y, targetPosition.z);
        transform.position = targetPosition;

        /*
        transform.Translate(new Vector2(Input.acceleration.x * (Time.deltaTime * 15), 0));
        Vector2 targetPosition = transform.position;
        var targetWidth = Mathf.Clamp(targetPosition.x, -_maxWidth, _maxWidth);
        targetPosition = new Vector2(targetWidth, targetPosition.y);
        transform.position = targetPosition;

    */
    }
}