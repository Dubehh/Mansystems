using Assets.Scripts.Manny;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MannyCondition {

    private Manny _manny;
    private Dictionary<Attribute, MannyConditionStatus> _conditionCheckers;

    public MannyCondition(Manny manny) {
        _manny = manny;
        _conditionCheckers = new Dictionary<Attribute, MannyConditionStatus>();
    }

    public void Register(Attribute attribute, float trigger, float decrease, string msg) {
        _conditionCheckers[attribute] = new MannyConditionStatus() {
            Message = msg,
            Minimum = trigger,
            Attribute = attribute,
            Decrease = decrease
        };
    }
    
    public HashSet<MannyConditionStatus> UpdateCondition() {
        var attribute = _manny.Attribute;
        var rtn = new HashSet<MannyConditionStatus>();
        foreach(var condition in _conditionCheckers) {
            if(condition.Value.Decrease>0)
                attribute.IncrementAttribute(condition.Key, -Time.deltaTime*condition.Value.Decrease);
            var actualWeak = attribute.GetAttribute(condition.Key) < condition.Value.Minimum;
            if ((!condition.Value.Weak && actualWeak) || (!actualWeak && condition.Value.Weak)) {
                condition.Value.SetWeak(!condition.Value.Weak);
                rtn.Add(condition.Value);
            }
        }
        return rtn;
    }

    public MannyConditionStatus GetStatus(Attribute attribute) {
        return _conditionCheckers[attribute];
    }
}
