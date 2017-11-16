using Assets.Scripts.App;
using Assets.Scripts.App.Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMMController : GameController {

    private Question CurrentQuestion;

    [SerializeField]
    public Text Text;

    [SerializeField]
    public Button[] Buttons;

    public override void OnUnload() {

    }

    protected override void BeforeLoad() {
        CurrentQuestion = new Question("Hoe heet de school waar wij nu werkzaam zijn?",
            new Answer[4] {
                new Answer() {
                    Text = "Deltion",
                },

                new Answer() {
                    Text = "Cibap",
                    IsAnswer = true
                },

                new Answer() {
                    Text = "Basisschool de Schakel",
                },

                new Answer() {
                    Text = "Windesheim",
                }
            }, QuestionCategory.Easy);
    }

    protected override void OnLoad() {
        UpdateUI();
    }

    protected override void Update() {
    }

    private void UpdateUI() {
        Text.text = CurrentQuestion.Text;
        for (int i = 0; i < CurrentQuestion.Answers.Length; i++) {
            var answer = CurrentQuestion.Answers[i].Text;
            Buttons[i].GetComponentInChildren<Text>().text = answer;
        }
    }

    public void OnButtonClick(int index) {
        if (CurrentQuestion.Answers[index].IsAnswer) {
            //Next Question
        } else {
            //Game over
        }

        foreach (var button in Buttons) {
            button.interactable = false;
        }
    }

    public void FiftyFifty() {
        List<int> falseAnswerIndexes = new List<int>();
        for (int i = 0; i < CurrentQuestion.Answers.Length; i++) {
            var answer = CurrentQuestion.Answers[i];
            if (!answer.IsAnswer) falseAnswerIndexes.Add(i);
        }

        falseAnswerIndexes.RemoveAt(Random.Range(0, falseAnswerIndexes.Count - 1));

        foreach(var index in falseAnswerIndexes) {
            Buttons[index].interactable = false;
        }
    }

    public void HelpLine() {

    }

    public void CrowdHelp() {
        Debug.Log("CrowdHelp");

        
        var goodP = Random.Range(15 * (int)CurrentQuestion.Category, 30 * (int)CurrentQuestion.Category);
        var falseP = Random.Range(0, (100 - goodP));
        var falseP2 = Random.Range(0, 100 - (goodP + falseP));
        var falseP3 = Random.Range(0, 100 - (goodP + falseP + falseP2));

        List<float> falsePercentages = new List<float>() {
            falseP, falseP2, falseP3
        };

        var percentage = 0;
        for (int i = 0; i < CurrentQuestion.Answers.Length; i++) {
            var answer = CurrentQuestion.Answers[i];

            if (answer.IsAnswer) percentage = goodP;
            else {
                percentage = (int)Mathf.Round(falsePercentages[Random.Range(0, falsePercentages.Count)]);
                falsePercentages.Remove(percentage);
            }
            Buttons[i].GetComponentInChildren<Text>().text = answer.Text +  "(" + percentage + "%)";
        }
    }

    public void BackButton() {
        AppData.Instance().Game.Unload();
    }
}
