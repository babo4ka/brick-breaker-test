using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallManager : MonoBehaviour {

    #region Balls prefabs
    [SerializeField]
    private GameObject basicBallPrefab;
    [SerializeField]
    private GameObject poisonBallPrefab;
    #endregion

    List<BasicBall> basicBalls = new List<BasicBall>();
    List<PoisonBall> poisonBalls = new List<PoisonBall>();



    public void BuyNewBall(BallType type)
    {

    }



    private void Start()
    {
        List<GameObject> balls = GameObject.FindGameObjectsWithTag("Ball").ToList();

        foreach (GameObject ball in balls)
        {
            if (ball.GetComponent<BallScript>().GetType() == typeof(PoisonBall))
            {
                poisonBalls.Add(ball.GetComponent<PoisonBall>());
            }

            if(ball.GetComponent<BallScript>().GetType() == typeof(BasicBall))
            {
                basicBalls.Add(ball.GetComponent<BasicBall>());
            }
        }
    }
}
