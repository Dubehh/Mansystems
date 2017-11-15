using Assets.Scripts.App.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct CatcherObject {
    [SerializeField]
    public GameObject GameObject;
    public float MaxWidth { get; set; }
}

public class CatcherController : GameController {

    private Camera _cam;

    [SerializeField]
    public CatcherObject[] Objects;

    private float timeLeft;

    private bool _gameStarted;
    protected override void BeforeLoad() {
        timeLeft = 5;

        if (_cam == null) {
            _cam = Camera.main;
        }

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = _cam.ScreenToWorldPoint(upperCorner);

        for (var i = 0; i < Objects.Length; i++) {
            float width = Objects[i].GameObject.GetComponent<Renderer>().bounds.extents.x;
            Objects[i].MaxWidth = targetWidth.x - width;
        }
    }

    protected override void OnLoad() {
        StartCoroutine(Spawn());
    }


    public override void OnUnload() {

    }

    protected override void Update() {
        if (timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            Debug.Log(timeLeft);
        } else StopCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        Debug.Log("Spawning");
        if (!_gameStarted) yield return new WaitForSeconds(2.0f);
        _gameStarted = true;
        while (timeLeft > 0) {
            foreach (var o in Objects) {
                var spawnPosition = new Vector3(
                UnityEngine.Random.Range(-o.MaxWidth, o.MaxWidth),
                transform.position.y,
                0.0f);

                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(o.GameObject, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.0f));
            }
        }
    }
}