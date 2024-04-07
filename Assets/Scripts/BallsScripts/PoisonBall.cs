using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoisonBall : BallScript
{

    [SerializeField]
    private float radius = 0.5f;
    private float radiusMultiplier;

    private float longDamageMul = 0;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        
        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(CountDamage(), DamageType.POISON, collision.gameObject);
            if(longDamageMul > 0)
            {
                attack?.Invoke(CountDamage() * longDamageMul, DamageType.LONGPOISON, collision.gameObject);
            }
        }

        Vector2 circleCenter = transform.position;

        List<Collider2D> around = Physics2D.OverlapCircleAll(circleCenter, radius * radiusMultiplier).ToList();

        around = around.Where(a =>
        a.gameObject.tag == "Brick" && a.gameObject != collision.gameObject).ToList();


        foreach (Collider2D c in around)
        {
            attack?.Invoke(CountDamage(), DamageType.POISON, c.gameObject);
            if (longDamageMul > 0)
            {
                attack?.Invoke(CountDamage() * longDamageMul, DamageType.LONGPOISON, c.gameObject);
            }
        }
    }

    private void UpdateCard(CardType type, BonusStats<float> bs) {

        if(type == CardType.RADIUS)
        {
            if(bs.activate)
            {
                radiusMultiplier *= bs.value;
            }
            else
            {
                radiusMultiplier /= bs.value;
            }
        }
    }

    

    private protected override void Awake()
    {
        base.Awake();
        gameManager.GetComponent<BonusManager>().updateCard += UpdateCard;

        BonusStats<float> bs = gameManager.GetComponent<BonusManager>().GetCardValue(CardType.RADIUS);
        if (bs.activate)
        {
            radiusMultiplier *= bs.value;
        }
    }

    private protected override void UpdatePrestigeValue(BallType ballType, 
        PrestigeBonusType prestigeBonusType, 
        BonusStats<float> bs)
    {
        if(ballType == BallType.POISON)
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
                        radiusMultiplier *= bs.value;
                    }
                    else
                    {
                        radiusMultiplier /= bs.value;
                    }
                    break;

                case PrestigeBonusType.EVERYSECONDDAMAGE:
                    if (bs.activate)
                    {
                        longDamageMul += bs.value;
                    }
                    else
                    {
                        longDamageMul -= bs.value;
                    }
                    break;
            }
        }
    }
}
