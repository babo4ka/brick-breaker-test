using System.Collections;
using System.Collections.Generic;
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
                bm.BuyNewBall(ballType);
                break;
        }
    }
}
