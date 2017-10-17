using System;
using System.Collections.Generic;

public class MannyNotification {
    private Dictionary<int, string> _messages;
    private int[] _hourlyDelays;

    public MannyNotification() {
        _messages = new Dictionary<int, string>{
            { 8, "Ik lust wel een broodje bij het ontbijt" },
            { 21, "Een pizza gaat er nog wel in!" },
            { 18, "Een flink bord spaghetti klinkt als een heerlijk diner!" },
            { 14, "Ik verveel me, zullen we een minigame spelen?" },
            { 23, "Mag ik nog een glaasje water voor het slapengaan?" }
        };

        _hourlyDelays = new int[] {
            1, 2, 4, 8, 16
        };
    }

    public void CreateNotificationQueue() {
        foreach (var message in _messages) {
            var delay = Math.Abs(message.Key - DateTime.Now.Hour);
            if (delay <= 0) delay = 24 - delay;

            NotificationUtil.Send(TimeSpan.FromHours(delay), message.Value);
        }
    }
}
