using UnityEngine;

public class ProfileManagement : MonoBehaviour {

    [SerializeField] public GameObject Main;

    private GameObject _currentView;

    /// <summary>
    /// Hides the current view and opens a new one
    /// </summary>
    /// <param name="view"></param>
    public void OpenView(GameObject view) {
        if (_currentView != null) _currentView.SetActive(false);
        _currentView = view;
        _currentView.SetActive(true);
    }

    /// <summary>
    /// Opens the main view
    /// </summary>
    public void OpenView() {
        OpenView(Main);
    }
}
