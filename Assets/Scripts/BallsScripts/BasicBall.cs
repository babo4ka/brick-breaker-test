using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBall : BallScript
{


    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(CountDamage(), DamageType.DAMAGE, collision.gameObject);
        }
    }
}
