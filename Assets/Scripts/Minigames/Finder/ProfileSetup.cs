using Assets.Scripts.App.Data_Management;
using UnityEngine;

public class ProfileSetup : MonoBehaviour {

    [SerializeField]
    private GameObject[] _steps;

    private int _currentStepIndex;

    private void Start() {
        new Handshake(HandshakeProtocol.Fetch).AddParameter("responseHandler", "finder").Shake((request) => {
            // Check if current player already has profile
            // Yes --> Hide
            // No --> Continue profile setup
        });
    }

    /// <summary>
    /// Disables the current panel and activates the next panel from the _steps list
    /// </summary>
    public void NextStep() {
        if (_currentStepIndex == _steps.Length - 1) {
            gameObject.SetActive(false);
            return;
        }

        _steps[_currentStepIndex].SetActive(false);
        _currentStepIndex += 1;
        _steps[_currentStepIndex].SetActive(true);
    }


}
