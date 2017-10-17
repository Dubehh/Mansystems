using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This controller manages the audio settings interaction.
/// It updates a sprite upon muting/unmuting the audio
/// </summary>
public class AudioSettingController : MonoBehaviour {

    [SerializeField]
    private Sprite _soundEnabled;
    [SerializeField]
    private Sprite _soundDisabled;
    private Image _image;
    private bool _enabled;

    private void Start() {
        _enabled = true;
        _image = GetComponentInChildren<Image>();
    }

    /// <summary>
    /// Fires upon hitting the mute/unmute button
    /// </summary>
    public void OnSettingChange() {
        _enabled = !_enabled;
        _image.sprite = _enabled ? _soundEnabled : _soundDisabled;
        AudioListener.pause = _enabled;
        AudioListener.volume = _enabled ? 1 : 0;
    }
}
