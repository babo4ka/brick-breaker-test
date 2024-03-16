using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class IncreaseValueScript : MonoBehaviour
{
    [SerializeField]
    private BallType ballType;

    [SerializeField]
    private ValueType valueType;

    [SerializeField]
    private GameObject gameManager;
    private BallManager ballManager;
    [SerializeField]
    private TMP_Text valueText;
    [SerializeField]
    private TMP_Text priceText;


    void Start()
    {
        GetComponent<Button>().onClick.AddListener(IncreaseValue);
        ballManager = gameManager.GetComponent<BallManager>();


        switch (valueType)
        {
            case ValueType.COUNT:
                priceText.text = System.Math.Round(ballManager.GetNewBallPrice(ballType), 2).ToString() + "$";
                valueText.text = ballManager.BallsCount(ballType).ToString();
                break;

            case ValueType.SPEED:
                priceText.text = System.Math.Round(ballManager.GetSpeedUpgradePrice(ballType), 2).ToString() + "$";
                valueText.text = System.Math.Round(ballManager.GetCurrentSpeed(ballType), 2).ToString();
                break;

            case ValueType.DAMAGE:
                priceText.text = System.Math.Round(ballManager.GetDamageUpgradePrice(ballType), 2).ToString() + "$";
                valueText.text = System.Math.Round(ballManager.GetCurrentDamage(ballType), 2).ToString();
                break;
        }
    }

    private void IncreaseValue()
    {
        switch(valueType)
        {
            case ValueType.COUNT:
                Dictionary<string, float> count = ballManager.BuyNewBall(ballType);
                valueText.text = ((int)count["value"]).ToString();
                priceText.text = System.Math.Round(count["price"], 2).ToString() + "$";
                break;

            case ValueType.SPEED:
                Dictionary<string, float> speed = ballManager.UpgradeSpeed(ballType);
                valueText.text = System.Math.Round(speed["value"], 2).ToString();
                priceText.text = System.Math.Round(speed["price"], 2).ToString() + "$";
                break;

            case ValueType.DAMAGE:
                Dictionary<string, float> damage = ballManager.UpgradeDamage(ballType);
                valueText.text = System.Math.Round(damage["value"], 2).ToString();
                priceText.text = System.Math.Round(damage["price"], 2).ToString() + "$";
                break;
        }
    }
}
