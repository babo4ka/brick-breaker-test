using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBall : BallScript
{

    void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        
    }


    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(damage, DamageType.DAMAGE, collision.gameObject);
        }
    }
}
