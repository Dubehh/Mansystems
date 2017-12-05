using Assets.Scripts.App.Game;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FinderController : GameController {

    [SerializeField]
    public RawImage Match;

    public override void OnUnload() {
    }

    protected override void BeforeLoad() {
    }

    protected override void OnLoad() {
    }

    protected override void Update() {
        if (Input.touchCount == 1) {
            Debug.Log(Input.touches[0].position);
            Match.transform.Translate(0, 0, 0);           

        }
    }
}
