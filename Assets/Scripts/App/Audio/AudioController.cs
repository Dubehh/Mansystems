using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.App.Audio {
    public class AudioController : MonoBehaviour {
        private Dictionary<string, AudioClip> _items;
        private AudioSource _source;

        [SerializeField] public AudioItem[] Items;

        private void Awake() {
            _source = GetComponent<AudioSource>();
            _items = new Dictionary<string, AudioClip>();
            foreach (var item in Items)
                _items[item.Key.ToLower()] = item.Clip;
        }

        /// <summary>
        ///     Plays the audiclip that is linked to the given name.
        ///     This name should be unique.
        /// </summary>
        /// <param name="name">The name of the clip</param>
        public void Play(string name) {
            if (!_items.ContainsKey(name.ToLower())) return;
            _source.clip = _items[name.ToLower()];
            _source.Play();
        }
    }
}