using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashBrickCard : Card
{
    public CashBrickCard(int level, int count) : base(level, count)
    {
    }

    public override void OnCreate(int level)
    {
        values = new Dictionary<int, float>
        {
            { 1, 2f}, { 2, 2.5f}, { 3, 3f}, {4, 3.5f}, { 5, 4f}
        };

        value = values[level];
    }
}
