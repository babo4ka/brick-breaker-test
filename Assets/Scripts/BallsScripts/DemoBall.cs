using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBall : BallScript
{
    private float killChance = 0f;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            DamageType dt = killChance==0? DamageType.DAMAGE: 
                Random.Range(0, 1) <= killChance ? DamageType.KILL : 
                DamageType.DAMAGE;

            attack?.Invoke(CountDamage(), dt, collision.gameObject);
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

            case PrestigeBonusType.KILLCHANCE:
                if (bs.activate)
                {
                    killChance += bs.value;
                }
                else
                {
                    killChance -= bs.value;
                }
                break;
        }
    }

    private protected override void UpdatePrestigeValue(BallType ballType, 
        PrestigeBonusType prestigeBonusType, 
        BonusStats<float> bs)
    {
        if(ballType == BallType.DEMO)
        {
            UpdatePrestigeValueInternal(prestigeBonusType, bs);
        }
    }


    private protected override void Awake()
    {
        base.Awake();

        UpdatePrestigeValueInternal(PrestigeBonusType.DAMAGE,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.DEMO, PrestigeBonusType.DAMAGE));

        UpdatePrestigeValueInternal(PrestigeBonusType.SPEED,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.DEMO, PrestigeBonusType.SPEED));

        UpdatePrestigeValueInternal(PrestigeBonusType.KILLCHANCE,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.DEMO, PrestigeBonusType.KILLCHANCE));
    }
}
