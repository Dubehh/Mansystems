using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void OnSettingChange() {
        _enabled = !_enabled;
        _image.sprite = _enabled ? _soundEnabled : _soundDisabled;
        AudioListener.pause = _enabled;
        AudioListener.volume = _enabled ? 1 : 0;
    }
}
