using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{

    private CashManager cashManager;

    public delegate void UpdateBonus(BonusType type, BonusStats<float> value);
    public UpdateBonus updateBonus;

   /* public delegate void RemoveBonus(BonusType type, float value);
    public RemoveBonus removeBonus;*/


    private Dictionary<BonusType, Bonus> bonuses = new Dictionary<BonusType, Bonus>();

    private List<BonusType> activeBonuses = new List<BonusType>();
    private int maxBonuses = 1;

    private int maxBonusesExpandPrice = 100;

    private void IncreaseMaxBonusesExpandPrice()
    {
        maxBonusesExpandPrice += 100;
    }

    
    public void ExpandMaxBonuses()
    {
        if (cashManager.SpendHardCash(maxBonusesExpandPrice))
        {
            maxBonuses++;
            IncreaseMaxBonusesExpandPrice();
        }
    }

    private System.Random rand = new System.Random();
    public void OpenNewBonus()
    {
        var allTypes = Enum.GetValues(typeof(BonusType));
        BonusType type;
        do
        {
            type = (BonusType)allTypes.GetValue(rand.Next(allTypes.Length));
        } while (bonuses[type] != null);

        AddNewBonus(type);
    }

    private void AddNewBonus(BonusType type)
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
        if(activeBonuses.Count < maxBonuses)
        {
            if (!activeBonuses.Contains(type))
            {
                activeBonuses.Add(type);
                BonusStats<float> bs = new BonusStats<float>(true, bonuses[type].value);
                updateBonus?.Invoke(type, bs);
            }
        }   
    }

    public void DeactivateBonus(BonusType type)
    {
        activeBonuses.Remove(type);
        BonusStats<float> bs = new BonusStats<float>(false, bonuses[type].value);
        updateBonus?.Invoke(type, bs);
    }


    public void AddCountToBonus(BonusType type, int count)
    {
        float oldValue = bonuses[type].value;

        if (bonuses[type].AddCount(count))
        {
            if (activeBonuses.Contains(type))
            {
                BonusStats<float> bs = new BonusStats<float>(true, bonuses[type].value - oldValue);
                updateBonus?.Invoke(type, bs);
            }
        }
        
        
    }

    public BonusStats<float> GetBonusValue(BonusType type)
    {
        if (activeBonuses.Contains(type))
        {
            return new BonusStats<float>(true, bonuses[type].value);
        }

        return new BonusStats<float>(false, 0f);
    }

    void Start()
    {
        cashManager = GetComponent<CashManager>();
    }
}


