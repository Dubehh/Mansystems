using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWelcomeController : MonoBehaviour {

    [SerializeField]
    private Text _nameResult;
    [SerializeField]
    private Text _nameInput;
    [SerializeField]
    private GameObject[] _views;
    private int _current;
    private UIController _controller;

    // Use this for initialization
    private void Awake() {
        foreach (var obj in _views) obj.SetActive(false);
        _views[_current].SetActive(true);
        _controller = GetComponentInParent<UIController>();
        if (_controller.IsFirstTime()) {
            _controller.Footer.SetActive(false);
            _controller.Navigation.SetActive(false);
        }
    }

    /// <summary>
    /// Requests the next view in the queue
    /// </summary>
    public void Next() {
        _views[_current++].SetActive(false);
        _views[_current].SetActive(true);
    }

    /// <summary>
    /// Saves the input from the registration
    /// </summary>
    public void SaveInput() {
        PlayerPrefs.SetString("name", _nameInput.text);
        PlayerPrefs.SetString("uid", Guid.NewGuid().ToString());
        PlayerPrefs.Save();
        _nameResult.text = _nameInput.text;
    }

    /// <summary>
    /// Sends the registration request and loads the default game
    /// </summary>
    public void RequestSend() {
        _controller.Footer.SetActive(true);
        _controller.Navigation.SetActive(true);
        _controller.LoadDefault();
    }
	
}
