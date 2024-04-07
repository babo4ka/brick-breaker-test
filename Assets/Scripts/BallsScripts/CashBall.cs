using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CashBall : BallScript
{
    [SerializeField]
    private float radius;
    private float radiusMultiplier;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(CountDamage(), DamageType.CASH, collision.gameObject);
        }

        if(radius != 0)
        {
            Vector2 circleCenter = transform.position;

            List<Collider2D> around = Physics2D.OverlapCircleAll(circleCenter, radius * radiusMultiplier).ToList();

            around = around.Where(a =>
            a.gameObject.tag == "Brick" && a.gameObject != collision.gameObject).ToList();


            foreach (Collider2D c in around)
            {
                attack?.Invoke(CountDamage() * 0.4f, DamageType.CASH, c.gameObject);
            }
        }
    }

    private protected override void UpdatePrestigeValue(BallType ballType, 
        PrestigeBonusType prestigeBonusType, 
        BonusStats<float> bs)
    {
        if(ballType == BallType.CASH)
        {
            switch(prestigeBonusType)
            {
                case PrestigeBonusType.SPEED:
                    if (bs.activate)
                    {
                        _speedMultiplier *= bs.value;
                    }
                    else
                    {
                        _speedMultiplier /= bs.value;
                    }
                    UpdateSpeed(this.speed);
                    break;

                case PrestigeBonusType.DAMAGE:
                    if (bs.activate)
                    {
                        _damageMultiplier *= bs.value;
                    }
                    else
                    {
                        _damageMultiplier /= bs.value;
                    }
                    break;


                case PrestigeBonusType.RADIUS:
                    if (bs.activate)
                    {
                        radius = 0.5f;
                        radiusMultiplier *= bs.value;
                    }
                    else
                    {
                        radius = 0;
                        radiusMultiplier /= bs.value;
                    }
                    break;
            }
        }
    }
}
