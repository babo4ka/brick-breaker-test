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
    private List<BasicBall> basicBalls = new List<BasicBall>();
    private List<PoisonBall> poisonBalls = new List<PoisonBall>();


    private int BallsCount(BallType ballType)
    {
        switch (ballType)
        {
            case BallType.BASIC:
                return basicBalls.Count;

            case BallType.POISON:
                return poisonBalls.Count;
        }

        return -1;
    }
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

    #region Stats dynamics
    private Dictionary<BallType, float> currentDamage = new Dictionary<BallType, float>();
    private Dictionary<BallType, float> damageIncrement = new Dictionary<BallType, float>();
    private float allBallsDamageIncrementMultiplier = 1.00969f * 1.00997f;
    private float poisonBallDamageIncrementMultiplier = 0.434783f * 0.9925f;
    private float cashBallDamageIncrementMultiplier = 0.434783f * 0.9967f;

    private Dictionary<BallType, float> currentSpeed = new Dictionary<BallType, float>();
    private Dictionary<BallType, float> speedIncrement = new Dictionary<BallType, float>();
    private float allBallsSpeedIncrementMultiplier = 0.62787f * 0.9944711f;
    private float demoBallSpeedIncrementMultiplier = 0.33039f * 0.99446999f;
    
    private Dictionary<BallType, float> damageMultipliers = new Dictionary<BallType, float>();
    private Dictionary<BallType, float> speedMultipliers = new Dictionary<BallType, float>();
    #endregion


    private int currentStage = 0;
    private Dictionary<int, float> stagePrice = new Dictionary<int, float> {
        {1, 0f }, {2, 175f}, {3, 7500f}, {4, 175000f}, {5, 15f * (float)Math.Pow(10, 6)}
    };

    private Dictionary<BallType, float> newBallPrice = new Dictionary<BallType, float>();
    private Dictionary<BallType, float> damageUpgradePrice = new Dictionary<BallType, float>();
    private Dictionary<BallType, float> speedUpgradePrice = new Dictionary<BallType, float>();

    private const float priceMultiplier = 0.95f;

    public int BuyNewBall(BallType type)
    {
        CashManager cm = GetComponent<CashManager>();
        float price = newBallPrice[type];
        
        if (cm.SpendCash(price))
        {
            newBallPrice[type] += price * priceMultiplier;
            InstantiateBall(type);
        }

        return BallsCount(type);
    }

    public float UpgradeSpeed(BallType type)
    {
        CashManager cm = GetComponent<CashManager>();
        float price = speedUpgradePrice[type];
        
        if (cm.SpendCash(price))
        {
            speedUpgradePrice[type] += price * priceMultiplier;

           

            switch (type)
            {
                case BallType.BASIC:
                    speedIncrement[type] += speedIncrement[type] * allBallsSpeedIncrementMultiplier;

                    currentSpeed[type] += speedIncrement[type];
                    foreach (BasicBall bb in basicBalls)
                    {
                        bb.speed = currentSpeed[type];
                    }
                    return currentSpeed[type];
            }
        }

        return -1f;
    }

    public float UpgraadeDamage(BallType type)
    {
        CashManager cm = GetComponent<CashManager>();
        float price = damageUpgradePrice[type];

        if (cm.SpendCash(price))
        {
            damageUpgradePrice[type] += price * priceMultiplier;
            switch (type)
            {
                case BallType.BASIC :
                    damageIncrement[type] += damageIncrement[type] * allBallsDamageIncrementMultiplier;

                    currentDamage[type] += damageIncrement[type];

                    foreach(BasicBall bb  in basicBalls)
                    {
                        bb.damage = currentDamage[type];
                    }
                    return currentDamage[type];
            }
        }

        return -1f;
    }

    public void OpenNewBall(BallType ballType)
    {
       
        CashManager cm = GetComponent<CashManager>();
        if (cm.SpendCash(stagePrice[currentStage + 1]))
        {
            currentStage++;

            currentDamage.Add(ballType, powerBaseStats[currentStage][ballType]);
            currentSpeed.Add(ballType, speedBaseStats[currentStage][ballType]);


            damageIncrement.Add(ballType, powerBaseStats[currentStage][ballType]);
            speedIncrement.Add(ballType, speedBaseStats[currentStage][ballType]);

            damageUpgradePrice.Add(ballType, stagePrice[currentStage] == 0f ? 6f : stagePrice[currentStage]);
            speedUpgradePrice.Add(ballType, stagePrice[currentStage] == 0f ? 6f : stagePrice[currentStage]);
            newBallPrice.Add(ballType, stagePrice[currentStage] == 0f? 6f:
                stagePrice[currentStage] + (stagePrice[currentStage] * priceMultiplier));


            InstantiateBall(ballType);
        }
    }
    

    #region Update stats
    public void UpdateDamage(BallType ballType)
    {
        if(ballType == BallType.POISON)
        {
            damageMultipliers[ballType] *= poisonBallDamageIncrementMultiplier;
        }else if(ballType == BallType.CASH)
        {
            damageMultipliers[ballType] *= cashBallDamageIncrementMultiplier;
        }
        else
        {
            damageMultipliers[ballType] *= allBallsDamageIncrementMultiplier;
        }

        float inc = damageMultipliers[ballType];
        currentDamage[ballType] += inc;

        switch(ballType)
        {
            case BallType.BASIC:
                foreach(BasicBall bb in basicBalls)
                {
                    bb.damage = currentDamage[ballType];
                }
                break;

            case BallType.POISON:
                foreach(PoisonBall pb in poisonBalls)
                {
                    pb.damage = currentDamage[ballType];
                }
                break;
        }
    }

    public void UpdateSpeed(BallType ballType)
    {
        if(ballType == BallType.DEMO)
        {
            speedMultipliers[ballType] *= demoBallSpeedIncrementMultiplier;
        }
        else
        {
            speedMultipliers[ballType] *= allBallsSpeedIncrementMultiplier;
        }

        float inc = speedMultipliers[ballType];

        currentSpeed[ballType] += inc;

        switch (ballType)
        {
            case BallType.BASIC:
                foreach(BasicBall bb in basicBalls)
                {
                    bb.speed = currentSpeed[ballType];
                }
                break;

            case BallType.POISON:
                foreach(PoisonBall pb  in poisonBalls)
                {
                    pb.speed = currentSpeed[ballType];
                }
                break;
        }
    }
    #endregion

    private void InstantiateBall(BallType ballType)
    {
        GameObject ballPrefab = null;
        switch (ballType)
        {
            case BallType.BASIC:
                ballPrefab = basicBallPrefab;
                break;

            case BallType.POISON:
                ballPrefab = poisonBallPrefab;
                break;
        }
        

        GameObject ball = Instantiate(ballPrefab, new Vector2(0, -4), Quaternion.identity);
        BallScript bs = ball.GetComponent<BallScript>();
        bs.damage = currentDamage[ballType];
        bs.speed = currentSpeed[ballType];
        bs.SetTrajectory();


        switch (ballType)
        {
            case BallType.BASIC:
                basicBalls.Add((BasicBall)bs);
                break;

            case BallType.POISON:
                poisonBalls.Add((PoisonBall)bs);
                break;
        }

        GetComponent<BrickManager>().SubscribeBricks(ball);
    }



    private void Start()
    {
        OpenNewBall(BallType.BASIC);

       /* List<GameObject> balls = GameObject.FindGameObjectsWithTag("Ball").ToList();

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
        }*/
    }
}
