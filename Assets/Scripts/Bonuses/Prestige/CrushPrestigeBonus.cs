using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushPrestigeBonus : PrestigeBonusBall
{
    public CrushPrestigeBonus()
    {
        ballType = BallType.CRUSH;
        mainWrapper = new PrestigeBonusWrapper(PrestigeBonusType.SPEED, new PrestigeBonus(1, 0.1f, 50))
        {
            left = new PrestigeBonusWrapper(PrestigeBonusType.DAMAGE, new PrestigeBonus(1, 0.1f, 50))
            {
                left = new PrestigeBonusWrapper(PrestigeBonusType.SECDAMAGE, new PrestigeBonus(1, 0.1f, 50))
            },

            right = new PrestigeBonusWrapper(PrestigeBonusType.COUNT, new PrestigeBonus(1, 1f, 5))
        };
    }
}
