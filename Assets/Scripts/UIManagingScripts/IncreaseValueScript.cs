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
                int count = bm.BuyNewBall(ballType);
                valueText.text = count.ToString();
                break;

            case ValueType.SPEED:
                float speed = bm.UpgradeSpeed(ballType);
                valueText.text = speed.ToString();
                break;
        }
    }
}
