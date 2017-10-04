using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MobileUtil : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerSettings.statusBarHidden = false;
        Screen.sleepTimeout = 0;
	}
}
