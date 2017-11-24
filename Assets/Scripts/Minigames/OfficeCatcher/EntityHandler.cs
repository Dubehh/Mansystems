using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour {

    [SerializeField]
    public Text ScoreText;
    [SerializeField]
    public Text FinalExpText;
    public int GameScore;
    public int Experience;
    [SerializeField]
    public Text FinalScoreText;
    public bool Broken;


    public void CalcExperience() {
        Experience = GameScore * 30 / 1080;
        if (Experience <= 0) {
            Experience = 0;
        }
    }
    /// <summary>
    /// Destroys gameobject when it collides with collider
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        var prefix = "(Clone)";
        Destroy(other.gameObject);
        GameScore += FindObjectOfType<CatcherController>().Objects.Find(x => x.GameObject.name + prefix == other.gameObject.name).ObjectScore;
        Broken = other.gameObject.name.Contains("Broken");
        UpdateScore();

        //Object.isCought(true);
    }
    // Use this for initialization
    public void Start() {
        GameScore = 0;
        UpdateScore();
    }
    /// <summary>
    /// Updates score and experience texts
    /// </summary>
    public void UpdateScore() {
        ScoreText.text = "" + GameScore;
        FinalScoreText.text = "" + GameScore;
        CalcExperience();
        FinalExpText.text = "" + Experience;
    }
}