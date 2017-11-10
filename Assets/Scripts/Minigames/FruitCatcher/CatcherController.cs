using Assets.Scripts.App.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CatcherController : GameController {

    private Camera _cam;

    [SerializeField]
    public GameObject Mouse;
    [SerializeField]
    public GameObject Stapler;
    [SerializeField]
    public GameObject Keyboard;
    [SerializeField]
    public GameObject Monitor;
    [SerializeField]
    public GameObject Printer;

    private float timeLeft;
    private float maxWidth;

    protected override void BeforeLoad() {
        // initialisatie
        /// <summary>
        /// calculates time left for the game to be played
        /// </summary>
        timeLeft -= Time.deltaTime;

        /// <summary>
        /// Defines Camera
        /// Renders GameObjects
        /// </summary>
        if (_cam == null) {
            _cam = Camera.main;
        }

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = _cam.ScreenToWorldPoint(upperCorner);

        //dit kan in array
        float mouseWidth = Mouse.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - mouseWidth; //defines max movementspace for mouseobject

        float staplerWidth = Stapler.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - staplerWidth; //defines max movementspace for staplerobject

        float keyboardWidth = Keyboard.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - keyboardWidth; //defines max movementspace for keyboardobject

        float monitorWidth = Monitor.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - monitorWidth; //defines max movementspace for monitorobject

        float printerWidth = Printer.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - printerWidth; //defines max movementspace for printerobject

        /// <summary>
        /// handles spawning of Game Objects
        /// </summary>
        /// <returns></returns>
        
    }

    protected override void OnLoad() {
        // starten game / timer
        IEnumerator Spawn() {
            yield return new WaitForSeconds(2.0f);
            while (timeLeft > 0) {

                //kan in array
                Vector3 spawnPositionmouse = new Vector3(
                    UnityEngine.Random.Range(-maxWidth, maxWidth),
                    transform.position.y,
                    0.0f);
                Vector3 spawnPositionstapler = new Vector3(
                    UnityEngine.Random.Range(-maxWidth, maxWidth),
                    transform.position.y,
                    0.0f);
                Vector3 spawnPositionkeyboard = new Vector3(
                    UnityEngine.Random.Range(-maxWidth, maxWidth),
                    transform.position.y,
                    0.0f);
                Vector3 spawnPositionmonitor = new Vector3(
                    UnityEngine.Random.Range(-maxWidth, maxWidth),
                    transform.position.y,
                    0.0f);
                Vector3 spawnPositionprinter = new Vector3(
                    UnityEngine.Random.Range(-maxWidth, maxWidth),
                    transform.position.y,
                    0.0f);


                //kan in een functie
                Quaternion _spawnRotationmouse = Quaternion.identity;
                Instantiate(Mouse, spawnPositionmouse, _spawnRotationmouse);
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.0f));

                Quaternion _spawnRotationstapler = Quaternion.identity;
                Instantiate(Stapler, spawnPositionstapler, _spawnRotationstapler);
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.0f));

                Quaternion _spawnRotationkeyboard = Quaternion.identity;
                Instantiate(Keyboard, spawnPositionkeyboard, _spawnRotationkeyboard);
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.0f));

                Quaternion _spawnRotationmonitor = Quaternion.identity;
                Instantiate(Monitor, spawnPositionmonitor, _spawnRotationmonitor);
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.0f));

                Quaternion _spawnRotationprinter = Quaternion.identity;
                Instantiate(Printer, spawnPositionprinter, _spawnRotationprinter);
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.0f));
            }
            StartCoroutine(Spawn());

        }
    }

    public override void OnUnload() {
        // opruimen
        throw new System.NotImplementedException();
    }

    protected override void Update() {
        throw new System.NotImplementedException();
    }
}
