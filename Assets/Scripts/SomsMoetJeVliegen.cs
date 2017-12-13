using Assets.Scripts.App.Data_Management.Handshakes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomsMoetJeVliegen : MonoBehaviour {
    FileProtocolQueue queue;
    // Use this for initialization
    void Start() {

        queue = new FileProtocolQueue(
            fpQueue => {
                Debug.Log("Queue complete");
                Debug.Log("Requests completed: " + fpQueue.Queue.Count);
            }, 
            fpComplete => {
                Debug.Log("Request sent success! -- Requests left: " + queue.Count);
            }
        );

        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder").AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);
        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder").AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);
        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder").AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);
        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder").AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);
        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder").AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);
        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder").AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);

        queue.Commit();

    }

    // Update is called once per frame
    void Update() {

    }
}
