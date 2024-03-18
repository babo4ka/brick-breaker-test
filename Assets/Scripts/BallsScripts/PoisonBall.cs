using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoisonBall : BallScript
{

    [SerializeField]
    private float radius;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        
        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(CountDamage(), DamageType.POISON, collision.gameObject);
        }

        Vector2 circleCenter = transform.position;

        List<Collider2D> around = Physics2D.OverlapCircleAll(circleCenter, radius).ToList();

        around = around.Where(a =>
        a.gameObject.tag == "Brick" && a.gameObject != collision.gameObject).ToList();


        foreach (Collider2D c in around)
        {
            attack?.Invoke(CountDamage(), DamageType.POISON, c.gameObject);
        }
    }

}
