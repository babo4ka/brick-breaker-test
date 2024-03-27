using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeBonusBall : MonoBehaviour
{
    public delegate void UpdatePrestigeBonus(BallType ballType, 
        PrestigeBonusType prestigeBonusType, 
        BonusStats<float> stats);
    public UpdatePrestigeBonus updatePrestigeBonus;

    private protected PrestigeBonusWrapper mainWrapper;
    private protected BallType ballType;


    public void AddLevelToBonus(PrestigeBonusType type)
    {
        bool added = mainWrapper.AddLevelToPrestigeBonus(type);
        float oldValue = mainWrapper.GetPrestigeBonusValue(type);

        if(added)
        {
            updatePrestigeBonus?.Invoke(ballType, type, 
                new BonusStats<float>(false, oldValue));

            updatePrestigeBonus?.Invoke(ballType, type,
                new BonusStats<float>(true, mainWrapper.GetPrestigeBonusValue(type)));
        }
    }

    public BonusStats<float> GetPrestigeBonusStats(PrestigeBonusType type)
    {
        float value = mainWrapper.GetPrestigeBonusValue(type);

        return new BonusStats<float>(true, value);
    }
}
