using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCrushBall : BallScript
{
    private BrickManager brickManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        brickManager = gameManager.GetComponent<BrickManager>();
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(CountDamage(), DamageType.DAMAGE, collision.gameObject);
            brickManager.UnsubscribeBricks(gameObject);
            Destroy(gameObject);
        }
    }

    private protected override void UpdatePrestigeValue(BallType ballType, PrestigeBonusType prestigeBonusType, BonusStats<float> bs)
    {
    }

    private protected override void UpdatePrestigeValueInternal(PrestigeBonusType prestigeBonusType, BonusStats<float> bs)
    {
    }
}
