using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeManager : MonoBehaviour
{
    #region Prestige data
    private int totalPrestiged;
    private int prestigeCash;
    private int nextLevelPrice = 150;

    private float prestigeMultiplier = 1f;
    #endregion

    #region Managers
    private GameManager gameManager;
    private CashManager cashManager;
    #endregion

    private const float prestigePriceInCash = 100000000f;
    private const float everyPoint = 1000000f;

    private Dictionary<BallType, PrestigeBonusBall> ballPrestiges = new Dictionary<BallType, PrestigeBonusBall>
    {
        {BallType.SPLASH, new SplashPrestigeBonus()}
    };

    public void AddLevelToPrestigeBonus(BallType ballType, PrestigeBonusType prestigeBonusType)
    {
        //проверка цены еще будет
        ballPrestiges[ballType].AddLevelToBonus(prestigeBonusType);
    }


    public void AddPrestigeCash(int amount)
    {
        prestigeCash += amount;
        totalPrestiged += amount;

        while(totalPrestiged >= nextLevelPrice)
        {
            nextLevelPrice = (int)Math.Truncate(nextLevelPrice * 1.5f);
            prestigeMultiplier *= 1.2f;
        }
    }

    public void PrestigeGame()
    {
        if(cashManager.totalSoftCashEarned >= prestigePriceInCash)
        {
            float totalCash = cashManager.totalSoftCashEarned - prestigePriceInCash;
            gameManager.ResetGame();

            int pointsEarned = (int)Math.Truncate(totalCash / everyPoint);

            AddPrestigeCash(pointsEarned * 5 + 50);
        }
    }

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        cashManager = GetComponent<CashManager>();
    }
}
