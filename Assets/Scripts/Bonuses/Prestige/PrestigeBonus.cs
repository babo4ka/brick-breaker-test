using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PrestigeBonus : Bonus
{
    private int _level;
    private float _levelStep;
    private int _maxLevel;

    private int _levelPrice;
    public int level
    {
        get { return _level; }
    }

    public int levelPrice
    {
        get { return _levelPrice; }
    }

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
            _levelPrice += (int)(_levelPrice * 1.5);
            return true;
        }

        return false;
    }
}
