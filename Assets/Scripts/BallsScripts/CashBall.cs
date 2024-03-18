using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashBall : BallScript
{
    [SerializeField]
    private GameObject gameManager;
    private CashManager cashManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        cashManager = gameManager.GetComponent<CashManager>();
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            cashManager.AddSoftCash(collision.gameObject.GetComponent<BrickScript>().health * CountDamage());
        }
    }
}
