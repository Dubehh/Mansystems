using Assets.Scripts.App.Game;
using Assets.Scripts.App.Tracking;
using Assets.Scripts.App.Tracking.Table;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour {

    public DataTableRegistry Registry { get; private set; }
    public GameManager Game { get; private set; }

	// Use this for initialization
	private void Awake () {
        Game = new GameManager();
        Registry = new DataTableRegistry();
	}
	
	// Update is called once per frame
	private void Start () {
    }
}
