using System.Collections.Generic;

public class Question {
    public string Text { get; set; }
    public Answer[] Answers;

    public Question(string text, Answer[] answers) {
        Text = text;
        Answers = answers;
    }
}
