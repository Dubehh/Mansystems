using Assets.Scripts.App.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

[Serializable]
public struct CatcherObject {
    [SerializeField]
    public GameObject GameObject;
    public float MaxWidth { get; set; }
    [SerializeField]
    public int ObjectScore;
}

public class CatcherController : GameController {

    [SerializeField]
    public List<CatcherObject> Objects;
    private Camera _cam;
    public Text _TimerText;
    [SerializeField]
    private float _timeLeft;
    private bool _gameStarted;

    /// <summary>
    /// Sets objects active, used for disrupting coroutine (spawn)
    /// </summary>
    /// <param name="active"></param>
    private void ToggleObjects(bool active) {
        foreach (var obj in Objects) {
            obj.GameObject.SetActive(active);
        }
    }

    /// <summary>
    /// Spawns entities until timeLeft is zero
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spawn() {
        ToggleObjects(true);
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

    /// <summary>
    /// loops through struct and finds Maxwidth for every GameObject
    /// </summary>
    protected override void BeforeLoad() {
        _cam = Camera.main;

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = _cam.ScreenToWorldPoint(upperCorner);

        for (var i = 0; i < Objects.Count; i++) {
            float width = Objects[i].GameObject.GetComponent<Renderer>().bounds.extents.x;
            var obj = Objects[i];
            obj.MaxWidth = targetWidth.x - width;
            Objects[i] = obj;
        }
    }

    protected override void OnLoad() {
        StartCoroutine(Spawn());
    }

    public override void OnUnload() {

    }

    /// <summary>
    /// Checks if time is zero
    /// </summary>
    protected override void Update() {
        if (_timeLeft > 0) {
            _timeLeft -= Time.deltaTime;
            _TimerText.text = "Time Left:\n" + Mathf.RoundToInt(_timeLeft);
        } else {
            StopCoroutine(Spawn());
            ToggleObjects(false);
        }
    }

    /// <summary>
    /// Destroys gameObject when it collides with the collider
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
    }
}