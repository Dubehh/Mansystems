using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileDebuggingUtil : MonoBehaviour {

    [SerializeField]
    public Text Text;
    // Use this for initialization

    private void Start() {
        Application.logMessageReceived += Log;
    }

    bool sent = false;

    public void Log(string msg, string stack, LogType type) {
        if (sent || Text == null || type == LogType.Warning || type == LogType.Log) return;
        sent = true;
        foreach(var a in SplitByLength(msg, 60)) {
            Text.text += a + "\n";
        }
        Text.text += "\n";
        foreach(var a in SplitByLength(stack, 60)) {
            Text.text += a + "\n";
        }
    }

    public IEnumerable<string> SplitByLength(string str, int maxLength) {
        for (int index = 0; index < str.Length; index += maxLength) {
            yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
        }
    }
}
