using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCard : Card
{
    public SpeedCard(int level, int count) : base(level, count)
    {
    }

    public override void OnCreate(int level)
    {
        values = new Dictionary<int, float>
        {
            { 1, 1.5f}, { 2, 2f}, { 3, 2.2f}, {4, 2.5f}, { 5, 2.5f}
        };

        value = values[level];
    }
}
