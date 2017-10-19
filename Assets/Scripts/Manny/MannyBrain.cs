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
        Condition.Register(Attribute.Food, 30, .1f,"Ik heb honger!");
        Condition.Register(Attribute.Coins, 10, 0, "Ik heb geld nodig!");
        Condition.Register(Attribute.Thirst, 35, .2f,"Ik wil poar neem'n!");
    }
    
    /// <summary>
    /// Updates the brain mechanics
    /// </summary>
    public void Update() {
        var status = Condition.UpdateCondition();
        if (status.Count != 0) {
            foreach (var condition in status) {
                _manny.Dashboard.DisplayDialog(condition.Message);
            }
        }
    }

    /// <summary>
    /// Initializes the condition of Manny.
    /// Updates the variables with offline-time
    /// </summary>
    public void Initialize() {
        var current = System.DateTime.Now;
        var last = PlayerPrefs.HasKey(StampKey) ? DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString(StampKey))) : DateTime.Now;
        var difference = (current.Subtract(last).TotalMilliseconds / 1000 * Time.fixedDeltaTime) * .45;
        InitializeAttribute(Attribute.Food, (float) difference);
        InitializeAttribute(Attribute.Thirst, (float) difference);
    }

    /// <summary>
    /// Updates the given attribute with the given difference.
    /// This is used to change the variable values
    /// </summary>
    /// <param name="attribute">The Attribute</param>
    /// <param name="difference">The float difference</param>
    private void InitializeAttribute(Attribute attribute, float difference) {
        _manny.Attribute.IncrementAttribute(attribute, -difference * Condition.GetStatus(attribute).Decrease);
    }
}
