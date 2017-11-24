using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public Camera _cam;
    private float _maxWidth;

    /// <summary>
    /// Sets up variables used during the update
    /// </summary>
    void Start() {
            _cam = Camera.main;

            Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
            Vector3 targetWidth = _cam.ScreenToWorldPoint(upperCorner);
            float MannyWidth = GetComponent<Renderer>().bounds.extents.x;
            _maxWidth = targetWidth.x - MannyWidth;        
    }
    /// <summary>
    /// Prevents Manny from leaving the screen
    /// </summary>
    void FixedUpdate() {
        transform.Translate(new Vector2(Input.acceleration.x * (Time.deltaTime * 15), 0));
        Vector2 targetPosition = transform.position;
        float targetWidth = Mathf.Clamp(targetPosition.x, -_maxWidth, _maxWidth);
        targetPosition = new Vector2(targetWidth, targetPosition.y);
        transform.position = targetPosition;
    }
}