using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour {

    [SerializeField]
    public Text ScoreText;

    [SerializeField]
    public Text FinalExpText;
    
    public int GameScore { get; set; }
    public int Experience { get; set; }

    [SerializeField]
    public Text FinalScoreText;

    [SerializeField]
    public bool Broken;

    [SerializeField]
    public bool Logo;

    [SerializeField]
    public bool FakeLogo;
    
    public float LogosCaught { get; set; }
    public float FakeLogosCaught { get; set; }

    [SerializeField]
    public Text AmountOfCustomers;

    [SerializeField]
    public Text FinalAmountOfFakeCustomers;

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
        AmountOfCustomers.text = "" + LogosCaught;
        FinalAmountOfFakeCustomers.text = "" + FakeLogosCaught;
    }

    /// <summary>
    /// Calculates the player's experience according to his score
    /// </summary>
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
        Logo = other.gameObject.name.Contains("Customer");
        FakeLogo = other.gameObject.name.Contains("FakeLogo");
        UpdateScore();
    }
}