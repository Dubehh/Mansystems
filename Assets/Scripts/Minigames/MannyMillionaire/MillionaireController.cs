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

    private List<Question> _questions;
    private int _currentQuestionIndex;
    private Question _currentQuestion;

    private PrizeController _prizeController;

    private bool _escapeActive;
    private bool _gameCompleted;

    /// <summary>
    /// Gives the player the money he has won
    /// </summary>
    public override void OnUnload() {

    }

    /// <summary>
    /// Retrieves 15 question from the database and fills an array with them
    /// </summary>
    protected override void BeforeLoad() {
        _prizeController = new PrizeController();

        /*for (int i = 0; i < System.Enum.GetValues(typeof(Difficulty)).Length; i++) {
            new Handshake(HandshakeProtocol.Response).AddParameter("query",
                "SELECT * " +
                "FROM MannyMillionaireQuestion " +
                "WHERE difficulty = " + i + " " +
                "ORDER BY rand() " +
                "LIMIT 5").Shake((request) => {
                    _questions.Add(
                        new Question(
                            "Question",
                            new Answer[] {
                                new Answer { Text = "Answer 1" },
                                new Answer { Text = "Answer 2", IsAnswer = true },
                                new Answer { Text = "Answer 3"},
                                new Answer { Text = "Answer 4" }
                            }, (Difficulty)3 - i
                            ));
                });
        }*/

        _questions = new List<Question> {
            new Question("Wat voor gebiedsbenaming verdient Hattem?",
            new Answer[4] {
                new Answer() { Text = "Stad" },
                new Answer() { Text = "Dorp", IsAnswer = true },
                new Answer() { Text = "Gehucht" },
                new Answer() { Text = "Buurtschap" }
            }, Difficulty.Easy),

        new Question("Wie is de beste programmeur van Nederland?",
            new Answer[4] {
                new Answer() { Text = "Hugo Kamps", IsAnswer = true},
                new Answer() { Text = "Hugo Kamps", IsAnswer = true },
                new Answer() { Text = "Sylvana Simons" },
                new Answer() { Text = "Eelco EIKELboom" }
            }, Difficulty.Easy)};
        for (int i = 0; i < 13; i++) {
            if (_questions.Count < 15) {
                if (i % 2 == 0) _questions.Add(_questions[0]);
                else _questions.Add(_questions[1]);
            }
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
        if(!_gameCompleted) Timer.value -= Time.deltaTime;

        if (Timer.value <= 0)
            GameOver();

    }

    private void GameCompleted() {
        _gameCompleted = true;
        QuestionText.text = "Gefeliciteerd, je hebt gewonnen!";
        foreach (var button in Buttons) button.gameObject.SetActive(false);
    }

    private void GameOver() {
        QuestionText.text = "Helaas, je hebt verloren (gewonnen: €" + _prizeController.CurrentPrize + ")";
        foreach (var button in Buttons) button.gameObject.SetActive(false);
    }

    /// <summary>
    /// Resets and fills the question and answer objects with the current question
    /// </summary>
    private void UpdateUI() {
        _currentQuestion = _questions[_currentQuestionIndex];

        QuestionText.text = _currentQuestion.Text;
        for (int i = 0; i < _currentQuestion.Answers.Count; i++) {
            Buttons[i].interactable = true;
            Buttons[i].gameObject.SetActive(true);
            var answer = _currentQuestion.Answers[i].Text;
            Buttons[i].GetComponentInChildren<Text>().text = answer;
        }

        PrizeText.text = "€" + _prizeController.CurrentPrize;
    }

    /// <summary>
    /// Event for when the player clicks an answer and checks if it was the right answer.
    /// </summary>
    /// <param name="index">The index of the clicked button</param>
    public void AnswerClick(int index) {
        if (_escapeActive || _currentQuestion.Answers[index].IsAnswer) {
            if (!_escapeActive) _prizeController.IncreasePrize();

            _currentQuestionIndex += 1;
            _escapeActive = false;
            if (_currentQuestionIndex >= _questions.Count) {
                GameCompleted();
            } else {
                Timer.value = Timer.maxValue;
                UpdateUI();
            }
        } else {
            GameOver();
        }
    }

    /// <summary>
    /// FiftyFifty is one of the usables that halves the answer options for the player.
    /// </summary>
    public void FiftyFifty() {
        var falseIndexes = _currentQuestion.Answers.Where(x => !x.IsAnswer).Select(x => _currentQuestion.Answers.IndexOf(x)).ToList();
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
        var goodP = Random.Range(15 * (int)_currentQuestion.Difficulty, 30 * (int)_currentQuestion.Difficulty);
        var falseP = Random.Range(0, (100 - goodP));
        var falseP2 = Random.Range(0, 100 - (goodP + falseP));
        var falseP3 = Random.Range(0, 100 - (goodP + falseP + falseP2));

        var falsePercentages = new List<float>() {
            falseP, falseP2, falseP3
        };

        var percentage = 0;
        for (int i = 0; i < _currentQuestion.Answers.Count; i++) {
            var answer = _currentQuestion.Answers[i];

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
