using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBall : BallScript
{
    private float sniperDamageMul = 1f;

    private GameObject closestBrick = null;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(CountDamage() * (collision.gameObject == closestBrick? sniperDamageMul:1), 
                DamageType.DAMAGE, collision.gameObject);
        }

        if(collision.gameObject.tag == "Wall")
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach(GameObject brick in bricks)
            {
                Vector3 diff = brick.transform.position - position;
                float currDistance = diff.sqrMagnitude;

                if(currDistance < distance)
                {
                    closest = brick;
                    distance = currDistance;
                }

            }
            closestBrick = closest;
            Vector2 goal = closest.transform.position - position;
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(goal.normalized * speed);
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

            case PrestigeBonusType.SECDAMAGE:
                if (bs.activate)
                {
                    sniperDamageMul += bs.value;
                }
                else
                {
                    sniperDamageMul -= bs.value;
                }
                break;
        }
    }

    private protected override void UpdatePrestigeValue(BallType ballType, 
        PrestigeBonusType prestigeBonusType, 
        BonusStats<float> bs)
    {
        if(ballType == BallType.SNIPER)
        {
            UpdatePrestigeValueInternal(prestigeBonusType, bs);
        }
    }

    private protected override void Awake()
    {
        base.Awake();

        UpdatePrestigeValueInternal(PrestigeBonusType.DAMAGE,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.SNIPER, PrestigeBonusType.DAMAGE));

        UpdatePrestigeValueInternal(PrestigeBonusType.SPEED,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.SNIPER, PrestigeBonusType.SPEED));

        UpdatePrestigeValueInternal(PrestigeBonusType.SECDAMAGE,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.SNIPER, PrestigeBonusType.SECDAMAGE));
    }
}
