using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperPrestigeBonus : PrestigeBonusBall
{
   
    public SniperPrestigeBonus()
    {
        ballType = BallType.SNIPER;
        mainWrapper = new PrestigeBonusWrapper(PrestigeBonusType.SPEED, new PrestigeBonus(1, 0.1f, 50))
        {
            left = new PrestigeBonusWrapper(PrestigeBonusType.DAMAGE, new PrestigeBonus(1, 0.1f, 50))
            {
                left = new PrestigeBonusWrapper(PrestigeBonusType.SECDAMAGE, new PrestigeBonus(1, 0.1f, 50))
            },
        };
    }
}
