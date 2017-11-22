using System.Collections.Generic;
using UnityEngine.Networking;

public class QuestionController {
    private List<Question> _questions;
    private int _currentQuestionIndex;

    public QuestionController() {
        _questions = new List<Question> {
            new Question("Wat voor gebiedsbenaming verdient Hattem?",
            new List<Answer> {
                new Answer() { Text = "Stad" },
                new Answer() { Text = "Dorp", IsAnswer = true },
                new Answer() { Text = "Gehucht" },
                new Answer() { Text = "Buurtschap" }
            }, Difficulty.Easy),

        new Question("Wie is de beste programmeur van Nederland?",
            new List<Answer> {
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
    public void AddQuestion(UnityWebRequest data, int index) {
        //TODO, USE DATA
        _questions.Add(
            new Question("Question", 
            new List<Answer> {
                new Answer { Text = "Answer 1" },
                new Answer { Text = "Answer 2", IsAnswer = true },
                new Answer { Text = "Answer 3" },
                new Answer { Text = "Answer 4" }
        }, (Difficulty)3 - index
        ));
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
