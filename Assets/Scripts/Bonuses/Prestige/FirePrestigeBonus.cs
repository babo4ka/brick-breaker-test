using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePrestigeBonus : PrestigeBonusBall
{
    public FirePrestigeBonus()
    {
        ballType = BallType.FIRE;
        mainWrapper = new PrestigeBonusWrapper(PrestigeBonusType.SPEED, new PrestigeBonus(1, 0.1f, 50))
        {
            left = new PrestigeBonusWrapper(PrestigeBonusType.DAMAGE, new PrestigeBonus(1, 0.1f, 50)),
            right = new PrestigeBonusWrapper(PrestigeBonusType.RADIUS, new PrestigeBonus(1, 0.01f, 50))
        };
    }
}
