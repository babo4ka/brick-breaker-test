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

    private float levelStep;
    private int maxLevel;

    public PrestigeBonus(int level, float levelStep, int maxLevel)
    {
        _level = level;
        this.levelStep = levelStep;
        this.maxLevel = maxLevel;
        value = 1;
    }

    public bool AddLevel()
    {
        if(_level < maxLevel)
        {
            this._level++;
            value += levelStep;
            return true;
        }

        return false;
    }
}
