using UnityEngine;

public class ProfileSetup : MonoBehaviour {

    [SerializeField]
    private GameObject[] _steps;

    private int _currentStepIndex;

    public void NextStep() {
        if (_currentStepIndex == _steps.Length - 1) gameObject.SetActive(false);
        _steps[_currentStepIndex].SetActive(false);
        _currentStepIndex += 1;
        _steps[_currentStepIndex].SetActive(true);
    }
}
