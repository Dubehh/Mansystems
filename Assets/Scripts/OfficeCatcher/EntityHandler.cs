using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHandler : MonoBehaviour {

    [SerializeField]
    public Text ScoreText;
    public int GameScore;

    /// <summary>
    /// Destroys gameobject when it collides with collider
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        var prefix = "(Clone)";
        Destroy(other.gameObject);
        GameScore += FindObjectOfType<CatcherController>().Objects.Find(x => x.GameObject.name + prefix == other.gameObject.name).ObjectScore;
        UpdateScore();

        //Object.isCought(true);
    }
    // Use this for initialization
    void Start() {
        GameScore = 0;
        UpdateScore();
    }
    /// <summary>
    /// Updates score text
    /// </summary>
    void UpdateScore() {
        ScoreText.text = "" + GameScore;
    }
}
