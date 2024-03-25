using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoisonBall : BallScript
{

    [SerializeField]
    private float radius = 0.5f;
    private float radiusMultiplier;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        
        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(CountDamage(), DamageType.POISON, collision.gameObject);
        }

        Vector2 circleCenter = transform.position;

        List<Collider2D> around = Physics2D.OverlapCircleAll(circleCenter, radius * radiusMultiplier).ToList();

        around = around.Where(a =>
        a.gameObject.tag == "Brick" && a.gameObject != collision.gameObject).ToList();


        foreach (Collider2D c in around)
        {
            attack?.Invoke(CountDamage(), DamageType.POISON, c.gameObject);
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

}
