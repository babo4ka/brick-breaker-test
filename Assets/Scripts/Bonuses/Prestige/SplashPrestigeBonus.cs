using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashPrestigeBonus : MonoBehaviour
{
    private Dictionary<PrestigeBonusType, PrestigeBonus> bonuses = new Dictionary<PrestigeBonusType, PrestigeBonus>
    {
        { PrestigeBonusType.SPEED, new PrestigeBonus(0, 0.2f, 10)},
        { PrestigeBonusType.DAMAGE, new PrestigeBonus(0, 0.1f, 10)},
        { PrestigeBonusType.RADIUS, new PrestigeBonus(0, 0.1f, 10)},
        { PrestigeBonusType.SECDAMAGE, new PrestigeBonus(0, 0.2f, 10)}
    };

    public void UpdateBonusLevelValid(PrestigeBonusType type)
    {
        if(type == PrestigeBonusType.DAMAGE || type == PrestigeBonusType.RADIUS)
        {
            if (bonuses[PrestigeBonusType.SPEED].level >= 3)
            {
                UpdateBonuseLevel(type);
            }
        }else if(type == PrestigeBonusType.SECDAMAGE)
        {
            if (bonuses[PrestigeBonusType.DAMAGE].level >= 3)
            {
                UpdateBonuseLevel(type);
            }
        }
        else
        {
            UpdateBonuseLevel(type);
        }
    }

    private void UpdateBonuseLevel(PrestigeBonusType type)
    {
        bonuses[type].AddLevel();
    }

}
