using Assets.Scripts.App;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTestCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Starting test...");
        StartCoroutine(Test());

	}

    private IEnumerator Test() {
        Debug.Log("Loading test scene in 5 seconds..");
        yield return new WaitForSeconds(5);
        AppData.Instance().Game.Load("3");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
