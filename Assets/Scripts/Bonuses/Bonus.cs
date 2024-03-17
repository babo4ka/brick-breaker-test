using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    private protected Dictionary<int, float> values;

    private float _value;

    public float value
    {
        get { return _value; }
        set { _value = value; }
    }


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



    public Bonus(int level)
    {
        OnCreate(level);
    }

    public abstract void OnCreate(int level);

    public void AddCount(int count)
    {
        if(this.count + count >= stageCounts[level])
        {
            _count = count - (stageCounts[level] - _count);

            level++;
        }
        else
        {
            _count += count;
        }
    }
}
