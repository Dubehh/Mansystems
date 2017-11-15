using Assets.Scripts.App.Game;
using System.Collections;
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
        throw new System.NotImplementedException();
    }

    protected override void BeforeLoad() {
        CurrentQuestion = new Question("Hoe oud ben je?",
            new Answer[4] {
                new Answer() {
                    Text = "1",
                },

                new Answer() {
                    Text = "20",
                    IsAnswer = true
                },

                new Answer() {
                    Text = "23",
                },

                new Answer() {
                    Text = "21",
                }
            });
    }

    protected override void OnLoad() {

    }

    protected override void Update() {
        UpdateUI();
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
            Debug.Log("Goed gedaan!");
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
}
