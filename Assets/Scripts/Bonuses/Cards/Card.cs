using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : Bonus
{
    private protected Dictionary<int, float> values;

    private int _level = 1;

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
        {1, 3 }, {2, 5 }, {3, 7 }, {4, 10 }, {5, 12 }
    };



    public Card(int level)
    {
        OnCreate(level);
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
