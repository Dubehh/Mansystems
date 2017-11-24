﻿using Assets.Scripts.App.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using Assets.Scripts.App.Tracking.Table;

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
    public bool IsBroken;
}

public class CatcherController : GameController {

    [SerializeField]
    public List<OfficeObject> Objects;
    [SerializeField]
    public CollisionHandler CollisionHandler;
    private Camera _cam;
    [SerializeField]
    private float _lifeLeft;
    private bool _gameStarted;
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    public GameObject GameOverScreen;
    public GameObject StopButton;


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
    /// activates game over screen
    /// </summary>
    public void StopGame() {
        _lifeLeft = 0;
        StopCoroutine(SpawnOfficeObject());
        StopButton.SetActive(false);
        CollisionHandler.UpdateScore();
        GameOverScreen.SetActive(true);
        ToggleObjects(false);
    }

    /// <summary>
    /// updates lives of player
    /// </summary>
    private void Updatelife() {
        if (CollisionHandler.Broken == true) {
            _lifeLeft = _lifeLeft - 1;
            CollisionHandler.Broken = false;
            ShowLives();
        }
    }

    /// <summary>
    /// shows remaining lives of player
    /// </summary>
    private void ShowLives() {
        if (_lifeLeft == 3) {
            Life3.SetActive(true);
            Life2.SetActive(true);
            Life1.SetActive(true);
        }
        if (_lifeLeft == 2) {
            Life3.SetActive(false);
            Life2.SetActive(true);
            Life1.SetActive(true);
        }
        if (_lifeLeft == 1) {
            Life3.SetActive(false);
            Life2.SetActive(false);
            Life1.SetActive(true);

        } else if (_lifeLeft == 0) {
            StopCoroutine(SpawnOfficeObject());
            Life3.SetActive(false);
            Life2.SetActive(false);
            Life1.SetActive(false);
            ToggleObjects(false);
            StopGame();
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
        while (_lifeLeft > 0) {
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

        var source = new DataTable("Catcher");
        source.AddProperty(new DataProperty("Points", DataProperty.DataPropertyType.INT));
        source.AddProperty(new DataProperty("ExperienceGained", DataProperty.DataPropertyType.INT));
        source.AddProperty(new DataProperty("Coins", DataProperty.DataPropertyType.INT));
        SetDataSource(source);
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
        var coins = Mathf.RoundToInt (CollisionHandler.GameScore * 36/1080f);
        if (coins <= 0) {
            coins = 0;
        }
        AppData.Instance().MannyAttribute.IncrementAttribute(Attribute.Coins, coins);

        var experience = CollisionHandler.GameScore * 30/1080;
        if (experience <= 0) {
            experience = 0;

        }
        AppData.Instance().MannyAttribute.IncrementAttribute(Attribute.Experience, experience);
        AppData.Instance().MannyAttribute.Save();

        DataSource.Insert(DataParams.Build("Points", CollisionHandler.GameScore).
            Append("ExperienceGained", experience).
            Append("Coins", coins));

        Tracking.RequestSend();
    }

    /// <summary>
    /// Checks if time is zero
    /// </summary>
    protected override void Update() {
        Updatelife();
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

