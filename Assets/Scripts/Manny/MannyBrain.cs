using Assets.Scripts.Manny;
using Assets.Scripts.Util;
using System;
using UnityEngine;

public class MannyBrain {

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
        var difference = DateTimeUtil.CurrentTimeMilli() - _manny.Attribute.GetAttribute(Attribute.LastPlayed);
        difference = Math.Abs(difference);
        //InitializeAttribute(Attribute.Food, difference);
        //InitializeAttribute(Attribute.Thirst, difference);
        Debug.Log(difference);
    }

    private void InitializeAttribute(Attribute attribute, float difference) {
        _manny.Attribute.IncrementAttribute(attribute, -difference * Condition.GetStatus(attribute).Decrease);
    }
}
