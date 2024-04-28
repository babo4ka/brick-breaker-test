using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCashCard : Card
{
    public StageCashCard(int level, int count) : base(level, count)
    {
    }

    public override void OnCreate(int level)
    {
        values = new Dictionary<int, float>
        {
            { 1, 1.8f}, { 2, 2.2f}, { 3, 2.6f}, {4, 3.2f}, { 5, 3.5f}
        };

        value = values[level];
    }
}
