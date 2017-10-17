using System;
using UnityEngine;

public class NotificationParams {
    /// <summary>
    /// Use random id for each new notification.
    /// </summary>
    public int Id { get; set; }
    public TimeSpan Delay { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string Ticker { get; set; }
    public bool Sound = true;
    public bool Vibrate = true;
    public bool Light = true;
    public NotificationIcon SmallIcon { get; set; }
    public Color SmallIconColor { get; set; }
    /// <summary>
    /// Use "" for simple notification. Use "app_icon" to use the app icon. Use custom value but first place image to "simple-android-notifications.aar/res/". To modify "aar" file just rename it to "zip" and back.
    /// </summary>
    public string LargeIcon { get; set; }
}
