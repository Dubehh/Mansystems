using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public Camera _cam;
    private float maxWidth;

    /// <summary>
    /// Defines Screensize
    /// </summary>
    void Start () {
		if (_cam == null) {
            _cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth= _cam.ScreenToWorldPoint (upperCorner);
        float MannyWidth = GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - MannyWidth;
	}
	
	/// <summary>
    /// clamps Manny to the Mouseposition
    /// </summary>
	void FixedUpdate () {
        Vector3 mousePos = _cam.ScreenToWorldPoint (Input.mousePosition);
        Vector3 targetPosition = new Vector3(mousePos.x, 1.0f, 0.0f);
        float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);
        targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
        transform.position = targetPosition;

        transform.position = new Vector2(Input.acceleration.x, 0);
    }
}