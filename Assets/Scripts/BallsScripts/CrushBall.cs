using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushBall : BallScript
{
    [SerializeField]
    private GameObject smallBallPrefab;

    private int smallBallsCount = 2;
    private float damagePercentInc = 0;

    private BrickManager brickManager;

    void Start()
    {
        brickManager = gameManager.GetComponent<BrickManager>();
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(CountDamage(), DamageType.DAMAGE, collision.gameObject);
        }



        if(collision.gameObject.tag == "Wall")
        {
            Vector2 position = transform.position;

            for(int i=0; i<smallBallsCount; i++)
            {
                GameObject smallBall = Instantiate(smallBallPrefab, position, Quaternion.identity);
                BallScript bs = smallBall.GetComponent<BallScript>();
                bs.speed = speed;
                bs.damage = damage * (damagePercentInc + 0.4f);
                bs.SetTrajectory();
                brickManager.SubscribeBricks(smallBall);
            }

        }
    }

    private protected override void UpdatePrestigeValueInternal(PrestigeBonusType prestigeBonusType, 
        BonusStats<float> bs)
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

            case PrestigeBonusType.SECDAMAGE:
                if (bs.activate)
                {
                    damagePercentInc += bs.value;
                }
                break;

            case PrestigeBonusType.COUNT:
                if (bs.activate)
                {
                    smallBallsCount += (int)bs.value;
                }
                break;
        }
    }

    private protected override void UpdatePrestigeValue(BallType ballType, 
        PrestigeBonusType prestigeBonusType, 
        BonusStats<float> bs)
    {
        if(ballType == BallType.CRUSH)
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
                        damagePercentInc += bs.value;
                    }
                    else
                    {
                        damagePercentInc -= bs.value;
                    }
                    break;

                case PrestigeBonusType.COUNT:
                    if (bs.activate)
                    {
                        smallBallsCount += (int)bs.value;
                    }
                    else
                    {
                        smallBallsCount -= (int)bs.value;
                    }
                    break;
            }
        }
    }

    private protected override void Awake()
    {
        base.Awake();

        UpdatePrestigeValueInternal(PrestigeBonusType.DAMAGE,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.CRUSH, PrestigeBonusType.DAMAGE));

        UpdatePrestigeValueInternal(PrestigeBonusType.SPEED,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.CRUSH, PrestigeBonusType.SPEED));

        UpdatePrestigeValueInternal(PrestigeBonusType.SECDAMAGE,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.CRUSH, PrestigeBonusType.SECDAMAGE));

        UpdatePrestigeValueInternal(PrestigeBonusType.COUNT,
            gameManager.GetComponent<PrestigeManager>()
            .GetPrestigeBonusValue(BallType.CRUSH, PrestigeBonusType.COUNT));
    }
}
