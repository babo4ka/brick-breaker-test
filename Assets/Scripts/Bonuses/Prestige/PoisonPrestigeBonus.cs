using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPrestigeBonus : PrestigeBonusBall
{
    public PoisonPrestigeBonus()
    {
        ballType = BallType.POISON;
        mainWrapper = new PrestigeBonusWrapper(PrestigeBonusType.SPEED, new PrestigeBonus(1, 0.1f, 50))
        {
            left = new PrestigeBonusWrapper(PrestigeBonusType.DAMAGE, new PrestigeBonus(1, 0.1f, 50))
            {
                left = new PrestigeBonusWrapper(PrestigeBonusType.EVERYSECONDDAMAGE, new PrestigeBonus(1, 0.1f, 50))
            },

            right = new PrestigeBonusWrapper(PrestigeBonusType.RADIUS, new PrestigeBonus(1, 0.01f, 50))
        };
    }
}
