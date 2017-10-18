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
            _controller.GetFooterComponent().SetActive(false);
            _controller.GetNavigationComponent().SetActive(false);
        }
    }

    public void Next() {
        _views[_current++].SetActive(false);
        _views[_current].SetActive(true);
    }

    public void SaveInput() {
        PlayerPrefs.SetString("name", _nameInput.text);
        PlayerPrefs.Save();
        _nameResult.text = _nameInput.text;
    }

    public void RequestSend() {
        _controller.GetFooterComponent().SetActive(true);
        _controller.GetNavigationComponent().SetActive(true);
        _controller.LoadDefault();
    }
	
}
