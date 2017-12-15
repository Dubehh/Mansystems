using System;
using UnityEngine;

public static class NotificationUtil {
    public static void Send(TimeSpan delay, string message) {
        NotificationManager.SendWithAppIcon(delay, "Manny", message, new Color(1, 0.3f, 0.15f));
    }

    public static void ClearNotifications() {
        NotificationManager.CancelAll();
    }
}