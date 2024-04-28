using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeManager : MonoBehaviour
{

    private const string TOTALPRESTIGEDKEY = "totalPrestiged";
    private const string PRESTIGECASHKEY = "prestigeCashKey";
    private const string NEXTLEVELPRICEKEY = "nextLevelPrice";
    private const string PRESTIGEMULKEY = "prestigeMulKey";


    #region Delegates
    public delegate void Prestiged();
    public Prestiged prestiged;

    public delegate void PrestigeUpdate(BallType ballType,
        PrestigeBonusType prestigeBonusType,
        BonusStats<float> stats);
    public PrestigeUpdate prestigeUpdate;

    public delegate void BallsCountPrestiged(int count);
    public BallsCountPrestiged ballsCountPrestiged;
    #endregion

    #region Prestige data
    private int totalPrestiged;
    private int _prestigeCash = 0;
    private int nextLevelPrice = 150;

    private float _prestigeMultiplier = 1f;

    public int prestigeCash
    {
        get { return _prestigeCash; }
    }

    public float prestigeMultiplier
    {
        get { return _prestigeMultiplier; }
    }
    #endregion

    #region Managers
    private GameManager gameManager;
    private CashManager cashManager;
    #endregion

    #region Prestige points
    private const float _prestigePriceInCash = 100000000f;
    private const float _everyPoint = 1000000f;
    #endregion


    #region Getting values
    public float prestigeBonus
    {
        get { return (int)Math.Truncate((cashManager.totalSoftCashEarned - _prestigePriceInCash) / _everyPoint) * 5 + 50; }
    }

    private Dictionary<BallType, PrestigeBonusBall> ballPrestiges = new Dictionary<BallType, PrestigeBonusBall>
    {
        {BallType.SPLASH, new SplashPrestigeBonus()}, { BallType.SNIPER, new SniperPrestigeBonus()},
        {BallType.POISON, new PoisonPrestigeBonus()}, {BallType.CRUSH, new CrushPrestigeBonus()}, 
        {BallType.DEMO, new DemoPrestigeBonus()}, {BallType.CASH, new CashPrestigeBonus()},
        {BallType.FIRE, new FirePrestigeBonus()}
    };


    private void PrestigeBonusUpdate(BallType ballType,
        PrestigeBonusType prestigeBonusType,
        BonusStats<float> stats)
    {
        prestigeUpdate?.Invoke(ballType, prestigeBonusType, stats);
    }

    public void AddLevelToPrestigeBonus(BallType ballType, PrestigeBonusType prestigeBonusType)
    {
        if(_prestigeCash >= ballPrestiges[ballType].GetPrestigeBonusPrice(prestigeBonusType)){
            _prestigeCash -= ballPrestiges[ballType].GetPrestigeBonusPrice(prestigeBonusType);
            ballPrestiges[ballType].AddLevelToBonus(prestigeBonusType);
            SaveLoadData<int> sldCash = new SaveLoadData<int>(PRESTIGECASHKEY, _prestigeCash);
            sldCash.SaveData();
        }
        
    }

    public BonusStats<float> GetPrestigeBonusValue(BallType ballType, 
        PrestigeBonusType prestigeBonusType)
    {
        return ballPrestiges[ballType].GetPrestigeBonusStats(prestigeBonusType);
    }

    public float GetNextPrestigeBonusValue(BallType ballType, PrestigeBonusType prestigeBonusType) {
        return ballPrestiges[ballType].GetNextPrestigeBonusValue(prestigeBonusType);
    }
    #endregion


    #region Utils
    public void AddPrestigeCash(int amount)
    {
        _prestigeCash += amount;
        totalPrestiged += amount;

        SaveLoadData<int> sldCash = new SaveLoadData<int>(PRESTIGECASHKEY, _prestigeCash);
        SaveLoadData<int> sldTotal = new SaveLoadData<int>(TOTALPRESTIGEDKEY, totalPrestiged);
        sldCash.SaveData();
        sldTotal.SaveData();

        while(totalPrestiged >= nextLevelPrice)
        {
            nextLevelPrice = (int)Math.Truncate(nextLevelPrice * 1.5f);
            _prestigeMultiplier *= 1.2f;
            ballsCountPrestiged?.Invoke(5);

            SaveLoadData<int> sldPrice = new SaveLoadData<int>(NEXTLEVELPRICEKEY, nextLevelPrice);
            SaveLoadData<float> sldMul = new SaveLoadData<float>(PRESTIGEMULKEY, _prestigeMultiplier);
            sldPrice.SaveData();
            sldMul.SaveData();
        }
    }

    public void PrestigeGame()
    {
        if(cashManager.totalSoftCashEarned >= _prestigePriceInCash)
        {

            float totalCash = cashManager.totalSoftCashEarned - _prestigePriceInCash;
            
            int pointsEarned = (int)Math.Truncate(totalCash / _everyPoint);

            AddPrestigeCash(pointsEarned * 5 + 50);
            
            gameManager.ResetGame();    

            prestiged?.Invoke();
        }
    }
    #endregion

    void Start()
    {
        LoadData();
        gameManager = GetComponent<GameManager>();
        cashManager = GetComponent<CashManager>();

        ballPrestiges[BallType.SPLASH].updatePrestigeBonus += PrestigeBonusUpdate;
        ballPrestiges[BallType.SNIPER].updatePrestigeBonus += PrestigeBonusUpdate;
        ballPrestiges[BallType.POISON].updatePrestigeBonus += PrestigeBonusUpdate;
        ballPrestiges[BallType.CRUSH].updatePrestigeBonus += PrestigeBonusUpdate;
        ballPrestiges[BallType.DEMO].updatePrestigeBonus += PrestigeBonusUpdate;
        ballPrestiges[BallType.CASH].updatePrestigeBonus += PrestigeBonusUpdate;
        ballPrestiges[BallType.FIRE].updatePrestigeBonus += PrestigeBonusUpdate;
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(TOTALPRESTIGEDKEY))
        {
            SaveLoadData<int> sld = new SaveLoadData<int>(TOTALPRESTIGEDKEY);
            totalPrestiged = sld.LoadData();
        }

        if (PlayerPrefs.HasKey(PRESTIGECASHKEY))
        {
            SaveLoadData<int> sld = new SaveLoadData<int>(PRESTIGECASHKEY);
            _prestigeCash = sld.LoadData();
        }

        if (PlayerPrefs.HasKey(NEXTLEVELPRICEKEY))
        {
            SaveLoadData<int> sld = new SaveLoadData<int>(NEXTLEVELPRICEKEY);
            nextLevelPrice = sld.LoadData();
        }

        if (PlayerPrefs.HasKey(PRESTIGEMULKEY))
        {
            SaveLoadData<float> sld = new SaveLoadData<float>(PRESTIGEMULKEY);
            _prestigeMultiplier = sld.LoadData();
        }

    }
}
