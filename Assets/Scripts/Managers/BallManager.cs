using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BallManager : MonoBehaviour {

    private const string MAXBALLSKEY = "MAXBALLSALLOWED";

    private const string BASICBALLCOUNTKEY = "BASICBALLCOUNT";
    private const string SPLASHBALLCOUNTKEY = "SPLASHALLCOUNT";
    private const string SNIPERBALLCOUNTKEY = "SNIPERBALLCOUNT";
    private const string POISONBALLCOUNTKEY = "POISONBALLCOUNT";
    private const string DEMOBALLCOUNTKEY = "DEMOBALLCOUNT";
    private const string CASHBALLCOUNTKEY = "CASHBALLCOUNT";
    private const string CRUSHBALLCOUNTKEY = "CRUSHBALLCOUNT";
    private const string FIREBALLCOUNTKEY = "FIREBALLCOUNT";

    private const string BASICBALLCURRENTDAMAGEKEY = "BASICBALLCURRENTDAMAGE";
    private const string SPLASHBALLCURRENTDAMAGEKEY = "SPLASHBALLCURRENTDAMAGE";
    private const string SNIPERBALLCURRENTDAMAGEKEY = "SNIPERBALLCURRENTDAMAGE";
    private const string POISONBALLCURRENTDAMAGEKEY = "POISONBALLCURRENTDAMAGE";
    private const string DEMOBALLCURRENTDAMAGEKEY = "DEMOBALLCURRENTDAMAGE";
    private const string CASHBALLCURRENTDAMAGEKEY = "CASHBALLCURRENTDAMAGE";
    private const string CRUSHBALLCURRENTDAMAGEKEY = "CRUSHBALLCURRENTDAMAGE";
    private const string FIREBALLCURRENTDAMAGEKEY = "FIREBALLCURRENTDAMAGE";

    private const string BASICBALLCURRENTSPEEDKEY = "BASICBALLCURRENTSPEED";
    private const string SPLASHBALLCURRENTSPEEDKEY = "SPLASHBALLCURRENTSPEED";
    private const string SNIPERBALLCURRENTSPEEDKEY = "SNIPERBALLCURRENTSPEED";
    private const string POISONBALLCURRENTSPEEDKEY = "POISONBALLCURRENTSPEED";
    private const string DEMOBALLCURRENTSPEEDKEY = "DEMOBALLCURRENTSPEED";
    private const string CASHBALLCURRENTSPEEDKEY = "CASHBALLCURRENTSPEED";
    private const string CRUSHBALLCURRENTSPEEDKEY = "CRUSHBALLCURRENTSPEED";
    private const string FIREBALLCURRENTSPEEDKEY = "FIREBALLCURRENTSPEED";

    private const string BASICBALLDAMAGEINCREMENTKEY = "BASICBALLDAMAGEINCREMENT";
    private const string SPLASHBALLDAMAGEINCREMENTKEY = "SPLASHBALLDAMAGEINCREMENT";
    private const string SNIPERBALLDAMAGEINCREMENTKEY = "SNIPERBALLDAMAGEINCREMENT";
    private const string POISONBALLDAMAGEINCREMENTKEY = "POISONBALLDAMAGEINCREMENT";
    private const string DEMOBALLDAMAGEINCREMENTKEY = "DEMOBALLDAMAGEINCREMENT";
    private const string CASHBALLDAMAGEINCREMENTKEY = "CASHBALLDAMAGEINCREMENT";
    private const string CRUSHBALLDAMAGEINCREMENTKEY = "CRUSHBALLDAMAGEINCREMENT";
    private const string FIREBALLDAMAGEINCREMENTKEY = "FIREBALLDAMAGEINCREMENT";

    private const string BASICBALLNEWBALLPRICEKEY = "BASICBALLNEWBALLPRICE";
    private const string BASICBALLDAMAGEUPGRADEPRICEKEY = "BASICBALLDAMAGEUPGRADEPRICE";
    private const string BASICBALLSPEEDUPGRADEPRICEKEY = "BASICBALLSPEEDUPGRADEPRICE";

    private const string SPLASHBALLNEWBALLPRICEKEY = "SPLASHBALLNEWBALLPRICE";
    private const string SPLASHBALLDAMAGEUPGRADEPRICEKEY = "SPLASHBALLDAMAGEUPGRADEPRICE";
    private const string SPLASHBALLSPEEDUPGRADEPRICEKEY = "SPLASHBALLSPEEDUPGRADEPRICE";

    private const string SNIPERBALLNEWBALLPRICEKEY = "SNIPERBALLNEWBALLPRICE";
    private const string SNIPERBALLDAMAGEUPGRADEPRICEKEY = "SNIPERBALLDAMAGEUPGRADEPRICE";
    private const string SNIPERBALLSPEEDUPGRADEPRICEKEY = "SNIPERBALLSPEEDUPGRADEPRICE";

    private const string POISONBALLNEWBALLPRICEKEY = "POISONBALLNEWBALLPRICE";
    private const string POISONBALLDAMAGEUPGRADEPRICEKEY = "POISONBALLDAMAGEUPGRADEPRICE";
    private const string POISONBALLSPEEDUPGRADEPRICEKEY = "POISONBALLSPEEDUPGRADEPRICE";

    private const string DEMOBALLNEWBALLPRICEKEY = "DEMOBALLNEWBALLPRICE";
    private const string DEMOBALLDAMAGEUPGRADEPRICEKEY = "DEMOBALLDAMAGEUPGRADEPRICE";
    private const string DEMOBALLSPEEDUPGRADEPRICEKEY = "DEMOBALLSPEEDUPGRADEPRICE";

    private const string CASHBALLNEWBALLPRICEKEY = "CASHBALLNEWBALLPRICE";
    private const string CASHBALLDAMAGEUPGRADEPRICEKEY = "CASHBALLDAMAGEUPGRADEPRICE";
    private const string CASHBALLSPEEDUPGRADEPRICEKEY = "CASHBALLSPEEDUPGRADEPRICE";

    private const string CRUSHBALLNEWBALLPRICEKEY = "CRUSHBALLNEWBALLPRICE";
    private const string CRUSHBALLDAMAGEUPGRADEPRICEKEY = "CRUSHBALLDAMAGEUPGRADEPRICE";
    private const string CRUSHBALLSPEEDUPGRADEPRICEKEY = "CRUSHBALLSPEEDUPGRADEPRICE";

    private const string FIREBALLNEWBALLPRICEKEY = "FIREBALLNEWBALLPRICE";
    private const string FIREBALLDAMAGEUPGRADEPRICEKEY = "FIREBALLDAMAGEUPGRADEPRICE";
    private const string FIREBALLSPEEDUPGRADEPRICEKEY = "FIREBALLSPEEDUPGRADEPRICE";

    private const string CURRENTSTAGEKEY = "CURRENTSTAGE";


    [SerializeField]
    private TMP_Text ballsCountText;

    private CashManager cashManager;
    private PrestigeManager prestigeManager;
    private int maxBallsAllowed = 50;

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


    public int BallsCount(BallType ballType)
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

            case BallType.SMALLCRUSH:
            default:
                break;
        }

        return -1;
    }

    public int AllBallsCount()
    {
        return basicBalls.Count + sniperBalls.Count +
            splashBalls.Count + poisonBalls.Count +
            demoBalls.Count + crushBalls.Count +
            cashBalls.Count + fireBalls.Count;
    }

    private void UpdateMaxBallsCount(int count)
    {
        maxBallsAllowed += count;
        SaveLoadData<int> sld = new SaveLoadData<int>(MAXBALLSKEY, maxBallsAllowed);
        sld.SaveData();
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

    public float GetCurrentDamage(BallType type){ return currentDamage[type];}

    public float GetCurrentSpeed(BallType type) { return currentSpeed[type]; }

    //не помню зачем их добавил
    //private Dictionary<BallType, float> damageMultipliers = new Dictionary<BallType, float>();
    //private Dictionary<BallType, float> speedMultipliers = new Dictionary<BallType, float>();
    #endregion



    #region Stage data
    private int currentStage = 0;
    private Dictionary<int, float> stagePrice = new Dictionary<int, float> {
        {1, 0f }, {2, 175f}, {3, 7500f}, {4, 175000f}, {5, 15f * (float)Math.Pow(10, 6)}
    };

    public int GetCurrentStage() { return currentStage;}

    public float NextStagePrice()
    {
        if (currentStage < 8)
        {
            return stagePrice[currentStage + 1];
        }
        return -1f;
    }

    private Dictionary<BallType, int> ballTypesOnStage = new Dictionary<BallType, int>
    {
        { BallType.BASIC, 1}, { BallType.SNIPER, 2}, { BallType.SPLASH, 2},
        { BallType.POISON, 3}, { BallType.DEMO, 3}, { BallType.CRUSH, 4},
        { BallType.CASH, 5}, { BallType.FIRE, 6}
    };

    private Dictionary<BallType, bool> ballOpened = new Dictionary<BallType, bool> {
        {BallType.BASIC, false}, {BallType.SPLASH, false}, {BallType.SNIPER, false},
        {BallType.POISON, false}, {BallType.DEMO, false}, {BallType.CRUSH, false},
        {BallType.CASH, false}, {BallType.FIRE, false}
    };

    public List<BallType> AllowedTypes()
    {
        List<BallType> types = new List<BallType>();

        int nextStage = currentStage + 1;

        foreach (BallType t in ballTypesOnStage.Keys)
        {
            if (ballTypesOnStage[t] <= nextStage && !ballOpened[t])
            {
                types.Add(t);
            }
        }

        return types;
    }

    private Dictionary<BallType, float> newBallPrice = new Dictionary<BallType, float>();
    private Dictionary<BallType, float> damageUpgradePrice = new Dictionary<BallType, float>();
    private Dictionary<BallType, float> speedUpgradePrice = new Dictionary<BallType, float>();

    private const float priceMultiplier = 0.95f;

    public float GetNewBallPrice(BallType ballType)
    {
        return newBallPrice[ballType];
    }

    public float GetDamageUpgradePrice(BallType ballType)
    {
        return damageUpgradePrice[ballType];
    }

    public float GetSpeedUpgradePrice(BallType ballType)
    {
        return speedUpgradePrice[ballType];
    }

    #endregion

   


    #region New Balls
    public Dictionary<string, float> BuyNewBall(BallType type)
    {

        float price = newBallPrice[type];

        Dictionary<string, float> returnValues = new Dictionary<string, float>();
        
        if(AllBallsCount() < maxBallsAllowed)
        {
            if (cashManager.SpendSoftCash(price))
            {
                newBallPrice[type] += price * priceMultiplier;
                InstantiateBall(type);

                switch (type)
                {
                    case BallType.BASIC:
                        SaveLoadData<float> sldPrice = new SaveLoadData<float>(BASICBALLNEWBALLPRICEKEY, newBallPrice[type]);
                        sldPrice.SaveData();
                        SaveLoadData<int> sldCount = new SaveLoadData<int>(BASICBALLCOUNTKEY, BallsCount(type));
                        sldCount.SaveData();
                        break;

                    case BallType.SPLASH:
                        SaveLoadData<float> sldSplashBallPrice = new SaveLoadData<float>(SPLASHBALLNEWBALLPRICEKEY, newBallPrice[type]);
                        sldSplashBallPrice.SaveData();
                        SaveLoadData<int> sldSplashBallCount = new SaveLoadData<int>(SPLASHBALLCOUNTKEY, BallsCount(type));
                        sldSplashBallCount.SaveData();
                        break;

                    case BallType.SNIPER:
                        SaveLoadData<float> sldSniperBallPrice = new SaveLoadData<float>(SNIPERBALLNEWBALLPRICEKEY, newBallPrice[type]);
                        sldSniperBallPrice.SaveData();
                        SaveLoadData<int> sldSniperBallCount = new SaveLoadData<int>(SNIPERBALLCOUNTKEY, BallsCount(type));
                        sldSniperBallCount.SaveData();
                        break;

                    case BallType.POISON:
                        SaveLoadData<float> sldPoisonBallPrice = new SaveLoadData<float>(POISONBALLNEWBALLPRICEKEY, newBallPrice[type]);
                        sldPoisonBallPrice.SaveData();
                        SaveLoadData<int> sldPoisonBallCount = new SaveLoadData<int>(POISONBALLCOUNTKEY, BallsCount(type));
                        sldPoisonBallCount.SaveData();
                        break;

                    case BallType.DEMO:
                        SaveLoadData<float> sldDemoBallPrice = new SaveLoadData<float>(DEMOBALLNEWBALLPRICEKEY, newBallPrice[type]);
                        sldDemoBallPrice.SaveData();
                        SaveLoadData<int> sldDemoBallCount = new SaveLoadData<int>(DEMOBALLCOUNTKEY, BallsCount(type));
                        sldDemoBallCount.SaveData();
                        break;

                    case BallType.CASH:
                        SaveLoadData<float> sldCashBallPrice = new SaveLoadData<float>(CASHBALLNEWBALLPRICEKEY, newBallPrice[type]);
                        sldCashBallPrice.SaveData();
                        SaveLoadData<int> sldCashBallCount = new SaveLoadData<int>(CASHBALLCOUNTKEY, BallsCount(type));
                        sldCashBallCount.SaveData();
                        break;

                    case BallType.CRUSH:
                        SaveLoadData<float> sldCrushBallPrice = new SaveLoadData<float>(CRUSHBALLNEWBALLPRICEKEY, newBallPrice[type]);
                        sldCrushBallPrice.SaveData();
                        SaveLoadData<int> sldCrushBallCount = new SaveLoadData<int>(CRUSHBALLCOUNTKEY, BallsCount(type));
                        sldCrushBallCount.SaveData();
                        break;

                    case BallType.FIRE:
                        SaveLoadData<float> sldFireBallPrice = new SaveLoadData<float>(FIREBALLNEWBALLPRICEKEY, newBallPrice[type]);
                        sldFireBallPrice.SaveData();
                        SaveLoadData<int> sldFireBallCount = new SaveLoadData<int>(FIREBALLCOUNTKEY, BallsCount(type));
                        sldFireBallCount.SaveData();
                        break;
                }

                ballsCountText.text = $"{AllBallsCount()}/{maxBallsAllowed}";
            }
        }
        

        returnValues.Add("value", (float)BallsCount(type));
        returnValues.Add("price", newBallPrice[type]);

        return returnValues;
    }

    

    public Dictionary<string, float> OpenNewBall(BallType ballType)
    {
        if(AllBallsCount() < maxBallsAllowed)
        {
            if (cashManager.SpendSoftCash(stagePrice[currentStage + 1]))
            {
                ballOpened[ballType] = true;
                Dictionary<string, float> returnValues = new Dictionary<string, float>();

                currentStage++;

                currentDamage.Add(ballType, powerBaseStats[currentStage][ballType]);
                currentSpeed.Add(ballType, speedBaseStats[currentStage][ballType]);


                damageIncrement.Add(ballType, powerBaseStats[currentStage][ballType]);

                damageUpgradePrice.Add(ballType, stagePrice[currentStage] == 0f ? 6f : stagePrice[currentStage]);
                speedUpgradePrice.Add(ballType, stagePrice[currentStage] == 0f ? 6f : stagePrice[currentStage]);
                newBallPrice.Add(ballType, stagePrice[currentStage] == 0f ? 6f :
                    stagePrice[currentStage] + (stagePrice[currentStage] * priceMultiplier));

                List<SaveLoadData<float>> sldsFloat = null;
                List<SaveLoadData<int>> sldsInt = null;

                switch (ballType)
                {
                    case BallType.BASIC:
                        sldsFloat = new()
                        {
                            new SaveLoadData<float>(BASICBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[ballType]),
                            new SaveLoadData<float>(BASICBALLCURRENTDAMAGEKEY, currentDamage[ballType]),
                            new SaveLoadData<float>(BASICBALLDAMAGEINCREMENTKEY, damageIncrement[ballType]),
                            new SaveLoadData<float>(BASICBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[ballType]),
                            new SaveLoadData<float>(BASICBALLCURRENTSPEEDKEY, currentSpeed[ballType]),
                            new SaveLoadData<float>(BASICBALLNEWBALLPRICEKEY, newBallPrice[ballType])
                        };

                        sldsInt = new()
                        {
                            new SaveLoadData<int>(BASICBALLCOUNTKEY, 1),
                        };
                        break;

                    case BallType.SPLASH:
                        sldsFloat = new List<SaveLoadData<float>>
                        {
                            new SaveLoadData<float>(SPLASHBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[ballType]),
                            new SaveLoadData<float>(SPLASHBALLCURRENTDAMAGEKEY, currentDamage[ballType]),
                            new SaveLoadData<float>(SPLASHBALLDAMAGEINCREMENTKEY, damageIncrement[ballType]),
                            new SaveLoadData<float>(SPLASHBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[ballType]),
                            new SaveLoadData<float>(SPLASHBALLCURRENTSPEEDKEY, currentSpeed[ballType]),
                            new SaveLoadData<float>(SPLASHBALLNEWBALLPRICEKEY, newBallPrice[ballType])
                        };

                        sldsInt = new List<SaveLoadData<int>>
                        {
                            new SaveLoadData<int>(SPLASHBALLCOUNTKEY, 1)
                        };
                        break;

                    case BallType.SNIPER:
                        sldsFloat = new List<SaveLoadData<float>>
                        {
                            new SaveLoadData<float>(SNIPERBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[ballType]),
                            new SaveLoadData<float>(SNIPERBALLCURRENTDAMAGEKEY, currentDamage[ballType]),
                            new SaveLoadData<float>(SNIPERBALLDAMAGEINCREMENTKEY, damageIncrement[ballType]),
                            new SaveLoadData<float>(SNIPERBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[ballType]),
                            new SaveLoadData<float>(SNIPERBALLCURRENTSPEEDKEY, currentSpeed[ballType]),
                            new SaveLoadData<float>(SNIPERBALLNEWBALLPRICEKEY, newBallPrice[ballType])
                        };

                        sldsInt = new List<SaveLoadData<int>>
                        {
                            new SaveLoadData<int>(SNIPERBALLCOUNTKEY, 1)
                        };
                        break;

                    case BallType.POISON:
                        sldsFloat = new List<SaveLoadData<float>>
                        {
                            new SaveLoadData<float>(POISONBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[ballType]),
                            new SaveLoadData<float>(POISONBALLCURRENTDAMAGEKEY, currentDamage[ballType]),
                            new SaveLoadData<float>(POISONBALLDAMAGEINCREMENTKEY, damageIncrement[ballType]),
                            new SaveLoadData<float>(POISONBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[ballType]),
                            new SaveLoadData<float>(POISONBALLCURRENTSPEEDKEY, currentSpeed[ballType]),
                            new SaveLoadData<float>(POISONBALLNEWBALLPRICEKEY, newBallPrice[ballType])
                        };

                        sldsInt = new List<SaveLoadData<int>>
                        {
                            new SaveLoadData<int>(POISONBALLCOUNTKEY, 1)
                        };
                        break;

                    case BallType.DEMO:
                        sldsFloat = new List<SaveLoadData<float>>
                        {
                            new SaveLoadData<float>(DEMOBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[ballType]),
                            new SaveLoadData<float>(DEMOBALLCURRENTDAMAGEKEY, currentDamage[ballType]),
                            new SaveLoadData<float>(DEMOBALLDAMAGEINCREMENTKEY, damageIncrement[ballType]),
                            new SaveLoadData<float>(DEMOBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[ballType]),
                            new SaveLoadData<float>(DEMOBALLCURRENTSPEEDKEY, currentSpeed[ballType]),
                            new SaveLoadData<float>(DEMOBALLNEWBALLPRICEKEY, newBallPrice[ballType])
                        };

                        sldsInt = new List<SaveLoadData<int>>
                        {
                            new SaveLoadData<int>(DEMOBALLCOUNTKEY, 1)
                        };
                        break;

                    case BallType.CASH:
                        sldsFloat = new List<SaveLoadData<float>>
                        {
                            new SaveLoadData<float>(CASHBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[ballType]),
                            new SaveLoadData<float>(CASHBALLCURRENTDAMAGEKEY, currentDamage[ballType]),
                            new SaveLoadData<float>(CASHBALLDAMAGEINCREMENTKEY, damageIncrement[ballType]),
                            new SaveLoadData<float>(CASHBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[ballType]),
                            new SaveLoadData<float>(CASHBALLCURRENTSPEEDKEY, currentSpeed[ballType]),
                            new SaveLoadData<float>(CASHBALLNEWBALLPRICEKEY, newBallPrice[ballType])
                        };

                        sldsInt = new List<SaveLoadData<int>>
                        {
                            new SaveLoadData<int>(CASHBALLCOUNTKEY, 1)
                        };
                        break;

                    case BallType.CRUSH:
                        sldsFloat = new List<SaveLoadData<float>>
                        {
                            new SaveLoadData<float>(CRUSHBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[ballType]),
                            new SaveLoadData<float>(CRUSHBALLCURRENTDAMAGEKEY, currentDamage[ballType]),
                            new SaveLoadData<float>(CRUSHBALLDAMAGEINCREMENTKEY, damageIncrement[ballType]),
                            new SaveLoadData<float>(CRUSHBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[ballType]),
                            new SaveLoadData<float>(CRUSHBALLCURRENTSPEEDKEY, currentSpeed[ballType]),
                            new SaveLoadData<float>(CRUSHBALLNEWBALLPRICEKEY, newBallPrice[ballType])
                        };

                        sldsInt = new List<SaveLoadData<int>>
                        {
                            new SaveLoadData<int>(CRUSHBALLCOUNTKEY, 1)
                        };
                        break;

                    case BallType.FIRE:
                        sldsFloat = new List<SaveLoadData<float>>
                        {
                            new SaveLoadData<float>(FIREBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[ballType]),
                            new SaveLoadData<float>(FIREBALLCURRENTDAMAGEKEY, currentDamage[ballType]),
                            new SaveLoadData<float>(FIREBALLDAMAGEINCREMENTKEY, damageIncrement[ballType]),
                            new SaveLoadData<float>(FIREBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[ballType]),
                            new SaveLoadData<float>(FIREBALLCURRENTSPEEDKEY, currentSpeed[ballType]),
                            new SaveLoadData<float>(FIREBALLNEWBALLPRICEKEY, newBallPrice[ballType])
                        };

                        sldsInt = new List<SaveLoadData<int>>
                        {new SaveLoadData<int>(FIREBALLCOUNTKEY, 1)
                        };
                        break;
                }

                sldsInt.Add(new SaveLoadData<int>(CURRENTSTAGEKEY, currentStage));

                foreach(SaveLoadData<float> sld in sldsFloat)
                {
                    sld.SaveData();
                }
                foreach(SaveLoadData<int> sld in sldsInt)
                {
                    sld.SaveData();
                }

                InstantiateBall(ballType);

                returnValues.Add("damage", currentDamage[ballType]);
                returnValues.Add("speed", currentSpeed[ballType]);
                returnValues.Add("count", 1);

                returnValues.Add("damagePrice", damageUpgradePrice[ballType]);
                returnValues.Add("speedPrice", speedUpgradePrice[ballType]);
                returnValues.Add("countPrice", newBallPrice[ballType]);

                ballsCountText.text = $"{AllBallsCount()}/{maxBallsAllowed}";

                return returnValues;
            }
        }

        return null;
    }
    #endregion


    #region Update stats
    public Dictionary<string, float> UpgradeSpeed(BallType type)
    {
        float price = speedUpgradePrice[type];

        Dictionary<string,float> returnValues = new Dictionary<string,float>();

        if (cashManager.SpendSoftCash(price))
        {
            speedUpgradePrice[type] += price * priceMultiplier;

            if(type == BallType.DEMO)
            {
                currentSpeed[type] += demoBallSpeedIncrementMultiplier;
            }
            else
            {
                currentSpeed[type] += allBallsSpeedIncrementMultiplier;
            }


            switch (type)
            {
                case BallType.BASIC:
                    foreach (BasicBall bb in basicBalls)
                    {
                        bb.speed = currentSpeed[type];
                    }
                    SaveLoadData<float> sldUpPrice = new SaveLoadData<float>(BASICBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[type]);
                    sldUpPrice.SaveData();
                    SaveLoadData<float> sldSpeed = new SaveLoadData<float>(BASICBALLCURRENTSPEEDKEY, currentSpeed[type]);
                    sldSpeed.SaveData();
                    break;

                case BallType.SNIPER:
                    foreach(SniperBall sb in sniperBalls)
                    {
                        sb.speed = currentSpeed[type];
                    }
                    SaveLoadData<float> sldSniperUpPrice = new SaveLoadData<float>(SNIPERBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[type]);
                    sldSniperUpPrice.SaveData();
                    SaveLoadData<float> sldSniperSpeed = new SaveLoadData<float>(SNIPERBALLCURRENTSPEEDKEY, currentSpeed[type]);
                    sldSniperSpeed.SaveData();
                    break;

                case BallType.SPLASH:
                    foreach(SplashBall sb in splashBalls)
                    {
                        sb.speed = currentSpeed[type];
                    }
                    SaveLoadData<float> sldSplashUpPrice = new SaveLoadData<float>(SPLASHBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[type]);
                    sldSplashUpPrice.SaveData();
                    SaveLoadData<float> sldSplashSpeed = new SaveLoadData<float>(SPLASHBALLCURRENTSPEEDKEY, currentSpeed[type]);
                    sldSplashSpeed.SaveData();
                    break;

                case BallType.POISON:
                    foreach(PoisonBall pb in poisonBalls)
                    {
                        pb.speed = currentSpeed[type];
                    }
                    SaveLoadData<float> sldPoisonUpPrice = new SaveLoadData<float>(POISONBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[type]);
                    sldPoisonUpPrice.SaveData();
                    SaveLoadData<float> sldPoisonSpeed = new SaveLoadData<float>(POISONBALLCURRENTSPEEDKEY, currentSpeed[type]);
                    sldPoisonSpeed.SaveData();
                    break;

                case BallType.DEMO:
                    foreach(DemoBall db in demoBalls)
                    {
                        db.speed = currentSpeed[type];
                    }
                    SaveLoadData<float> sldDemoUpPrice = new SaveLoadData<float>(DEMOBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[type]);
                    sldDemoUpPrice.SaveData();
                    SaveLoadData<float> sldDemoSpeed = new SaveLoadData<float>(DEMOBALLCURRENTSPEEDKEY, currentSpeed[type]);
                    sldDemoSpeed.SaveData();
                    break;

                case BallType.CRUSH:
                    foreach(CrushBall cb  in crushBalls)
                    {
                        cb.speed = currentSpeed[type];
                    }
                    SaveLoadData<float> sldCrushUpPrice = new SaveLoadData<float>(CRUSHBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[type]);
                    sldCrushUpPrice.SaveData();
                    SaveLoadData<float> sldCrushSpeed = new SaveLoadData<float>(CRUSHBALLCURRENTSPEEDKEY, currentSpeed[type]);
                    sldCrushSpeed.SaveData();
                    break;

                case BallType.CASH:
                    foreach(CashBall cb in cashBalls)
                    {
                        cb.speed = currentSpeed[type];
                    }
                    SaveLoadData<float> sldCashUpPrice = new SaveLoadData<float>(CASHBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[type]);
                    sldCashUpPrice.SaveData();
                    SaveLoadData<float> sldCashSpeed = new SaveLoadData<float>(CASHBALLCURRENTSPEEDKEY, currentSpeed[type]);
                    sldCashSpeed.SaveData();
                    break;

                case BallType.FIRE:
                    foreach (FireBall fb in fireBalls)
                    {
                        fb.speed = currentSpeed[type];
                    }
                    SaveLoadData<float> sldFireUpPrice = new SaveLoadData<float>(FIREBALLSPEEDUPGRADEPRICEKEY, speedUpgradePrice[type]);
                    sldFireUpPrice.SaveData();
                    SaveLoadData<float> sldFireSpeed = new SaveLoadData<float>(FIREBALLCURRENTSPEEDKEY, currentSpeed[type]);
                    sldFireSpeed.SaveData();
                    break;
            }
        }

        returnValues.Add("value", currentSpeed[type]);
        returnValues.Add("price", speedUpgradePrice[type]);


        return returnValues;
    }

    public Dictionary<string, float> UpgradeDamage(BallType type)
    {
        float price = damageUpgradePrice[type];

        Dictionary<string, float> returnValues = new Dictionary<string, float>();

        if (cashManager.SpendSoftCash(price))
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

            List<SaveLoadData<float>> slds = null;

            switch (type)
            {
                case BallType.BASIC:
                    foreach (BasicBall bb in basicBalls)
                    {
                        bb.damage = currentDamage[type];
                    }
                    slds = new()
                    {
                        new SaveLoadData<float>(BASICBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[type]),
                        new SaveLoadData<float>(BASICBALLCURRENTDAMAGEKEY, currentDamage[type]),
                        new SaveLoadData<float>(BASICBALLDAMAGEINCREMENTKEY, damageIncrement[type])
                    };
                    break;

                case BallType.SNIPER:
                    foreach (SniperBall sb in sniperBalls)
                    {
                        sb.damage = currentDamage[type];
                    }
                    slds = new List<SaveLoadData<float>>
                    {
                        new SaveLoadData<float>(SNIPERBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[type]),
                        new SaveLoadData<float>(SNIPERBALLCURRENTDAMAGEKEY, currentDamage[type]),
                        new SaveLoadData<float>(SNIPERBALLDAMAGEINCREMENTKEY, damageIncrement[type])
                    };
                    break;

                case BallType.SPLASH:
                    foreach (SplashBall sb in splashBalls)
                    {
                        sb.damage = currentDamage[type];
                    }
                    slds = new()
                    {
                        new SaveLoadData<float>(SPLASHBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[type]),
                        new SaveLoadData<float>(SPLASHBALLCURRENTDAMAGEKEY, currentDamage[type]),
                        new SaveLoadData<float>(SPLASHBALLDAMAGEINCREMENTKEY, damageIncrement[type])
                    };
                    break;

                case BallType.POISON:
                    foreach (PoisonBall pb in poisonBalls)
                    {
                        pb.damage = currentDamage[type];
                    }
                    slds = new List<SaveLoadData<float>>
                    {
                        new SaveLoadData<float>(POISONBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[type]),
                        new SaveLoadData<float>(POISONBALLCURRENTDAMAGEKEY, currentDamage[type]),
                        new SaveLoadData<float>(POISONBALLDAMAGEINCREMENTKEY, damageIncrement[type])
                    };
                    break;

                case BallType.DEMO:
                    foreach (DemoBall db in demoBalls)
                    {
                        db.damage = currentDamage[type];
                    }
                    slds = new List<SaveLoadData<float>>
                    {
                        new SaveLoadData<float>(DEMOBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[type]),
                        new SaveLoadData<float>(DEMOBALLCURRENTDAMAGEKEY, currentDamage[type]),
                        new SaveLoadData<float>(DEMOBALLDAMAGEINCREMENTKEY, damageIncrement[type])
                    };
                    break;

                case BallType.CRUSH:
                    foreach (CrushBall cb in crushBalls)
                    {
                        cb.damage = currentDamage[type];
                    }
                    slds = new List<SaveLoadData<float>>
                    {
                        new SaveLoadData<float>(CRUSHBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[type]),
                        new SaveLoadData<float>(CRUSHBALLCURRENTDAMAGEKEY, currentDamage[type]),
                        new SaveLoadData<float>(CRUSHBALLDAMAGEINCREMENTKEY, damageIncrement[type])
                    };
                    break;

                case BallType.CASH:
                    foreach (CashBall cb in cashBalls)
                    {
                        cb.damage = currentDamage[type];
                    }
                    slds = new List<SaveLoadData<float>>
                    {
                        new SaveLoadData<float>(CASHBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[type]),
                        new SaveLoadData<float>(CASHBALLCURRENTDAMAGEKEY, currentDamage[type]),
                        new SaveLoadData<float>(CASHBALLDAMAGEINCREMENTKEY, damageIncrement[type])
                    };
                    break;

                case BallType.FIRE:
                    foreach (FireBall fb in fireBalls)
                    {
                        fb.damage = currentDamage[type];
                    }
                    slds = new List<SaveLoadData<float>>
                    {
                        new SaveLoadData<float>(FIREBALLDAMAGEUPGRADEPRICEKEY, damageUpgradePrice[type]),
                        new SaveLoadData<float>(FIREBALLCURRENTDAMAGEKEY, currentDamage[type]),
                        new SaveLoadData<float>(FIREBALLDAMAGEINCREMENTKEY, damageIncrement[type])
                    };
                    break;
            }
            foreach (SaveLoadData<float> sld in slds)
            {
                sld.SaveData();
            }
        }



        returnValues.Add("value", currentDamage[type]);
        returnValues.Add("price", damageUpgradePrice[type]);


        return returnValues;
    }
    #endregion


    #region Balls appearing
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

    #endregion


    private void Start()
    {
        LoadData();

        cashManager = GetComponent<CashManager>();
        prestigeManager = GetComponent<PrestigeManager>();
        prestigeManager.ballsCountPrestiged += UpdateMaxBallsCount;
        OpenNewBall(BallType.BASIC);
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(MAXBALLSKEY))
        {
            SaveLoadData<int> sld = new SaveLoadData<int>(MAXBALLSKEY);
            maxBallsAllowed = sld.LoadData();
        }

        if(PlayerPrefs.HasKey(CURRENTSTAGEKEY))
        {
            SaveLoadData<int> sld = new SaveLoadData<int>(CURRENTSTAGEKEY);
            currentStage = sld.LoadData();
        }

        if(PlayerPrefs.HasKey(BASICBALLCOUNTKEY))
        {
            currentDamage[BallType.BASIC] = new SaveLoadData<float>(BASICBALLCURRENTDAMAGEKEY).LoadData();
            currentSpeed[BallType.BASIC] = new SaveLoadData<float>(BASICBALLCURRENTSPEEDKEY).LoadData();
            damageIncrement[BallType.BASIC] = new SaveLoadData<float>(BASICBALLDAMAGEINCREMENTKEY).LoadData();
            
            damageUpgradePrice[BallType.BASIC] = new SaveLoadData<float>(BASICBALLDAMAGEUPGRADEPRICEKEY).LoadData();
            speedUpgradePrice[BallType.BASIC] = new SaveLoadData<float>(BASICBALLSPEEDUPGRADEPRICEKEY).LoadData();
            newBallPrice[BallType.BASIC] = new SaveLoadData<float>(BASICBALLNEWBALLPRICEKEY).LoadData();

            ballOpened[BallType.BASIC] = true;

            int count = new SaveLoadData<int>(BASICBALLCOUNTKEY).LoadData();
            for(int i=0; i<count; i++)
            {
                InstantiateBall(BallType.BASIC);
            }
        }

        if (PlayerPrefs.HasKey(SPLASHBALLCOUNTKEY))
        {
            currentDamage[BallType.SPLASH] = new SaveLoadData<float>(SPLASHBALLCURRENTDAMAGEKEY).LoadData();
            currentSpeed[BallType.SPLASH] = new SaveLoadData<float>(SPLASHBALLCURRENTSPEEDKEY).LoadData();
            damageIncrement[BallType.SPLASH] = new SaveLoadData<float>(SPLASHBALLDAMAGEINCREMENTKEY).LoadData();

            damageUpgradePrice[BallType.SPLASH] = new SaveLoadData<float>(SPLASHBALLDAMAGEUPGRADEPRICEKEY).LoadData();
            speedUpgradePrice[BallType.SPLASH] = new SaveLoadData<float>(SPLASHBALLSPEEDUPGRADEPRICEKEY).LoadData();
            newBallPrice[BallType.SPLASH] = new SaveLoadData<float>(SPLASHBALLNEWBALLPRICEKEY).LoadData();

            ballOpened[BallType.SPLASH] = true;

            int count = new SaveLoadData<int>(SPLASHBALLCOUNTKEY).LoadData();
            for (int i = 0; i < count; i++)
            {
                InstantiateBall(BallType.SPLASH);
            }
        }

        if (PlayerPrefs.HasKey(SNIPERBALLCOUNTKEY))
        {
            currentDamage[BallType.SNIPER] = new SaveLoadData<float>(SNIPERBALLCURRENTDAMAGEKEY).LoadData();
            currentSpeed[BallType.SNIPER] = new SaveLoadData<float>(SNIPERBALLCURRENTSPEEDKEY).LoadData();
            damageIncrement[BallType.SNIPER] = new SaveLoadData<float>(SNIPERBALLDAMAGEINCREMENTKEY).LoadData();

            damageUpgradePrice[BallType.SNIPER] = new SaveLoadData<float>(SNIPERBALLDAMAGEUPGRADEPRICEKEY).LoadData();
            speedUpgradePrice[BallType.SNIPER] = new SaveLoadData<float>(SNIPERBALLSPEEDUPGRADEPRICEKEY).LoadData();
            newBallPrice[BallType.SNIPER] = new SaveLoadData<float>(SNIPERBALLNEWBALLPRICEKEY).LoadData();

            ballOpened[BallType.SNIPER] = true;

            int count = new SaveLoadData<int>(SNIPERBALLCOUNTKEY).LoadData();
            for (int i = 0; i < count; i++)
            {
                InstantiateBall(BallType.SNIPER);
            }
        }

        if (PlayerPrefs.HasKey(POISONBALLCOUNTKEY))
        {
            currentDamage[BallType.POISON] = new SaveLoadData<float>(POISONBALLCURRENTDAMAGEKEY).LoadData();
            currentSpeed[BallType.POISON] = new SaveLoadData<float>(POISONBALLCURRENTSPEEDKEY).LoadData();
            damageIncrement[BallType.POISON] = new SaveLoadData<float>(POISONBALLDAMAGEINCREMENTKEY).LoadData();

            damageUpgradePrice[BallType.POISON] = new SaveLoadData<float>(POISONBALLDAMAGEUPGRADEPRICEKEY).LoadData();
            speedUpgradePrice[BallType.POISON] = new SaveLoadData<float>(POISONBALLSPEEDUPGRADEPRICEKEY).LoadData();
            newBallPrice[BallType.POISON] = new SaveLoadData<float>(POISONBALLNEWBALLPRICEKEY).LoadData();

            ballOpened[BallType.POISON] = true;

            int count = new SaveLoadData<int>(POISONBALLCOUNTKEY).LoadData();
            for (int i = 0; i < count; i++)
            {
                InstantiateBall(BallType.POISON);
            }
        }

        if (PlayerPrefs.HasKey(DEMOBALLCOUNTKEY))
        {
            currentDamage[BallType.DEMO] = new SaveLoadData<float>(DEMOBALLCURRENTDAMAGEKEY).LoadData();
            currentSpeed[BallType.DEMO] = new SaveLoadData<float>(DEMOBALLCURRENTSPEEDKEY).LoadData();
            damageIncrement[BallType.DEMO] = new SaveLoadData<float>(DEMOBALLDAMAGEINCREMENTKEY).LoadData();

            damageUpgradePrice[BallType.DEMO] = new SaveLoadData<float>(DEMOBALLDAMAGEUPGRADEPRICEKEY).LoadData();
            speedUpgradePrice[BallType.DEMO] = new SaveLoadData<float>(DEMOBALLSPEEDUPGRADEPRICEKEY).LoadData();
            newBallPrice[BallType.DEMO] = new SaveLoadData<float>(DEMOBALLNEWBALLPRICEKEY).LoadData();

            ballOpened[BallType.DEMO] = true;

            int count = new SaveLoadData<int>(DEMOBALLCOUNTKEY).LoadData();
            for (int i = 0; i < count; i++)
            {
                InstantiateBall(BallType.DEMO);
            }
        }

        if (PlayerPrefs.HasKey(CASHBALLCOUNTKEY))
        {
            currentDamage[BallType.CASH] = new SaveLoadData<float>(CASHBALLCURRENTDAMAGEKEY).LoadData();
            currentSpeed[BallType.CASH] = new SaveLoadData<float>(CASHBALLCURRENTSPEEDKEY).LoadData();
            damageIncrement[BallType.CASH] = new SaveLoadData<float>(CASHBALLDAMAGEINCREMENTKEY).LoadData();

            damageUpgradePrice[BallType.CASH] = new SaveLoadData<float>(CASHBALLDAMAGEUPGRADEPRICEKEY).LoadData();
            speedUpgradePrice[BallType.CASH] = new SaveLoadData<float>(CASHBALLSPEEDUPGRADEPRICEKEY).LoadData();
            newBallPrice[BallType.CASH] = new SaveLoadData<float>(CASHBALLNEWBALLPRICEKEY).LoadData();

            ballOpened[BallType.CASH] = true;

            int count = new SaveLoadData<int>(CASHBALLCOUNTKEY).LoadData();
            for (int i = 0; i < count; i++)
            {
                InstantiateBall(BallType.CASH);
            }
        }

        if (PlayerPrefs.HasKey(CRUSHBALLCOUNTKEY))
        {
            currentDamage[BallType.CRUSH] = new SaveLoadData<float>(CRUSHBALLCURRENTDAMAGEKEY).LoadData();
            currentSpeed[BallType.CRUSH] = new SaveLoadData<float>(CRUSHBALLCURRENTSPEEDKEY).LoadData();
            damageIncrement[BallType.CRUSH] = new SaveLoadData<float>(CRUSHBALLDAMAGEINCREMENTKEY).LoadData();

            damageUpgradePrice[BallType.CRUSH] = new SaveLoadData<float>(CRUSHBALLDAMAGEUPGRADEPRICEKEY).LoadData();
            speedUpgradePrice[BallType.CRUSH] = new SaveLoadData<float>(CRUSHBALLSPEEDUPGRADEPRICEKEY).LoadData();
            newBallPrice[BallType.CRUSH] = new SaveLoadData<float>(CRUSHBALLNEWBALLPRICEKEY).LoadData();

            ballOpened[BallType.CRUSH] = true;

            int count = new SaveLoadData<int>(CRUSHBALLCOUNTKEY).LoadData();
            for (int i = 0; i < count; i++)
            {
                InstantiateBall(BallType.CRUSH);
            }
        }


        if (PlayerPrefs.HasKey(FIREBALLCOUNTKEY))
        {
            currentDamage[BallType.FIRE] = new SaveLoadData<float>(FIREBALLCURRENTDAMAGEKEY).LoadData();
            currentSpeed[BallType.FIRE] = new SaveLoadData<float>(FIREBALLCURRENTSPEEDKEY).LoadData();
            damageIncrement[BallType.FIRE] = new SaveLoadData<float>(FIREBALLDAMAGEINCREMENTKEY).LoadData();
            damageUpgradePrice[BallType.FIRE] = new SaveLoadData<float>(FIREBALLDAMAGEUPGRADEPRICEKEY).LoadData();
            speedUpgradePrice[BallType.FIRE] = new SaveLoadData<float>(FIREBALLSPEEDUPGRADEPRICEKEY).LoadData();
            newBallPrice[BallType.FIRE] = new SaveLoadData<float>(FIREBALLNEWBALLPRICEKEY).LoadData();

            ballOpened[BallType.FIRE] = true;

            int count = new SaveLoadData<int>(FIREBALLCOUNTKEY).LoadData();
            for (int i = 0; i < count; i++)
            {
                InstantiateBall(BallType.FIRE);
            }
        }

    }

    public void ResetBalls()
    {
        ResetData();
        currentStage = 0;

        currentDamage = new Dictionary<BallType, float>();
        damageIncrement = new Dictionary<BallType, float>();

        currentSpeed = new Dictionary<BallType, float>();
        speedIncrement = new Dictionary<BallType, float>();

        ballOpened = new Dictionary<BallType, bool> {
            {BallType.BASIC, false}, {BallType.SPLASH, false}, {BallType.SNIPER, false},
            {BallType.POISON, false}, {BallType.DEMO, false}, {BallType.CRUSH, false},
            {BallType.CASH, false}, {BallType.FIRE, false}
        };

        newBallPrice = new Dictionary<BallType, float>();
        speedUpgradePrice = new Dictionary<BallType, float>();
        damageUpgradePrice = new Dictionary<BallType, float>();


        basicBalls = new List<BasicBall>();
        poisonBalls = new List<PoisonBall>();
        crushBalls = new List<CrushBall>();
        demoBalls = new List<DemoBall>();
        sniperBalls = new List<SniperBall>();
        splashBalls = new List<SplashBall>();
        cashBalls = new List<CashBall>();
        fireBalls = new List<FireBall>();

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Ball").ToList())
        {
            Destroy(item);
        };

        OpenNewBall(BallType.BASIC);
    }

    private void ResetData()
    {
        if (PlayerPrefs.HasKey(MAXBALLSKEY))
        {
            new SaveLoadData<int>(MAXBALLSKEY).RemoveData();
            
        }

        if (PlayerPrefs.HasKey(CURRENTSTAGEKEY))
        {
            new SaveLoadData<int>(CURRENTSTAGEKEY).RemoveData();
        }

        if (PlayerPrefs.HasKey(BASICBALLCOUNTKEY))
        {
            new SaveLoadData<float>(BASICBALLCURRENTDAMAGEKEY).RemoveData();
            new SaveLoadData<float>(BASICBALLCURRENTSPEEDKEY).RemoveData();
            new SaveLoadData<float>(BASICBALLDAMAGEINCREMENTKEY).RemoveData();

            new SaveLoadData<float>(BASICBALLDAMAGEUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(BASICBALLSPEEDUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(BASICBALLNEWBALLPRICEKEY).RemoveData();

            new SaveLoadData<int>(BASICBALLCOUNTKEY).RemoveData();
        }

        if (PlayerPrefs.HasKey(SPLASHBALLCOUNTKEY))
        {
            new SaveLoadData<float>(SPLASHBALLCURRENTDAMAGEKEY).RemoveData();
            new SaveLoadData<float>(SPLASHBALLCURRENTSPEEDKEY).RemoveData();
            new SaveLoadData<float>(SPLASHBALLDAMAGEINCREMENTKEY).RemoveData();

            new SaveLoadData<float>(SPLASHBALLDAMAGEUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(SPLASHBALLSPEEDUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(SPLASHBALLNEWBALLPRICEKEY).RemoveData();

            new SaveLoadData<int>(SPLASHBALLCOUNTKEY).RemoveData();
        }

        if (PlayerPrefs.HasKey(SNIPERBALLCOUNTKEY))
        {
            new SaveLoadData<float>(SNIPERBALLCURRENTDAMAGEKEY).RemoveData();
            new SaveLoadData<float>(SNIPERBALLCURRENTSPEEDKEY).RemoveData();
            new SaveLoadData<float>(SNIPERBALLDAMAGEINCREMENTKEY).RemoveData();

            new SaveLoadData<float>(SNIPERBALLDAMAGEUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(SNIPERBALLSPEEDUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(SNIPERBALLNEWBALLPRICEKEY).RemoveData();

            new SaveLoadData<int>(SNIPERBALLCOUNTKEY).RemoveData();
        }

        if (PlayerPrefs.HasKey(POISONBALLCOUNTKEY))
        {
            new SaveLoadData<float>(POISONBALLCURRENTDAMAGEKEY).RemoveData();
            new SaveLoadData<float>(POISONBALLCURRENTSPEEDKEY).RemoveData();
            new SaveLoadData<float>(POISONBALLDAMAGEINCREMENTKEY).RemoveData();

            new SaveLoadData<float>(POISONBALLDAMAGEUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(POISONBALLSPEEDUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(POISONBALLNEWBALLPRICEKEY).RemoveData();

            new SaveLoadData<int>(POISONBALLCOUNTKEY).RemoveData();
        }

        if (PlayerPrefs.HasKey(DEMOBALLCOUNTKEY))
        {
            new SaveLoadData<float>(DEMOBALLCURRENTDAMAGEKEY).RemoveData();
            new SaveLoadData<float>(DEMOBALLCURRENTSPEEDKEY).RemoveData();
            new SaveLoadData<float>(DEMOBALLDAMAGEINCREMENTKEY).RemoveData();

            new SaveLoadData<float>(DEMOBALLDAMAGEUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(DEMOBALLSPEEDUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(DEMOBALLNEWBALLPRICEKEY).RemoveData();

            new SaveLoadData<int>(DEMOBALLCOUNTKEY).RemoveData();
        }

        if (PlayerPrefs.HasKey(CASHBALLCOUNTKEY))
        {
            new SaveLoadData<float>(CASHBALLCURRENTDAMAGEKEY).RemoveData();
            new SaveLoadData<float>(CASHBALLCURRENTSPEEDKEY).RemoveData();
            new SaveLoadData<float>(CASHBALLDAMAGEINCREMENTKEY).RemoveData();

            new SaveLoadData<float>(CASHBALLDAMAGEUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(CASHBALLSPEEDUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(CASHBALLNEWBALLPRICEKEY).RemoveData();

            new SaveLoadData<int>(CASHBALLCOUNTKEY).RemoveData();
        }

        if (PlayerPrefs.HasKey(CRUSHBALLCOUNTKEY))
        {
            new SaveLoadData<float>(CRUSHBALLCURRENTDAMAGEKEY).RemoveData();
            new SaveLoadData<float>(CRUSHBALLCURRENTSPEEDKEY).RemoveData();
            new SaveLoadData<float>(CRUSHBALLDAMAGEINCREMENTKEY).RemoveData();

            new SaveLoadData<float>(CRUSHBALLDAMAGEUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(CRUSHBALLSPEEDUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(CRUSHBALLNEWBALLPRICEKEY).RemoveData();

            new SaveLoadData<int>(CRUSHBALLCOUNTKEY).RemoveData();
        }

        if (PlayerPrefs.HasKey(FIREBALLCOUNTKEY))
        {
            new SaveLoadData<float>(FIREBALLCURRENTDAMAGEKEY).RemoveData();
            new SaveLoadData<float>(FIREBALLCURRENTSPEEDKEY).RemoveData();
            new SaveLoadData<float>(FIREBALLDAMAGEINCREMENTKEY).RemoveData();
            new SaveLoadData<float>(FIREBALLDAMAGEUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(FIREBALLSPEEDUPGRADEPRICEKEY).RemoveData();
            new SaveLoadData<float>(FIREBALLNEWBALLPRICEKEY).RemoveData();
        }
    }
}
