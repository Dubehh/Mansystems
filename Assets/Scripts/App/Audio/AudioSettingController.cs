using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     This controller manages the audio settings interaction.
///     It updates a sprite upon muting/unmuting the audio
/// </summary>
public class AudioSettingController : MonoBehaviour {
    private bool _enabled;
    private Image _image;

    [SerializeField] public Sprite SoundDisabled;

    [SerializeField] public Sprite SoundEnabled;

    private void Start() {
        _enabled = true;
        _image = GetComponentInChildren<Image>();
    }

    /// <summary>
    ///     Fires upon hitting the mute/unmute button
    /// </summary>
    public void OnSettingChange() {
        _enabled = !_enabled;
        _image.sprite = _enabled ? SoundEnabled : SoundDisabled;
        AudioListener.pause = _enabled;
        AudioListener.volume = _enabled ? 1 : 0;
    }
}