using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    //private List<Bonus> bonuses = new List<Bonus>();

    private Dictionary<BonusType, Bonus> bonuses = new Dictionary<BonusType, Bonus>();

    public void AddNewBonus(BonusType type)
    {
        switch (type)
        {
            case BonusType.BALLDAMAGE:
                bonuses.Add(BonusType.BALLDAMAGE, new DamageBonus(1));
                break;
        }
    }

    public void AddCountToBonus(BonusType type, int count)
    {
        bonuses[type].AddCount(count);
    }

    public Bonus GetBonus(BonusType type)
    {
        return bonuses[type];
    }
}

