using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.App {
    public class AudioController : MonoBehaviour {

        [SerializeField]
        public AudioItem[] Items;
        private Dictionary<string, AudioSource> _items;

        private void Awake() {
            _items = new Dictionary<string, AudioSource>();
            foreach (var item in Items)
                _items[item.Key.ToLower()] = item.Source;
        }

        public void Play(string name) {
            if (_items.ContainsKey(name.ToLower()))
                _items[name.ToLower()].Play();
        }
    }
}
