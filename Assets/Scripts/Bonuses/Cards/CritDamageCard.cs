using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritDamageCard : Card
{
    public CritDamageCard(int level, int count) : base(level, count)
    {
    }

    public override void OnCreate(int level)
    {
        values = new Dictionary<int, float>
        {
            { 1, 5f}, { 2, 8f}, { 3, 10f}, {4, 15f}, { 5, 20f}
        };

        value = values[level];
    }
}
