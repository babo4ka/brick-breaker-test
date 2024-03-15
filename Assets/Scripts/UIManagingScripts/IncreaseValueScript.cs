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
    [SerializeField]
    private TMP_Text valueText;
    [SerializeField]
    private TMP_Text priceText;


    void Start()
    {
        GetComponent<Button>().onClick.AddListener(IncreaseValue);
    }

    private void IncreaseValue()
    {
        BallManager bm = gameManager.GetComponent<BallManager>();
        switch(valueType)
        {
            case ValueType.COUNT:
                Dictionary<string, float> count = bm.BuyNewBall(ballType);
                valueText.text = ((int)count["value"]).ToString();
                priceText.text = System.Math.Round(count["price"], 2).ToString() + "$";
                break;

            case ValueType.SPEED:
                Dictionary<string, float> speed = bm.UpgradeSpeed(ballType);
                valueText.text = System.Math.Round(speed["value"], 2).ToString();
                priceText.text = System.Math.Round(speed["price"], 2).ToString() + "$";
                break;

            case ValueType.DAMAGE:
                Dictionary<string, float> damage = bm.UpgradeDamage(ballType);
                valueText.text = System.Math.Round(damage["value"], 2).ToString();
                priceText.text = System.Math.Round(damage["price"], 2).ToString() + "$";
                break;
        }
    }
}
