using Assets.Scripts.Manny;
using System;
using UnityEngine;

public class MannyBrain {

    public const string StampKey = "systemTime";
    public MannyCondition Condition { get; private set; }
    private Manny _manny;

    public MannyBrain(Manny manny) {
        _manny = manny;
        Condition = new MannyCondition(manny);
        Condition.Register(Attribute.Food, 30, .1f,"Manny heeft honger!");
        Condition.Register(Attribute.Coins, 10, 0, "Manny heeft geld nodig!");
        Condition.Register(Attribute.Thirst, 35, .2f,"Manny wil poar neem'n!");
    }
    
    public void Update() {
        var status = Condition.UpdateCondition();
        if (status.Count != 0) {
            foreach (var condition in status) {
               // do something with each condition
            }
        }
    }

    public void Initialize() {
        var current = System.DateTime.Now;
        var last = PlayerPrefs.HasKey(StampKey) ? DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString(StampKey))) : DateTime.Now;
        var difference = (current.Subtract(last).TotalMilliseconds / 1000 * Time.fixedDeltaTime) * .45;
        InitializeAttribute(Attribute.Food, (float) difference);
        InitializeAttribute(Attribute.Thirst, (float) difference);
    }

    private void InitializeAttribute(Attribute attribute, float difference) {
        _manny.Attribute.IncrementAttribute(attribute, -difference * Condition.GetStatus(attribute).Decrease);
    }
}
