using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager
{
    private float _amount;

    public float amount
    {
        get { return _amount; }
    }


    public void AddCash(float amount)
    {
        this._amount += amount;
    }


    public bool SpendCash(float amount)
    {
        if(this._amount < amount)
        {
            return false;
        }

        this._amount -= amount;
        return true;
    }
}
