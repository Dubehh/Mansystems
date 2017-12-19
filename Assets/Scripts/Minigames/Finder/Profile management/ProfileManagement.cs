using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManagement : MonoBehaviour {

    [SerializeField] public GameObject Main;

    private GameObject _currentView;

    public void OpenView(GameObject view) {
        if (_currentView != null) _currentView.SetActive(false);
        _currentView = view;
        _currentView.SetActive(true);
    }

    public void OpenView() {
        OpenView(Main);
    }
}
