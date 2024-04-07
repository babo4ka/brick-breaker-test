using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeBonusBall
{
    public delegate void UpdatePrestigeBonus(BallType ballType, 
        PrestigeBonusType prestigeBonusType, 
        BonusStats<float> stats);
    public UpdatePrestigeBonus updatePrestigeBonus;

    private protected PrestigeBonusWrapper mainWrapper;
    private protected BallType ballType;


    public void AddLevelToBonus(PrestigeBonusType type)
    {
        float oldValue = mainWrapper.GetPrestigeBonusValue(type);
        bool added = mainWrapper.AddLevelToPrestigeBonus(type);

        if(added)
        {
            updatePrestigeBonus?.Invoke(ballType, type, 
                new BonusStats<float>(false, oldValue));

            updatePrestigeBonus?.Invoke(ballType, type,
                new BonusStats<float>(true, mainWrapper.GetPrestigeBonusValue(type)));
        }
    }

    public float GetNextPrestigeBonusValue(PrestigeBonusType type)
    {
        return mainWrapper.GetNextPrestigeBonusValue(type);
    }

    public BonusStats<float> GetPrestigeBonusStats(PrestigeBonusType type)
    {
        return new BonusStats<float>(true, mainWrapper.GetPrestigeBonusValue(type));
    }
}
