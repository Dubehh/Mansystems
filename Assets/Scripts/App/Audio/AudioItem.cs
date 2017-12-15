using System;
using UnityEngine;

namespace Assets.Scripts.App {
    /// <summary>
    ///     Represents a unique audioclip that can be played throughout the game
    /// </summary>
    [Serializable]
    public struct AudioItem {
        /// <summary>
        ///     The unique name/key of the audioclip.
        ///     This is used to identify each clip
        /// </summary>
        [SerializeField] public string Key;

        /// <summary>
        ///     The actual sound file
        /// </summary>
        [SerializeField] public AudioClip Clip;
    }
}