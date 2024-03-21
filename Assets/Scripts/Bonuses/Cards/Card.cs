using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : Bonus
{
    private protected Dictionary<int, float> values;

    private int _level;

    public int level
    {
        get { return _level; }
        set { _level = value; }
    }

    private int _count;

    public int count
    {
        get { return _count; }
        set { _count = value; }
    }

    private Dictionary<int, int> stageCounts = new Dictionary<int, int>
    {
        {1, 3 }, {2, 5 }, {3, 7 }, {4, 10 }
    };


    public int NextLevelPrice()
    {
        if(_level + 1 < 5) {
            return stageCounts[_level];
        }
        return 0;
    }


    public Card(int level)
    {
        OnCreate(level);
        _level = level;
        _count = 0;
    }

    public abstract void OnCreate(int level);

    private void UpdateLevel(int newLevel)
    {
        value = values[newLevel];
    }

    public bool AddCount(int count)
    {
        if (this.count + count >= stageCounts[level])
        {
            _count = count - (stageCounts[level] - _count);

            level++;
            UpdateLevel(level);
            return true;
        }
        else
        {
            _count += count;
            return false;
        }
    }
}
