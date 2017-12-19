using UnityEngine;

public class DashboardAnimationHandler {

    private Animator _animator;
    private GameObject _manny;

    public void SetAnimator(GameObject manny) {
        _animator = manny.GetComponent<Animator>();
        _manny = manny;
    }

    public void ScanInput() {
        if (Input.GetKeyDown(KeyCode.A)) _animator.Play("Waving", 2);
        if (Input.GetKeyDown(KeyCode.B)) _animator.Play("Waving 2_0", 2);

        _manny.transform.Rotate(new Vector3(0, 0, Input.acceleration.x));
    }
}