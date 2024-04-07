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

    private protected override void UpdatePrestigeValue(BallType ballType, 
        PrestigeBonusType prestigeBonusType, 
        BonusStats<float> bs)
    {
        if(ballType == BallType.DEMO)
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
    }
}
