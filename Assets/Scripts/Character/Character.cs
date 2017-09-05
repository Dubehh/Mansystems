using System;
using UnityEngine;

namespace Assets.Scripts.Character {
    public class Character : MonoBehaviour {
        public static readonly string ColliderTag = "Ground";
        private CharacterTranslation _translation;

        public float Speed { get; set; }
        public float JumpForce { get; set; }
        public Camera camera;

        private void Awake() {
            _translation = new CharacterTranslation(this);
            JumpForce = 20.0f;
            Speed = 10.0f;
            camera = GetComponentInChildren<Camera>();
        }

        private void FixedUpdate() {
            _translation.Update();
            camera.transform.position.Set(camera.transform.position.x, transform.position.y, 1);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.tag == ColliderTag && _translation.Airborne) {
                _translation.Airborne = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.name == "end") {
                Debug.Log("end");
            }
        }
    }
}