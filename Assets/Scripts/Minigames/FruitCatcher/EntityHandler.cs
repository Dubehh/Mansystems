using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHandler : MonoBehaviour {

    /// <summary>
    /// Destroys gameobject when it collides with collider
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D (Collider2D other)
    {
        Destroy(other.gameObject);
    }
}