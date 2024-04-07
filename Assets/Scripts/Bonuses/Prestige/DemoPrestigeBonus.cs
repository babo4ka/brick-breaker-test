using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPrestigeBonus : PrestigeBonusBall
{
    public DemoPrestigeBonus()
    {
        ballType = BallType.DEMO;
        mainWrapper = new PrestigeBonusWrapper(PrestigeBonusType.SPEED, new PrestigeBonus(1, 0.1f, 50))
        {
            left = new PrestigeBonusWrapper(PrestigeBonusType.DAMAGE, new PrestigeBonus(1, 0.1f, 50))
            {
                left = new PrestigeBonusWrapper(PrestigeBonusType.KILLCHANCE, new PrestigeBonus(1, 0.01f, 50))
            }
        };
    }
}
