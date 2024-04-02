using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeBonus : Bonus
{
    private int _level;

    public int level
    {
        get { return _level; }
    }

    private float _levelStep;
    private int _maxLevel;

    public float levelStep
    {
        get { return _levelStep; }
    }

    public int maxLevel
    {
        get { return _maxLevel; }
    }

    public PrestigeBonus(int level, float levelStep, int maxLevel)
    {
        _level = level;
        this._levelStep = levelStep;
        this._maxLevel = maxLevel;
        value = 1;
    }

    public bool AddLevel()
    {
        if(_level < _maxLevel)
        {
            this._level++;
            value += _levelStep;
            return true;
        }

        return false;
    }
}
