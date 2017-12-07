using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWelcomeController : MonoBehaviour {

    [SerializeField]
    public Text NameResult;
    [SerializeField]
    public Text NameInput;
    [SerializeField]
    public GameObject[] Views;
    private int _current;
    private UIController _controller;

    // Use this for initialization
    private void Awake() {
        foreach (var obj in Views) obj.SetActive(false);
        Views[_current].SetActive(true);
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
        Views[_current++].SetActive(false);
        Views[_current].SetActive(true);
    }

    /// <summary>
    /// Saves the input from the registration
    /// </summary>
    public void SaveInput() {
        PlayerPrefs.SetString("name", NameInput.text);
        PlayerPrefs.SetString("uid", Guid.NewGuid().ToString());
        PlayerPrefs.Save();
        NameResult.text = NameInput.text;
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
