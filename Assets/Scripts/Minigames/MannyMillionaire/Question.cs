﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Assets.Scripts.Minigames.MannyMillionaire {
    public enum Difficulty {
        Hard = 1,
        Moderate = 2,
        Easy = 3
    }

    public class Question {
        public Question(string text, List<Answer> answers, Difficulty difficulty) {
            Text = HttpUtility.HtmlDecode(Regex.Unescape(text));
            Answers = answers;
            Difficulty = difficulty;
        }

        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}