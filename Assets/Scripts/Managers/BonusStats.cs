using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusStats<T>
{
    private bool _activate;
    private T _value;

    public bool activate
    {
        get { return _activate; }
        set { _activate = value; }
    }

    public T value
    {
        get { return _value; }
        set { _value = value; }
    }

    public BonusStats(bool activate, T value) {
        this.activate = activate;
        this.value = value;
    }
}
