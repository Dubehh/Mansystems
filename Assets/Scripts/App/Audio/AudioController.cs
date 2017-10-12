using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.App {
    public class AudioController : MonoBehaviour {

        [SerializeField]
        public AudioItem[] Items;

        private Dictionary<string, AudioClip> _items;
        private AudioSource _source;

        private void Awake() {
            _source = GetComponent<AudioSource>();
            _items = new Dictionary<string, AudioClip>();
            foreach (var item in Items)
                _items[item.Key.ToLower()] = item.Clip;
        }

        /// <summary>
        /// Plays the audiclip that is linked to the given name.
        /// This name should be unique.
        /// </summary>
        /// <param name="name">The name of the clip</param>
        public void Play(string name) {
            if (_items.ContainsKey(name.ToLower())) {
                _source.clip = _items[name.ToLower()];
                _source.Play();
            }
            name = "return"; //Plays when player returns to the dashboard
            name = "fwd"; //Plays when player goes away from dashboard into other screen
            name = "lvl"; //Plays when player levels up
            name = "startup"; //Plays when game starts up
            name = "coinsound"; //Plays when player picks up 
            name = "button"; //Plays when player presses a button
            name = "notif"; //Plays when notification appears ingame
            name = "vsound"; //Plays when player wins the game, plays when popup for replay/quit appears
            name = "lsound"; //Plays when player loses the game, plays when popup for replay/quit appears
            name = "cjfs"; //Plays when player falls off off the platform
            }
    }
}
