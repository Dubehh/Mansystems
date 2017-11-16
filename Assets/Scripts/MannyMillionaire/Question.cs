using System.Collections.Generic;
public enum QuestionCategory {
    Hard = 1,
    Moderate = 2,
    Easy = 3
}

public class Question {
    public string Text { get; set; }
    public Answer[] Answers;
    public QuestionCategory Category;

    public Question(string text, Answer[] answers, QuestionCategory category) {
        Text = text;
        Answers = answers;
        Category = category;
    }
}
