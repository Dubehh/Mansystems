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

    private float _timeLeft;

    private bool _gameStarted;

    /// <summary>
    /// Loopt door de struct heen en vindt voor elk GameObject de Maxwidth
    /// </summary>
    protected override void BeforeLoad() {
        _timeLeft = 60;

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

    /// <summary>
    /// checkt de tijd en stopt als de tijd op 0 staat
    /// </summary>
    protected override void Update() {
        if (_timeLeft > 0) {
            _timeLeft -= Time.deltaTime;
        } else StopCoroutine(Spawn());
    }
    /// <summary>
    /// Spawnt de entities tot timeLeft 0 is
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spawn() {
        if (!_gameStarted) yield return new WaitForSeconds(2.0f);
        _gameStarted = true;
        while (_timeLeft > 0) {
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