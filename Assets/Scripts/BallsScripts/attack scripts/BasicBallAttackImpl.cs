using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBallAttackImpl : IAttack
{
    public void Attack(float damage, DamageType damageType, GameObject collision, BallScript.Attack attack)
    {
        if (collision.tag == "Brick")
        {
            attack?.Invoke(damage, DamageType.DAMAGE, collision);
        }
    }
}
