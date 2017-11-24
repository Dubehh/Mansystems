using Assets.Scripts.App;
using Assets.Scripts.App.Data_Management;
using Assets.Scripts.App.Game;
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

    private QuestionController _questionController;
    private PrizeController _prizeController;

    private bool _escapeActive;
    private bool _gameCompleted;

    /// <summary>
    /// Gives the player the money he has won
    /// </summary>
    public override void OnUnload() {
        AppData.Instance().MannyAttribute.IncrementAttribute(Attribute.Coins, _prizeController.CurrentPrize);
        AppData.Instance().MannyAttribute.Save();
    }

    /// <summary>
    /// Retrieves 15 question from the database and fills an array with them
    /// </summary>
    protected override void BeforeLoad() {
        _questionController = new QuestionController();
        _prizeController = new PrizeController();

        for (int i = 0; i < System.Enum.GetValues(typeof(Difficulty)).Length; i++) {
            new Handshake(HandshakeProtocol.Response).AddParameter("query",
                "SELECT * " +
                "FROM MannyMillionaireQuestion " +
                "WHERE difficulty = " + i + " " +
                "ORDER BY rand() " +
                "LIMIT 5").Shake((request) => {
                    _questionController.AddQuestion(request, i);
                });
        }
    }

    /// <summary>
    /// Prepares the interface for the game
    /// </summary>
    protected override void OnLoad() {
        UpdateUI();
    }

    /// <summary>
    /// Keeps updating the UI
    /// </summary>
    protected override void Update() {
        if (!_gameCompleted) Timer.value -= Time.deltaTime;

        if (Timer.value <= 0)
            GameCompleted(false);

    }

    /// <summary>
    /// Resets and fills the question and answer objects with the current question
    /// </summary>
    private void UpdateUI() {
        var currentQuestion = _questionController.GetCurrentQuestion();

        UpdatePrize();
        QuestionText.text = currentQuestion.Text;
        for (int i = 0; i < currentQuestion.Answers.Count; i++) {
            Buttons[i].interactable = true;
            Buttons[i].gameObject.SetActive(true);
            Buttons[i].GetComponentInChildren<Text>().text = currentQuestion.Answers[i].Text; ;
        }
        Timer.value = Timer.maxValue;
    }

    private void UpdatePrize() {
        PrizeText.text = _prizeController.CurrentPrize.ToString();
    }

    /// <summary>
    /// Method is called once the players wins the game. Updates the UI to the 'win screen'
    /// </summary>
    private void GameCompleted(bool won) {
        if (won) {
            LampAnimator.Play("game_completed");
            FindObjectOfType<ParticleSystem>().Play();
            QuestionText.text = "Gefeliciteerd, je hebt gewonnen!";
        } else {
            _prizeController.CurrentPrize = _prizeController.StaticPrize;
            QuestionText.text = "Helaas, je hebt verloren!";
        }

        UpdatePrize();

        _gameCompleted = true;
        foreach (var button in Buttons) button.gameObject.SetActive(false);
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

    /// <summary>
    /// Returns the player to the dashboard and gives him the current prize money
    /// </summary>
    public void BackButton() {
        AppData.Instance().Game.Unload();
    }
}
