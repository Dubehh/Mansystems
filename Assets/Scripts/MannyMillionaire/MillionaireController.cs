using Assets.Scripts.App;
using Assets.Scripts.App.Game;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MillionaireController : GameController {

    [SerializeField]
    public Text Text;

    [SerializeField]
    public Button[] Buttons;

    [SerializeField]
    public Slider Timer;

    private Question[] _questions;
    private Question _currentQuestion;

    private bool _escapeActive;

    /// <summary>
    /// Gives the player the money he has won
    /// </summary>
    public override void OnUnload() {

    }

    /// <summary>
    /// Retrieves 15 question from the database and fills an array with them
    /// </summary>
    protected override void BeforeLoad() {
        _currentQuestion = new Question("Hoe heet de school waar wij nu werkzaam zijn?",
            new Answer[4] {
                new Answer() { Text = "Deltion" },
                new Answer() { Text = "Cibap", IsAnswer = true },
                new Answer() { Text = "Basisschool de Schakel" },
                new Answer() { Text = "Windesheim" }
            }, Difficulty.Easy);
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
        Timer.value -= Time.deltaTime;
    }

    /// <summary>
    /// Resets and fills the question and answer objects with the current question
    /// </summary>
    private void UpdateUI() {
        Text.text = _currentQuestion.Text;
        for (int i = 0; i < _currentQuestion.Answers.Count; i++) {
            Buttons[i].gameObject.SetActive(true);

            var answer = _currentQuestion.Answers[i].Text;
            Buttons[i].GetComponentInChildren<Text>().text = answer;
        }
    }

    /// <summary>
    /// Event for when the player clicks an answer and checks if it was the right answer.
    /// </summary>
    /// <param name="index">The index of the clicked button</param>
    public void AnswerClick(int index) {
        if (_currentQuestion.Answers[index].IsAnswer) {
            //Next Question
            //Next prize level
        } else if (_escapeActive) {
            //Next Question
        } else
            //Game over

            foreach (var button in Buttons)
                button.interactable = false;
    }

    /// <summary>
    /// FiftyFifty is one of the usables that halves the answer options for the player.
    /// </summary>
    public void FiftyFifty() {
        var falseIndexes = _currentQuestion.Answers.Where(x => !x.IsAnswer).Select(x => _currentQuestion.Answers.IndexOf(x)).ToList();
        falseIndexes.RemoveAt(Random.Range(0, falseIndexes.Count - 1));

        foreach (var index in falseIndexes)
            Buttons[index].interactable = false;
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
