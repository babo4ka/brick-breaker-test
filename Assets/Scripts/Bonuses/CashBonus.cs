using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashBonus : Card
{
    public CashBonus(int level) : base(level)
    {
    }

    public override void OnCreate(int level)
    {
        values = new Dictionary<int, float>
        {
            { 1, 1.5f}, { 2, 2f}, { 3, 2.2f}, {4, 2.5f}, { 5, 3f}
        };

        value = values[level];
    }
}
