using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manny {
    public class MannyCondition {
        private readonly Dictionary<Attribute, MannyConditionStatus> _conditionCheckers;

        private readonly Manny _manny;

        public MannyCondition(Manny manny) {
            _manny = manny;
            _conditionCheckers = new Dictionary<Attribute, MannyConditionStatus>();
        }

        /// <summary>
        ///     Registers an attribute in the condition checker.
        ///     The given attribute will be checked for changes and will represent the condition of Manny.
        /// </summary>
        /// <param name="attribute">The Attribute that will be monitored</param>
        /// <param name="trigger">The value that will trigger the state</param>
        /// <param name="decrease">The 'N' value that will determine the speed of loss for the variable</param>
        /// <param name="msg">The message response that will be sent upon trigger</param>
        public void Register(Attribute attribute, float trigger, float decrease, string msg) {
            _conditionCheckers[attribute] = new MannyConditionStatus {
                Message = msg,
                Minimum = trigger,
                Attribute = attribute,
                Decrease = decrease
            };
        }

        /// <summary>
        ///     Updates the condition of Manny based on the variables.
        ///     This method returns a collection if there are changes to the state of manny's condition
        /// </summary>
        /// <returns>A set containing the different status changes</returns>
        public HashSet<MannyConditionStatus> UpdateCondition() {
            var attribute = _manny.Attribute;
            var rtn = new HashSet<MannyConditionStatus>();
            foreach (var condition in _conditionCheckers) {
                if (condition.Value.Decrease > 0)
                    attribute.IncrementAttribute(condition.Key, -Time.deltaTime * condition.Value.Decrease);
                var actualWeak = attribute.GetAttribute(condition.Key) < condition.Value.Minimum;
                if (!condition.Value.Weak && actualWeak || !actualWeak && condition.Value.Weak) {
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
}