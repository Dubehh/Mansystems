using UnityEngine;

namespace Assets.Scripts.Character {
    public class CharacterTranslation {
        private readonly Character _character;
        private Vector3 _momentum;

        public CharacterTranslation(Character character) {
            _character = character;
            Airborne = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public bool Airborne { get; set; }

        //Handles the basic movement of the player + the jumping
        public void Update() {
            var vertical = Input.GetAxis("Vertical") * _character.Speed * Time.deltaTime;
            var horizontal = Input.GetAxis("Horizontal") * _character.Speed * Time.deltaTime;

            //Allow movement if the player is not in the air
            var translation = new Vector3(horizontal, 0, vertical);
            _character.transform.Translate(translation);

            if (Input.GetKeyDown(KeyCode.Space) && !Airborne) {
                //Space is pressed and the player is NOT in the air
                Airborne = true;
                _character.GetComponent<Rigidbody2D>().velocity += _character.JumpForce * Vector2.up;
            }
        }
    }
}