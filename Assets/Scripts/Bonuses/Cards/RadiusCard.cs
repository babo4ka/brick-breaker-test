using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusCard : Card
{
    public RadiusCard(int level, int count) : base(level, count)
    {
    }

    public override void OnCreate(int level)
    {
        values = new Dictionary<int, float>
        {
            { 1, .2f}, { 2, .3f}, { 3, .5f}, {4, .7f}, { 5, 1f}
        };

        value = values[level];
    }
}
