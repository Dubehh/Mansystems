using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class QuestionController {
    private List<Question> _questions;
    private int _currentQuestionIndex;

    public QuestionController() {
        _questions = new List<Question>();
    }

    /// <summary>
    /// Returns the current question from the list
    /// </summary>
    public Question GetCurrentQuestion() {
        return _questions[_currentQuestionIndex];
    }

    /// <summary>
    /// Adds a question to the list with data from a webrequest
    /// </summary>
    /// <param name="data">The data from the webrequest</param>
    /// <param name="index">The index used to determine the difficulty</param>
    public void AddQuestion(UnityWebRequest data) {
        var questions = new JSONObject(data.downloadHandler.text);
        var random = new System.Random();

        for (int i = 0; i < questions.keys.Count; i++) {
            var key = questions.keys[i];
            for (int j = 0; j < questions[key].Count; j++) {
                var value = questions[questions.keys[i]][j];

                _questions.Add(new Question(
                    value["question"].str,
                    new List<Answer> {
                        new Answer { Text = value["correct"].str, IsAnswer = true },
                        new Answer { Text = value["wrong1"].str },
                        new Answer { Text = value["wrong2"].str },
                        new Answer { Text = value["wrong3"].str }
                    }.OrderBy(x => random.Next()).ToList(), (Difficulty)value["difficulty"].i
                ));
            }
        }
    }

    /// <summary>
    /// Selects the next question
    /// </summary>
    public void NextQuestion() {
        _currentQuestionIndex += 1;
    }

    /// <summary>
    /// Checks if all the questions have been answered
    /// </summary>
    /// <returns>True = all questions answered</returns>
    public bool AllQuestionsAnswered() {
        return _currentQuestionIndex >= _questions.Count;
    }
}
