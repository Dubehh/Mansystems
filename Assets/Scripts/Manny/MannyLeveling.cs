using System;
using UnityEngine;

public class MannyLeveling {
    private Manny _manny;

    public MannyLeveling(Manny manny) {
        _manny = manny;
    }

    private const int _offset = 30;

    /// <summary>
    /// Gets the level based on a given amount of experience through a quadratic formula
    /// </summary>
    /// <param name="xp">The amount of experience</param>
    /// <returns>An int value with the calculated level</returns>
    public int GetLevel(float xp) {
        return (int)Math.Floor(_offset + Math.Sqrt(_offset * _offset - 4 * _offset * (-xp))) / (2 * _offset);
    }

    /// <summary>
    /// Gets the required experience for a certain level through a formula
    /// </summary>
    /// <param name="level">The level to get the required experience for</param>
    /// <returns>A float value with the required experience</returns>
    public float GetRequiredExperience(int level) {
        return _offset * level * level - _offset * level;
    }
}

