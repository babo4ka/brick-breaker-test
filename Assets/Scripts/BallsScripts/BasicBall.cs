using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBall : BallScript
{
    private IAttack attackImpl = new BasicBallAttackImpl();

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            attackImpl.Attack(CountDamage(), DamageType.DAMAGE, collision.gameObject, attack);
            //attack?.Invoke(CountDamage(), DamageType.DAMAGE, collision.gameObject);
        }
    }

    private protected override void UpdatePrestigeValue(BallType ballType, PrestigeBonusType prestigeBonusType, BonusStats<float> bs)
    {
        throw new System.NotImplementedException();
    }
}
