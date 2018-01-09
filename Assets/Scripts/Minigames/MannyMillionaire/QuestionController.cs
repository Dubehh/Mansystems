using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;
using Random = System.Random;

public class QuestionController {
    private readonly List<Question> _questions;

    public QuestionController() {
        _questions = new List<Question>();
    }

    public int CurrentQuestionIndex { get; set; }

    /// <summary>
    ///     Returns the current question from the list
    /// </summary>
    public Question GetCurrentQuestion() {
        return _questions[CurrentQuestionIndex];
    }

    /// <summary>
    ///     Adds a question to the list with data from a webrequest
    /// </summary>
    /// <param name="data">The data from the webrequest</param>
    public void LoadQuestions(UnityWebRequest data) {
        var questions = new JSONObject(data.downloadHandler.text);
        var random = new Random();

        foreach (var key in questions.keys)
            for (var j = 0; j < questions[key].Count; j++) {
                var value = questions[key][j];

                _questions.Add(new Question(
                    value["question"].str,
                    new List<Answer> {
                        new Answer {Text = value["correct"].str, IsAnswer = true},
                        new Answer {Text = value["wrong1"].str},
                        new Answer {Text = value["wrong2"].str},
                        new Answer {Text = value["wrong3"].str}
                    }.OrderBy(x => random.Next()).ToList(), (Difficulty) int.Parse(value["difficulty"].str)
                ));
            }
    }

    /// <summary>
    ///     Selects the next question
    /// </summary>
    public void NextQuestion() {
        CurrentQuestionIndex += 1;
    }

    /// <summary>
    ///     Checks if all the questions have been answered
    /// </summary>
    /// <returns>True = all questions answered</returns>
    public bool AllQuestionsAnswered() {
        return CurrentQuestionIndex >= _questions.Count;
    }
}