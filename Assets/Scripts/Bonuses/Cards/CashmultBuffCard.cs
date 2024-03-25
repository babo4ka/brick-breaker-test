using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashmultBuffCard : Card
{
    public CashmultBuffCard(int level) : base(level)
    {
    }

    public override void OnCreate(int level)
    {
        values = new Dictionary<int, float>
        {
            { 1, 5f}, { 2, 8f}, { 3, 10f}, {4, 12f}, { 5, 15f}
        };

        value = values[level];
    }
}
