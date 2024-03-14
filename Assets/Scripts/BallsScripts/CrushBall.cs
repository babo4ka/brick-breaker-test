using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushBall : BallScript
{
    [SerializeField]
    private GameObject smallBallPrefab;

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
        }



        if(collision.gameObject.tag == "Wall")
        {
            Vector2 position = transform.position;

            for(int i=0; i<2; i++)
            {
                GameObject smallBall = Instantiate(smallBallPrefab, position, Quaternion.identity);
                BallScript bs = smallBall.GetComponent<BallScript>();
                bs.speed = speed;
                bs.damage = damage * 0.4f;
                bs.SetTrajectory();
                brickManager.SubscribeBricks(smallBall);
            }

        }
    }
}
