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
    [SerializeField]
    private GameObject crushBallPrefab;
    [SerializeField]
    private GameObject demoBallPrefab;
    [SerializeField]
    private GameObject sniperBallPrefab;
    [SerializeField]
    private GameObject splashBallPrefab;
    [SerializeField]
    private GameObject cashBallPrefab;
    [SerializeField]
    private GameObject fireBallPrefab;
    #endregion

    #region Balls lists
    private List<BasicBall> basicBalls = new List<BasicBall>();
    private List<PoisonBall> poisonBalls = new List<PoisonBall>();
    private List<CrushBall> crushBalls = new List<CrushBall>();
    private List<DemoBall> demoBalls = new List<DemoBall>();
    private List<SniperBall> sniperBalls = new List<SniperBall>();
    private List<SplashBall> splashBalls = new List<SplashBall>();
    private List<CashBall> cashBalls = new List<CashBall>();
    private List<FireBall> fireBalls = new List<FireBall>();


    private int BallsCount(BallType ballType)
    {
        switch (ballType)
        {
            case BallType.BASIC:
                return basicBalls.Count;

            case BallType.SNIPER:
                return sniperBalls.Count;

            case BallType.SPLASH:
                return splashBalls.Count;

            case BallType.POISON:
                return poisonBalls.Count;

            case BallType.DEMO:
                return demoBalls.Count;

            case BallType.CRUSH:
                return crushBalls.Count;

            case BallType.CASH:
                return cashBalls.Count;

            case BallType.FIRE:
                return fireBalls.Count;
        }

        return -1;
    }

    public int AllBallsCount()
    {
        return BallsCount(BallType.BASIC) + BallsCount(BallType.SNIPER) + 
            BallsCount(BallType.SPLASH) + BallsCount(BallType.POISON) +
            BallsCount(BallType.DEMO) + BallsCount(BallType.CRUSH) + 
            BallsCount(BallType.CASH) + BallsCount(BallType.FIRE);
    }
    #endregion


    #region Base stats
    private Dictionary<int, Dictionary<BallType, float>> powerBaseStats =
        new Dictionary<int, Dictionary<BallType, float>>
        {
            {1 ,  new Dictionary<BallType, float> {
                {BallType.BASIC, 1f}}},

            {2, new Dictionary<BallType, float>{
                {BallType.SNIPER, 4.2f},
                {BallType.SPLASH, 4f}
            }},
            {3, new Dictionary<BallType, float>{
                {BallType.SNIPER, 15.75f},
                {BallType.SPLASH, 15f},
                {BallType.POISON, 1.5f},
                {BallType.DEMO, 300f}
            }},
            {4, new Dictionary<BallType, float>{
                {BallType.SNIPER, 63f},
                {BallType.SPLASH, 60f},
                {BallType.POISON, 1.65f},
                {BallType.DEMO, 1200f},
                {BallType.CRUSH, 60f}
            }},
            {5, new Dictionary<BallType, float>{
                {BallType.SNIPER, 210f},
                {BallType.SPLASH, 200f},
                {BallType.POISON, 1.85f},
                {BallType.DEMO, 4000f},
                {BallType.CRUSH, 200f},
                {BallType.CASH, 0.35f}
            }},
            {6, new Dictionary<BallType, float>{
                {BallType.SNIPER, 945f},
                {BallType.SPLASH, 900f},
                {BallType.POISON, 2.1f},
                {BallType.DEMO, 18000f},
                {BallType.CRUSH, 900f},
                {BallType.CASH, 0.3f},
                {BallType.FIRE, 900f}
            }},
            {7, new Dictionary<BallType, float>{
                {BallType.SNIPER, 2730f},
                {BallType.SPLASH, 2600f},
                {BallType.POISON, 2.35f},
                {BallType.DEMO, 52000f},
                {BallType.CRUSH, 2600f},
                {BallType.CASH, 0.43f},
                {BallType.FIRE, 2600f}
            }},
            {8, new Dictionary<BallType, float>{
                {BallType.SNIPER, 12600f},
                {BallType.SPLASH, 12000f},
                {BallType.POISON, 2.7f},
                {BallType.DEMO, 240000f},
                {BallType.CRUSH, 12000f},
                {BallType.CASH, 0.5f},
                {BallType.FIRE, 12000f}
            }}
        };

    private Dictionary<int, Dictionary<BallType, float>> speedBaseStats =
        new Dictionary<int, Dictionary<BallType, float>>
        {
            {1 ,  new Dictionary<BallType, float> {
                {BallType.BASIC, 1f}}},

            {2, new Dictionary<BallType, float>{
                {BallType.SNIPER, 1.375f},
                {BallType.SPLASH, 0.825f}
            }},
            {3, new Dictionary<BallType, float>{
                {BallType.SNIPER, 1.5f},
                {BallType.SPLASH, 0.9f},
                {BallType.POISON, 1.08f},
                {BallType.DEMO, 0.54f}
            }},
            {4, new Dictionary<BallType, float>{
                {BallType.SNIPER, 1.625f},
                {BallType.SPLASH, 0.975f},
                {BallType.POISON, 1.17f},
                {BallType.DEMO, 0.585f},
                {BallType.CRUSH, 1.3f}
            }},
            {5, new Dictionary<BallType, float>{
                {BallType.SNIPER, 1.75f},
                {BallType.SPLASH, 1.05f},
                {BallType.POISON, 1.26f},
                {BallType.DEMO, 0.63f},
                {BallType.CRUSH, 1.4f},
                {BallType.CASH, 1.54f}
            }},
            {6, new Dictionary<BallType, float>{
                {BallType.SNIPER, 1.9375f},
                {BallType.SPLASH, 1.1625f},
                {BallType.POISON, 1.395f},
                {BallType.DEMO, 0.6975f},
                {BallType.CRUSH, 1.55f},
                {BallType.CASH, 1.705f},
                {BallType.FIRE, 1.9375f}
            }},
            {7, new Dictionary<BallType, float>{
                {BallType.SNIPER, 2.1875f},
                {BallType.SPLASH, 1.3125f},
                {BallType.POISON, 1.575f},
                {BallType.DEMO, 0.7875f},
                {BallType.CRUSH, 1.75f},
                {BallType.CASH, 1.925f},
                {BallType.FIRE, 2.1875f}
            }},
            {8, new Dictionary<BallType, float>{
                {BallType.SNIPER, 2.375f},
                {BallType.SPLASH, 1.425f},
                {BallType.POISON, 1.71f},
                {BallType.DEMO, 0.855f},
                {BallType.CRUSH, 1.9f},
                {BallType.CASH, 2.09f},
                {BallType.FIRE, 2.375f}
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
    
    //не помню зачем их добавил
    //private Dictionary<BallType, float> damageMultipliers = new Dictionary<BallType, float>();
    //private Dictionary<BallType, float> speedMultipliers = new Dictionary<BallType, float>();
    #endregion


    private int currentStage = 0;
    private Dictionary<int, float> stagePrice = new Dictionary<int, float> {
        {1, 0f }, {2, 175f}, {3, 7500f}, {4, 175000f}, {5, 15f * (float)Math.Pow(10, 6)}
    };

    private Dictionary<BallType, float> newBallPrice = new Dictionary<BallType, float>();
    private Dictionary<BallType, float> damageUpgradePrice = new Dictionary<BallType, float>();
    private Dictionary<BallType, float> speedUpgradePrice = new Dictionary<BallType, float>();

    private const float priceMultiplier = 0.95f;


    #region New Balls
    public Dictionary<string, float> BuyNewBall(BallType type)
    {
        CashManager cm = GetComponent<CashManager>();
        float price = newBallPrice[type];

        Dictionary<string, float> returnValues = new Dictionary<string, float>();
        
        if (cm.SpendCash(price))
        {
            newBallPrice[type] += price * priceMultiplier;
            InstantiateBall(type);
        }

        returnValues.Add("value", (float)BallsCount(type));
        returnValues.Add("price", newBallPrice[type]);

        return returnValues;
    }

    

    public Dictionary<string, float> OpenNewBall(BallType ballType)
    {
       
        CashManager cm = GetComponent<CashManager>();
        if (cm.SpendCash(stagePrice[currentStage + 1]))
        {
            Dictionary<string, float> returnValues = new Dictionary<string, float>();

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

            returnValues.Add("damage", currentDamage[ballType]);
            returnValues.Add("speed", currentSpeed[ballType]);
            returnValues.Add("count", 1);

            returnValues.Add("damagePrice", damageUpgradePrice[ballType]);
            returnValues.Add("speedPrice", speedUpgradePrice[ballType]);
            returnValues.Add("countPrice", newBallPrice[ballType]);

            return returnValues;
        }

        return null;
    }
    #endregion


    #region Update stats
    public Dictionary<string, float> UpgradeSpeed(BallType type)
    {
        CashManager cm = GetComponent<CashManager>();
        float price = speedUpgradePrice[type];

        Dictionary<string,float> returnValues = new Dictionary<string,float>();

        if (cm.SpendCash(price))
        {
            speedUpgradePrice[type] += price * priceMultiplier;

            if(type == BallType.DEMO)
            {
                speedIncrement[type] *= demoBallSpeedIncrementMultiplier;
            }
            else
            {
                speedIncrement[type] *= allBallsSpeedIncrementMultiplier;
            }

            currentSpeed[type] += speedIncrement[type];

            switch (type)
            {
                case BallType.BASIC:
                    foreach (BasicBall bb in basicBalls)
                    {
                        bb.speed = currentSpeed[type];
                    }
                    break;

                case BallType.SNIPER:
                    foreach(SniperBall sb in sniperBalls)
                    {
                        sb.speed = currentSpeed[type];
                    }
                    break;

                case BallType.SPLASH:
                    foreach(SplashBall sb in splashBalls)
                    {
                        sb.speed = currentSpeed[type];
                    }
                    break;

                case BallType.POISON:
                    foreach(PoisonBall pb in poisonBalls)
                    {
                        pb.speed = currentSpeed[type];
                    }
                    break;

                case BallType.DEMO:
                    foreach(DemoBall db in demoBalls)
                    {
                        db.speed = currentSpeed[type];
                    }
                    break;

                case BallType.CRUSH:
                    foreach(CrushBall cb  in crushBalls)
                    {
                        cb.speed = currentSpeed[type];
                    }
                    break;

                case BallType.CASH:
                    foreach(CashBall cb in cashBalls)
                    {
                        cb.speed = currentSpeed[type];
                    }
                    break;

                case BallType.FIRE:
                    foreach (FireBall fb in fireBalls)
                    {
                        fb.speed = currentSpeed[type];
                    }
                    break;
            }
        }

        returnValues.Add("value", currentSpeed[type]);
        returnValues.Add("price", speedUpgradePrice[type]);


        return returnValues;
    }

    public Dictionary<string, float> UpgradeDamage(BallType type)
    {
        CashManager cm = GetComponent<CashManager>();
        float price = damageUpgradePrice[type];

        Dictionary<string, float> returnValues = new Dictionary<string, float>();

        if (cm.SpendCash(price))
        {
            damageUpgradePrice[type] += price * priceMultiplier;

            if(type == BallType.POISON)
            {
                damageIncrement[type] *= poisonBallDamageIncrementMultiplier;
            }
            else if(type == BallType.CASH)
            {
                damageIncrement[type] *= cashBallDamageIncrementMultiplier;
            }
            else
            {
                damageIncrement[type] *= allBallsDamageIncrementMultiplier;
            }

            currentDamage[type] += damageIncrement[type];

            switch (type)
            {
                case BallType.BASIC:
                    foreach (BasicBall bb in basicBalls)
                    {
                        bb.damage = currentDamage[type];
                    }
                    break;

                case BallType.SNIPER:
                    foreach (SniperBall sb in sniperBalls)
                    {
                        sb.damage = currentDamage[type];
                    }
                    break;

                case BallType.SPLASH:
                    foreach (SplashBall sb in splashBalls)
                    {
                        sb.damage = currentDamage[type];
                    }
                    break;

                case BallType.POISON:
                    foreach (PoisonBall pb in poisonBalls)
                    {
                        pb.damage = currentDamage[type];
                    }
                    break;

                case BallType.DEMO:
                    foreach (DemoBall db in demoBalls)
                    {
                        db.damage = currentDamage[type];
                    }
                    break;

                case BallType.CRUSH:
                    foreach (CrushBall cb in crushBalls)
                    {
                        cb.damage = currentDamage[type];
                    }
                    break;

                case BallType.CASH:
                    foreach (CashBall cb in cashBalls)
                    {
                        cb.damage = currentDamage[type];
                    }
                    break;

                case BallType.FIRE:
                    foreach (FireBall fb in fireBalls)
                    {
                        fb.damage = currentDamage[type];
                    }
                    break;
            }
        }



        returnValues.Add("value", currentDamage[type]);
        returnValues.Add("price", damageUpgradePrice[type]);


        return returnValues;
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

            case BallType.SNIPER:
                ballPrefab = sniperBallPrefab;
                break;

            case BallType.SPLASH:
                ballPrefab = splashBallPrefab;
                break;

            case BallType.POISON:
                ballPrefab = poisonBallPrefab;
                break;

            case BallType.DEMO:
                ballPrefab = demoBallPrefab;
                break;

            case BallType.CRUSH:
                ballPrefab = crushBallPrefab;
                break;

            case BallType.CASH:
                ballPrefab = cashBallPrefab;
                break;

            case BallType.FIRE:
                ballPrefab = fireBallPrefab;
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

            case BallType.SNIPER:
                sniperBalls.Add((SniperBall)bs);
                break;

            case BallType.SPLASH:
                splashBalls.Add((SplashBall)bs);
                break;

            case BallType.POISON:
                poisonBalls.Add((PoisonBall)bs);
                break;

            case BallType.DEMO:
                demoBalls.Add((DemoBall)bs);
                break;

            case BallType.CRUSH:
                crushBalls.Add((CrushBall)bs);
                break;

            case BallType.CASH:
                cashBalls.Add((CashBall)bs);
                break;

            case BallType.FIRE:
                fireBalls.Add((FireBall)bs);
                break;
        }

        GetComponent<BrickManager>().SubscribeBricks(ball);
    }

    public void DestroyBall(BallType ballType)
    {
        GameObject toDestroy = null;
        switch (ballType)
        {
            case BallType.BASIC:
                toDestroy = basicBalls[basicBalls.Count - 1].gameObject;
                basicBalls.Remove(toDestroy.GetComponent<BasicBall>());
                break;

            case BallType.SNIPER:
                toDestroy = sniperBalls[sniperBalls.Count - 1].gameObject;
                sniperBalls.Remove(toDestroy.GetComponent<SniperBall>());
                break;

            case BallType.SPLASH:
                toDestroy = splashBalls[splashBalls.Count - 1].gameObject;
                splashBalls.Remove(toDestroy.GetComponent<SplashBall>());
                break;

            case BallType.POISON:
                toDestroy = poisonBalls[poisonBalls.Count - 1].gameObject;
                poisonBalls.Remove(toDestroy.GetComponent<PoisonBall>());
                break;

            case BallType.DEMO:
                toDestroy = demoBalls[demoBalls.Count - 1].gameObject;
                demoBalls.Remove(toDestroy.GetComponent<DemoBall>());
                break;

            case BallType.CRUSH:
                toDestroy = crushBalls[crushBalls.Count - 1].gameObject;
                crushBalls.Remove(toDestroy.GetComponent<CrushBall>());
                break;

            case BallType.CASH:
                toDestroy = cashBalls[cashBalls.Count - 1].gameObject;
                cashBalls.Remove(toDestroy.GetComponent<CashBall>());
                break;

            case BallType.FIRE:
                toDestroy = fireBalls[fireBalls.Count - 1].gameObject;
                fireBalls.Remove(toDestroy.GetComponent<FireBall>());
                break;

        }

        GetComponent<BrickManager>().UnsubscribeBricks(toDestroy);
    }



    private void Start()
    {
        //OpenNewBall(BallType.BASIC);
        
    }
}
