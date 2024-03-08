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

    #region Balls lists
    List<BasicBall> basicBalls = new List<BasicBall>();
    List<PoisonBall> poisonBalls = new List<PoisonBall>();
    #endregion


    #region Base stats
    private Dictionary<int, Dictionary<BallType, float>> powerBaseStats =
        new Dictionary<int, Dictionary<BallType, float>>
        {
            {1 ,  new Dictionary<BallType, float> {
                { BallType.BASIC, 1f} } },
            {2, new Dictionary<BallType, float>{
                { BallType.POISON, 1.5f}
            }}
        };

    private Dictionary<int, Dictionary<BallType, float>> speedBaseStats =
        new Dictionary<int, Dictionary<BallType, float>>
        {
            {1 ,  new Dictionary<BallType, float> {
                { BallType.BASIC, 1f} } },
            {2, new Dictionary<BallType, float>{
                { BallType.POISON, 1.08f}
            }}
        };

    #endregion

    private int currentStage = 1;


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
