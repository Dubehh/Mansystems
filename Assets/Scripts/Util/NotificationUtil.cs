using System;
using Assets.Scripts.App.Notifications;
using UnityEngine;

namespace Assets.Scripts.Util {
    public static class NotificationUtil {
        public static void Send(TimeSpan delay, string message) {
            NotificationManager.SendWithAppIcon(delay, "Manny", message, new Color(1, 0.3f, 0.15f));
        }

        public static void ClearNotifications() {
            NotificationManager.CancelAll();
        }
    }
}