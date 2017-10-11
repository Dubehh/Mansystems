using Assets.Scripts.App.Tracking;
using Assets.Scripts.App.Tracking.Table;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour {

    public DataTableRegistry Registry { get; set; }

	// Use this for initialization
	private void Awake () {
        Registry = new DataTableRegistry();
	}
	
	// Update is called once per frame
	private void Start () {
    }
}
