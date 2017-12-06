using Assets.Scripts.App.Game;
using Assets.Scripts.App.Data_Management;
using Assets.Scripts.App.Tracking.Table;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MillionaireController : GameController {

    [SerializeField]
    public Text QuestionText;

    [SerializeField]
    public Text PrizeText;

    [SerializeField]
    public Button[] Buttons;

    [SerializeField]
    public Slider Timer;

    [SerializeField]
    public Animator MannyAnimator;

    [SerializeField]
    public Animator LampAnimator;

    [SerializeField]
    public GameObject Summary;

    private QuestionController _questionController;
    private PrizeController _prizeController;

    private bool _escapeActive;
    private bool _gameStarted;
    private bool _gameCompleted;
    private bool _won;

    private float _experience;
    
    /// <summary>
    /// Gives the player the money he has won
    /// </summary>
    public override void OnUnload() {
        AppData.Instance().MannyAttribute.IncrementAttribute(Attribute.Coins, _prizeController.CurrentPrize);
        AppData.Instance().MannyAttribute.IncrementAttribute(Attribute.Experience, _experience);

        AppData.Instance().MannyAttribute.Save();

        DataSource.Insert(DataParams.
            Build("Won", _won ? 1 : 0).
            Append("CorrectAnswers", _questionController.CurrentQuestionIndex).
            Append("Prize", _prizeController.CurrentPrize).
            Append("Experience", _experience).
            Append("TimePlayedSeconds", Time.time));
        Tracking.RequestSend();
    }

    /// <summary>
    /// Retrieves 15 question from the database and fills an array with them
    /// </summary>
    protected override void BeforeLoad() {
        var table = new DataTable("Millionaire");
        table.AddProperty(new DataProperty("Won", DataProperty.DataPropertyType.TINYINT));
        table.AddProperty(new DataProperty("CorrectAnswers", DataProperty.DataPropertyType.INT));
        table.AddProperty(new DataProperty("Prize", DataProperty.DataPropertyType.INT));
        table.AddProperty(new DataProperty("Experience", DataProperty.DataPropertyType.INT));
        table.AddProperty(new DataProperty("TimePlayedSeconds", DataProperty.DataPropertyType.INT));
        SetDataSource(table);

        _questionController = new QuestionController();
        _prizeController = new PrizeController();

        new Handshake(HandshakeProtocol.Fetch).AddParameter("responseHandler", "millionaire").Shake((request) => {
            _questionController.LoadQuestions(request);
            UpdateUI();
        });
    }

    protected override void OnLoad() {
        //GameObject.Find("Loading Screen").SetActive(false);
    }

    /// <summary>
    /// Keeps updating the UI
    /// </summary>
    protected override void Update() {
        if (!_gameCompleted && _gameStarted) Timer.value -= Time.deltaTime;

        if (Timer.value <= 0)
            GameCompleted(false);
    }

    /// <summary>
    /// Resets and fills the question and answer objects with the current question
    /// </summary>
    private void UpdateUI() {
        var currentQuestion = _questionController.GetCurrentQuestion();

        UpdatePrize();
        QuestionText.text = currentQuestion.Difficulty + "  " + currentQuestion.Text;
        for (int i = 0; i < currentQuestion.Answers.Count; i++) {
            Buttons[i].interactable = true;
            Buttons[i].gameObject.SetActive(true);
            Buttons[i].GetComponentInChildren<Text>().text = currentQuestion.Answers[i].Text; ;
        }
        Timer.value = Timer.maxValue;
    }

    /// <summary>
    /// Updates the prize indicator's text with the current prize
    /// </summary>
    private void UpdatePrize() {
        PrizeText.text = _prizeController.CurrentPrize.ToString();
    }

    /// <summary>
    /// Method is called once the players wins the game. Updates the UI to the 'win screen'
    /// </summary>
    private void GameCompleted(bool won) {
        _gameCompleted = true;
        _won = won;

        if (won) {
            LampAnimator.Play("game_completed");
            FindObjectOfType<ParticleSystem>().Play();
            QuestionText.text = "Gefeliciteerd, je hebt gewonnen!";
        } else {
            _prizeController.CurrentPrize = _prizeController.StaticPrize;
            QuestionText.text = "Helaas, je hebt verloren!";
        }

        _experience = _prizeController.CurrentPrize * 0.35f;

        UpdatePrize();
        foreach (var button in Buttons) button.gameObject.SetActive(false);
        DisplaySummary(won);
    }

    /// <summary>
    /// Prepares and displays the summary panel. This method is called once the game is completed.
    /// </summary>
    /// <param name="won">Used to determine the color of the panel</param>
    private void DisplaySummary(bool won) {
        var color = won ? new Color32(7, 175, 106, 255) : new Color32(255, 0, 89, 255);

        Summary.GetComponent<Image>().color = color;

        List<Text> textObjects = new List<Text>(Summary.GetComponentsInChildren<Text>());

        textObjects.Find(x => x.name == "CorrectAnswers").text = _questionController.CurrentQuestionIndex.ToString();
        textObjects.Find(x => x.name == "MonnyEarned").text = _prizeController.CurrentPrize.ToString();
        textObjects.Find(x => x.name == "ExperienceEarned").text = _experience.ToString();
        textObjects.Find(x => x.name == "Text").color = color;

        Summary.gameObject.SetActive(true);
    }

    /// <summary>
    /// Event for when the player clicks an answer and checks if it was the right answer.
    /// </summary>
    /// <param name="index">The index of the clicked button</param>
    public void AnswerClick(int index) {
        MannyAnimator.Play("Talking", 2);

        var currentQuestion = _questionController.GetCurrentQuestion();
        if (_escapeActive || currentQuestion.Answers[index].IsAnswer) {
            LampAnimator.Play("Answer_true");

            if (!_escapeActive) _prizeController.IncreasePrize();
            _questionController.NextQuestion();
            _escapeActive = false;

            if (_questionController.AllQuestionsAnswered())
                GameCompleted(true);
            else
                UpdateUI();
        } else {
            LampAnimator.Play("Answer_false", 0, 0);
            MannyAnimator.Play("Sad", 2);
            GameCompleted(false);
        }
    }

    /// <summary>
    /// FiftyFifty is one of the usables that halves the answer options for the player.
    /// </summary>
    public void FiftyFifty() {
        var currentQuestion = _questionController.GetCurrentQuestion();
        var falseIndexes = currentQuestion.Answers.Where(x => !x.IsAnswer).Select(x => currentQuestion.Answers.IndexOf(x)).ToList();
        falseIndexes.RemoveAt(Random.Range(0, falseIndexes.Count - 1));

        foreach (var index in falseIndexes)
            Buttons[index].gameObject.SetActive(false);
    }

    public void Escape() {
        _escapeActive = true;
    }

    /// <summary>
    /// Generates random percentages for each answer how likely that answer is to be the right one. 
    /// Easy questions generate a high percentage for the right questions. The harder the questions 
    /// get, the closer the percentages get to each other.
    /// </summary>
    public void CrowdHelp() {
        var currentQuestion = _questionController.GetCurrentQuestion();
        var goodP = Random.Range(15 * (int)currentQuestion.Difficulty, 30 * (int)currentQuestion.Difficulty);
        var falseP = Random.Range(0, (100 - goodP));
        var falseP2 = Random.Range(0, 100 - (goodP + falseP));
        var falseP3 = Random.Range(0, 100 - (goodP + falseP + falseP2));

        var falsePercentages = new List<float>() {
            falseP, falseP2, falseP3
        };

        var percentage = 0;
        for (int i = 0; i < currentQuestion.Answers.Count; i++) {
            var answer = currentQuestion.Answers[i];

            if (answer.IsAnswer) percentage = goodP;
            else {
                percentage = (int)Mathf.Round(falsePercentages[Random.Range(0, falsePercentages.Count)]);
                falsePercentages.Remove(percentage);
            }
            Buttons[i].GetComponentInChildren<Text>().text = answer.Text + "(" + percentage + "%)";
        }
    }

    public void StartButton() {
        _gameStarted = true;
        GameObject.Find("Tutorial").SetActive(false);
    }

    /// <summary>
    /// Returns the player to the dashboard and gives him the current prize money
    /// </summary>
    public void BackButton() {
        AppData.Instance().Game.Unload();
    }
}
