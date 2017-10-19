using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.iOS;

public class MannyNotification {

    public delegate bool Condition(IterationStamp span, MannyAttribute attr);
    public struct Notification {
        public string Message { get; set; }
        public Condition Condition { get; set; }
    }

    public enum IterationStamp {
        Minute = 1,
        Half = 30,
        Full = 2 * Half,
        Four = 4 * Full,
        Eight = 2 * Four
    }

    private List<Notification> _notifications;

    private Manny _manny;
    public MannyNotification(Manny manny) {
        _manny = manny;
        _notifications = new List<Notification>();
        Register();
    }

    private void Register() {
        Register("Er is een minuutje voorbij! Tijd voor een biertje!", (stamp, attr) => {
            return stamp == IterationStamp.Minute;
        });
        Register("Hmm. Ik heb eigenlijk wel zin in wat eten!", (stamp, attr) => {
            return attr.GetAttribute(Attribute.Food) < 40;
        });
        Register("Hey! Heb je misschien een drankje voor mij?", (stamp, attr) => {
            return attr.GetAttribute(Attribute.Thirst) < 40;
        });
        Register("Pfff.. ik voel mij nogal zwak.. kan je even komen?", (stamp, attr) => {
            return attr.GetAttribute(Attribute.Thirst) < 35 && attr.GetAttribute(Attribute.Food) < 30 && (int)stamp > (int)IterationStamp.Four;
        });
        Register("Hmm.. ga ik straks Pizza eten of Spaghetti..?", (stamp, attr) => {
            return (int)stamp > (int)IterationStamp.Four;
        });
        Register("Hey kanjer! Heb je zin om een spelletje te spelen?", (stamp, attr) => { return true; });
    }

    private void Register(string msg, Condition cond) {
        _notifications.Add(new Notification() {
            Condition = cond,
            Message = msg
        });
    }

    private void Reset() {
#if UNITY_ANDROID
        NotificationUtil.ClearNotifications();
#endif
#if UNITY_IHPONE
        NotificationServices.CancelAllLocalNotifications();
#endif
    }

    public void Send() {
        Reset();
        foreach (var stamp in Enum.GetValues(typeof(IterationStamp)).Cast<IterationStamp>()) {
            var notification = _notifications.Where(x => x.Condition.Invoke(stamp, _manny.Attribute)).FirstOrDefault();
#if UNITY_ANDROID
            NotificationUtil.Send(TimeSpan.FromMinutes((int)stamp), notification.Message);
#endif
#if UNITY_IPHONE
            var iosNotification = new LocalNotification();
            iosNotification.fireDate = DateTime.Now.AddMinutes((int)stamp);
            iosNotification.alertBody = notification.Message;
            iosNotification.alertAction = "Manny";
            NotificationServices.ScheduleLocalNotification(iosNotification);
#endif
        }
    }
}
