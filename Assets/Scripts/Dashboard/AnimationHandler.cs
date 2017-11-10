using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler {

    private Animator _animator; 

	public void SetAnimator(GameObject manny) {
        _animator = manny.GetComponent<Animator>();
    }

	public void ScanInput() {
        if (Input.GetKeyDown(KeyCode.A)) _animator.Play("Waving", 2);
        if (Input.GetKeyDown(KeyCode.B)) _animator.Play("Waving 2_0", 2);
	}
}
