using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void Attack(float damage, DamageType damageType, GameObject collision, BallScript.Attack attack);
}
