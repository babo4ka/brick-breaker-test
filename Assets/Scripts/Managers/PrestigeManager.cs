using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeManager : MonoBehaviour
{
    public delegate void Prestiged();
    public Prestiged prestiged;

    public delegate void PrestigeUpdate(BallType ballType,
        PrestigeBonusType prestigeBonusType,
        BonusStats<float> stats);
    public PrestigeUpdate prestigeUpdate;

    #region Prestige data
    private int totalPrestiged;
    private int _prestigeCash = 0;
    private int nextLevelPrice = 150;

    private float _prestigeMultiplier = 1f;

    public float prestigeCash
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

    private const float _prestigePriceInCash = 100000000f;
    private const float _everyPoint = 1000000f;

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
        //проверка цены еще будет
        ballPrestiges[ballType].AddLevelToBonus(prestigeBonusType);
    }

    public BonusStats<float> GetPrestigeBonusValue(BallType ballType, 
        PrestigeBonusType prestigeBonusType)
    {
        return ballPrestiges[ballType].GetPrestigeBonusStats(prestigeBonusType);
    }

    public float GetNextPrestigeBonusValue(BallType ballType, PrestigeBonusType prestigeBonusType) {
        return ballPrestiges[ballType].GetNextPrestigeBonusValue(prestigeBonusType);
    }

    public void AddPrestigeCash(int amount)
    {
        _prestigeCash += amount;
        totalPrestiged += amount;

        while(totalPrestiged >= nextLevelPrice)
        {
            nextLevelPrice = (int)Math.Truncate(nextLevelPrice * 1.5f);
            _prestigeMultiplier *= 1.2f;
        }
    }

    public void PrestigeGame()
    {
        if(cashManager.totalSoftCashEarned >= _prestigePriceInCash)
        {
            float totalCash = cashManager.totalSoftCashEarned - _prestigePriceInCash;
            gameManager.ResetGame();

            int pointsEarned = (int)Math.Truncate(totalCash / _everyPoint);

            AddPrestigeCash(pointsEarned * 5 + 50);

            prestiged?.Invoke();
        }
    }

    void Start()
    {
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
}
