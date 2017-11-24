using Assets.Scripts.App.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using Assets.Scripts.App;

/// <summary>
/// Struct for Office Objects
/// </summary>
[Serializable]
public struct OfficeObject {
    [SerializeField]
    public GameObject GameObject;
    public float MaxWidth { get; set; }
    [SerializeField]
    public int ObjectScore;
}

public class CatcherController : GameController {

    [SerializeField]
    public List<OfficeObject> Objects;

    [SerializeField]
    public EntityHandler EntityHandler;
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
    private IEnumerator SpawnOfficeObject() {
        ToggleObjects(true);
        if (!_gameStarted) yield return new WaitForSeconds(2.0f);
        _gameStarted = true;
        while (_timeLeft > 0) {
            foreach (var o in Objects) {
                var spawnPosition = new Vector3(
                UnityEngine.Random.Range(-o.MaxWidth, o.MaxWidth),
                o.GameObject.transform.position.y,
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

    /// <summary>
    /// loads the game and starts it
    /// </summary>
    protected override void OnLoad() {
        StartCoroutine(SpawnOfficeObject());
    }

    /// <summary>
    /// shuts down game and returns to menu
    /// </summary>
    public override void OnUnload() {
        var coins = Mathf.RoundToInt (EntityHandler.GameScore * 36/1080f);
        if (coins <= 0) {
            coins = 0;
        }
        AppData.Instance().MannyAttribute.IncrementAttribute(Attribute.Coins, coins);

        var experience = EntityHandler.GameScore * 30/1080;
        if (experience <= 0) {
            experience = 0;

        }
        AppData.Instance().MannyAttribute.IncrementAttribute(Attribute.Experience, experience);

        AppData.Instance().MannyAttribute.Save();
    }

    /// <summary>
    /// Checks if time is zero
    /// </summary>
    protected override void Update() {
        if (_timeLeft > 0) {
            _timeLeft -= Time.deltaTime;
            _TimerText.text = "Time Left:\n" + Mathf.RoundToInt(_timeLeft);
        } else {
            StopCoroutine(SpawnOfficeObject());
            ToggleObjects(false);
        }
    }

    /// <summary>
    /// Returns the player to the Main screen
    /// </summary>
    public void ExitButton() {
        AppData.Instance().Game.Unload();
    }

    /// <summary>
    /// Destroys gameObject when it collides with the collider
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
    }
}

