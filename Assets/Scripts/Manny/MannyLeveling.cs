using System;
using UnityEngine;

class MannyLeveling {
    private Manny _manny;

    public MannyLeveling(Manny manny) {
        _manny = manny;
    }

    private const int _offset = 30;

    public int GetLevel(float xp) {
        return (int)Math.Floor(_offset + Math.Sqrt(_offset * _offset - 4 * _offset * (-xp))) / (2 * _offset);
    }

    public float GetRequiredExperience(float level) {
        return _offset * level * level - _offset * level;
    }
}

