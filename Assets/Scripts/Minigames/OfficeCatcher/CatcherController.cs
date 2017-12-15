using Assets.Scripts.App.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.App.Tracking.Table;
using UnityEngine.UI;

/// <summary>
/// Struct for Office Objects
/// </summary>
[Serializable]
public class OfficeObject {

    [SerializeField]
    public GameObject GameObject;

    [SerializeField]
    public int ObjectScore;

    public float MaxWidth { get; set; }

    [SerializeField]
    public bool IsBroken;

    [SerializeField]
    public bool IsLogo;

    [SerializeField]
    public bool IsFakeLogo;
    public int ID { get; set; }

}

public class CatcherController : GameController {

    [SerializeField]
    public List<OfficeObject> Objects;

    [SerializeField]
    public CollisionHandler CollisionHandler;

    [SerializeField]
    public List<GameObject> Lives;

    [SerializeField]
    public GameObject GameOverScreen;

    [SerializeField]
    public GameObject StopButton;

    [SerializeField]
    public int LifeLeft;

    [SerializeField]
    public Text AmountOfCustomers;

    [SerializeField]
    public Text FinalAmountOfFakeCustomers;

    [SerializeField]
    public Text FinalExpText;

    [SerializeField]
    public Text FinalScoreText;

    [SerializeField]
    public Text ScoreText;

    [SerializeField]
    public int GameScore;

    public int Experience { get; set; }
    public float LogosCaught { get; set; }
    public float FakeLogosCaught { get; set; }

    private Camera _cam;
    private bool _gameStarted;

    public Dictionary<GameObject, OfficeObject> ObjectRegister { get; private set; }

    /// <summary>
    /// shuts down game and returns to menu
    /// </summary>
    public override void OnUnload() {
        var coins = Mathf.RoundToInt(GameScore * 36 / 1080f);
        coins = coins <= 0 ? 0 : coins;

        AppData.Instance().MannyAttribute.IncrementAttribute(Attribute.Coins, coins);

        var experience = GameScore * 30 / 1080;
        experience = experience <= 0 ? 0 : experience;

        AppData.Instance().MannyAttribute.IncrementAttribute(Attribute.Experience, experience);
        AppData.Instance().MannyAttribute.Save();

        DataSource.Insert(DataParams.
            Build("Points", GameScore).
            Append("ExperienceGained", experience).
            Append("Coins", coins).
            Append("LogosCaught", LogosCaught).
            Append("FakeLogosCaught", FakeLogosCaught).
            Append("TimePlayedSeconds", Time.time));
        Tracking.RequestSend();
    }

    /// <summary>
    /// loops through struct and finds Maxwidth for every GameObject
    /// </summary>
    protected override void BeforeLoad() {
        _cam = Camera.main;
        ObjectRegister = new Dictionary<GameObject, OfficeObject>();

        var upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        var targetWidth = _cam.ScreenToWorldPoint(upperCorner);

        for (var i = 0; i < Objects.Count; i++) {
            var width = Objects[i].GameObject.GetComponent<Renderer>().bounds.extents.x;
            var obj = Objects[i];
            obj.MaxWidth = targetWidth.x - width;
            Objects[i] = obj;
        }

        var source = new DataTable("Catcher");
        source.AddProperty(new DataProperty("Points", DataProperty.DataPropertyType.INT));
        source.AddProperty(new DataProperty("ExperienceGained", DataProperty.DataPropertyType.INT));
        source.AddProperty(new DataProperty("Coins", DataProperty.DataPropertyType.INT));
        source.AddProperty(new DataProperty("LogosCaught", DataProperty.DataPropertyType.INT));
        source.AddProperty(new DataProperty("FakeLogosCaught", DataProperty.DataPropertyType.INT));
        source.AddProperty(new DataProperty("TimePlayedSeconds", DataProperty.DataPropertyType.INT));
        SetDataSource(source);
        Prepare();
    }

    /// <summary>
    /// loads the game and starts it
    /// </summary>
    protected override void OnLoad() {
        StartCoroutine(SpawnOfficeObject());
    }

    /// <summary>
    /// Checks if time is zero
    /// </summary>
    protected override void Update() {
        UpdateScore();
    }

    /// <summary>
    /// Spawns entities until timeLeft is zero
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnOfficeObject() {
        ToggleObjects(true);
        if (!_gameStarted) yield return new WaitForSeconds(2.0f);
        _gameStarted = true;
        while (LifeLeft > 0) {
            foreach (var o in Objects) {
                if (o.GameObject == null) continue;
                var spawnPosition = new Vector3(
                UnityEngine.Random.Range(-o.MaxWidth, o.MaxWidth),
                o.GameObject.transform.position.y,
                0.0f);

                var spawnRotation = Quaternion.identity;
                ObjectRegister[Instantiate(o.GameObject, spawnPosition, spawnRotation)] = o;
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.0f));
            }
        }
    }

    /// <summary>
    /// shows remaining lives of player
    /// </summary>
    private void ShowLives() {
        Lives.ForEach(x => x.SetActive(false));
        Lives.GetRange(0, LifeLeft).ForEach(x => x.SetActive(true));

        if (LifeLeft <= 0) {
            StopCoroutine(SpawnOfficeObject());
            ToggleObjects(false);
            StopGame();
        }
    }

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
    /// Updates score and experience texts
    /// </summary>
    public void UpdateScore() {
        ScoreText.text = "" + GameScore;
        FinalScoreText.text = "" + GameScore;
        FinalExpText.text = "" + Experience;
        AmountOfCustomers.text = "" + LogosCaught;
        FinalAmountOfFakeCustomers.text = "" + FakeLogosCaught;
    }

    /// <summary>
    /// updates lives of player
    /// </summary>
    public void Updatelife() {
        LifeLeft--;
        ShowLives();
    }

    /// <summary>
    /// activates game over screen
    /// </summary>
    public void StopGame() {
        LifeLeft = 0;
        StopCoroutine(SpawnOfficeObject());
        StopButton.SetActive(false);
        UpdateScore();
        GameOverScreen.SetActive(true);
        ToggleObjects(false);
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