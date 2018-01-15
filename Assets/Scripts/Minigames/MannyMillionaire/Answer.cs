using System.Text.RegularExpressions;

namespace Assets.Scripts.Minigames.MannyMillionaire {
    public struct Answer {

        private string _text;
        public string Text {
            get { return _text; }
            set { _text = HttpUtility.HtmlDecode(Regex.Unescape(value)); }
        }
        public bool IsAnswer { get; set; }
    }
}