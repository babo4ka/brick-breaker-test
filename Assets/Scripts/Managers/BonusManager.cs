using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public delegate void UpdateBonus(BonusType type, float value);
    public UpdateBonus updateBonus;


    private Dictionary<BonusType, Bonus> bonuses = new Dictionary<BonusType, Bonus>();

    private List<BonusType> activeBonuses = new List<BonusType>();

    public void AddNewBonus(BonusType type)
    {
        switch (type)
        {
            case BonusType.BALLDAMAGE:
                bonuses.Add(BonusType.BALLDAMAGE, new DamageBonus(1));
                break;

            case BonusType.BALLSPEED:
                bonuses.Add(BonusType.BALLSPEED, new SpeedBonus(1));
                break;

            case BonusType.CRITDAMAGE:
                bonuses.Add(BonusType.CRITDAMAGE, new CritDamageBonus(1));
                break;
        }
    }

    public void ActivateBonus(BonusType type)
    {
        if(!activeBonuses.Contains(type)){
            activeBonuses.Add(type);
            updateBonus?.Invoke(type, bonuses[type].value);
        }
    }

    public void DeactivateBonus(BonusType type)
    {
        activeBonuses.Remove(type);
        updateBonus?.Invoke(type, -1f);
    }


    public void AddCountToBonus(BonusType type, int count)
    {
        bonuses[type].AddCount(count);
        if (activeBonuses.Contains(type))
        {
            updateBonus?.Invoke(type, bonuses[type].value);
        }
        
    }

    public float GetBonusValue(BonusType type)
    {
        if (activeBonuses.Contains(type))
        {
            return bonuses[type].value;
        }
        return -1f;
    }
}

