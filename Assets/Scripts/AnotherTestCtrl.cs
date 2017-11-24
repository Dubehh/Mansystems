using Assets.Scripts.App;
using Assets.Scripts.App.Data_Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherTestCtrl : MonoBehaviour {

    public void ClickMe() {
        Debug.Log("Starting test...");
        StartCoroutine(Test());
    }

    private IEnumerator Test() {
        Debug.Log("Loading test scene in 5 seconds..");
        yield return new WaitForSeconds(5);
        AppData.Instance().Game.Load("3");
    }
}
