using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashPrestigeBonus : PrestigeBonusBall
{
    public SplashPrestigeBonus()
    {
        ballType = BallType.SPLASH;
        mainWrapper = new PrestigeBonusWrapper(PrestigeBonusType.SPEED);

        mainWrapper.left = new PrestigeBonusWrapper(PrestigeBonusType.DAMAGE);
        mainWrapper.left.left = new PrestigeBonusWrapper(PrestigeBonusType.SECDAMAGE);

        mainWrapper.right = new PrestigeBonusWrapper(PrestigeBonusType.RADIUS);
    }    
    
}
