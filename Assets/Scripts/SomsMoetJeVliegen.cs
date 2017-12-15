using Assets.Scripts.App.Data_Management.Handshakes;
using UnityEngine;

public class SomsMoetJeVliegen : MonoBehaviour {
    private FileProtocolQueue queue;

    // Use this for initialization
    private void Start() {
        queue = new FileProtocolQueue(
            fpQueue => {
                Debug.Log("Queue complete");
                Debug.Log("Requests completed: " + fpQueue.Queue.Count);
            },
            fpComplete => { Debug.Log("Request sent success! -- Requests left: " + queue.Count); }
        );

        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder")
            .AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);
        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder")
            .AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);
        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder")
            .AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);
        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder")
            .AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);
        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder")
            .AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);
        queue.Attach(new FileProtocol(Protocol.Download, this).Target("finder")
            .AddParameter("name", "7e2c365d34e2463.png") as FileProtocol);

        queue.Commit();
    }

    // Update is called once per frame
    private void Update() {
    }
}