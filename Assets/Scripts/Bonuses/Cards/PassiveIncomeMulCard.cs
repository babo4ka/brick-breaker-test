using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveIncomeMulCard : Card
{
    public PassiveIncomeMulCard(int level, int count) : base(level, count)
    {
    }

    public override void OnCreate(int level)
    {
        values = new Dictionary<int, float>
        {
            { 1, 2f}, { 2, 2.8f}, { 3, 3.6f}, {4, 4.2f}, { 5, 5f}
        };

        value = values[level];
    }
}
