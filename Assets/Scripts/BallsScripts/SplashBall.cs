using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplashBall : BallScript
{

    [SerializeField]
    private float radius;
    private float radiusMultiplier;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(CountDamage(), DamageType.DAMAGE, collision.gameObject);
        }

        Vector2 circleCenter = transform.position;

        List<Collider2D> around = Physics2D.OverlapCircleAll(circleCenter, radius * radiusMultiplier).ToList();

        around = around.Where(a => 
        a.gameObject.tag == "Brick" && a.gameObject!=collision.gameObject).ToList();


        foreach(Collider2D c in around)
        {
            attack?.Invoke(CountDamage() * 0.4f, DamageType.DAMAGE, c.gameObject);
        }
    }

    private void UpdateCardSplash(CardType type, BonusStats<float> bs)
    {

        if (type == CardType.RADIUS)
        {
            if (bs.activate)
            {
                radiusMultiplier *= bs.value;
            }
            else
            {
                radiusMultiplier /= bs.value;
            }
        }
    }

    private protected override void UpdatePrestigeValueInternal(PrestigeBonusType prestigeBonusType, BonusStats<float> bs)
    {
        switch (prestigeBonusType)
        {
            case PrestigeBonusType.SPEED:
                if (bs.activate)
                {
                    _speedMultiplier *= bs.value;
                    UpdateSpeed(this.speed);
                }
                break;

            case PrestigeBonusType.DAMAGE:
                if (bs.activate)
                {
                    _damageMultiplier *= bs.value;
                }
                break;

            case PrestigeBonusType.RADIUS:
                if (bs.activate)
                {
                    radiusMultiplier *= bs.value;
                }
                break;
        }
    }

    private protected override void UpdatePrestigeValue(BallType ballType,
        PrestigeBonusType prestigeBonusType,
        BonusStats<float> bs)
    {
        if (ballType == BallType.SPLASH)
        {
            switch (prestigeBonusType)
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
                    Debug.Log(bs.activate);
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
            }
        }
        
    }



    private protected override void Awake()
    {
        base.Awake();
        gameManager.GetComponent<BonusManager>().updateCard += UpdateCardSplash;


        BonusStats<float> bs = gameManager.GetComponent<BonusManager>().GetCardValue(CardType.RADIUS);
        if (bs.activate)
        {
            radiusMultiplier *= bs.value;
        }

        UpdatePrestigeValueInternal(PrestigeBonusType.DAMAGE,
           gameManager.GetComponent<PrestigeManager>()
           .GetPrestigeBonusValue(BallType.SPLASH, PrestigeBonusType.DAMAGE));

        UpdatePrestigeValueInternal(PrestigeBonusType.SPEED,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.SPLASH, PrestigeBonusType.SPEED));

        UpdatePrestigeValueInternal(PrestigeBonusType.RADIUS,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.SPLASH, PrestigeBonusType.RADIUS));
    }


}
