using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCrushBall : BallScript
{
    [SerializeField]
    private GameObject gameManager;
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
            attack?.Invoke(damage, DamageType.DAMAGE, collision.gameObject);
            brickManager.UnsubscribeBricks(gameObject);
            Destroy(gameObject);
        }
    }
}
