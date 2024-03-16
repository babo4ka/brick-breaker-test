using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenBallScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private GameObject ballManagingPanel;
    [SerializeField]
    private GameObject newBallsPanel;

    [SerializeField]
    private BallType ballType;

    [SerializeField]
    private TMP_Text countText;
    [SerializeField]
    private TMP_Text speedText;
    [SerializeField]
    private TMP_Text damageText;
    [SerializeField]
    private TMP_Text countPriceText;
    [SerializeField]
    private TMP_Text speedPriceText;
    [SerializeField]
    private TMP_Text damagePriceText;

    void Awake()
    {
         GetComponent<Button>().onClick.AddListener(OpenNewBall);
    }

    private void OpenNewBall()
    {
        Dictionary<string, float> opened = gameManager.GetComponent<BallManager>().OpenNewBall(ballType);

        if(opened != null)
        {
            ballManagingPanel.SetActive(true);
            countText.text = ((int)opened["count"]).ToString();
            speedText.text = opened["speed"].ToString();
            damageText.text = opened["damage"].ToString();

            countPriceText.text = System.Math.Round(opened["countPrice"], 2).ToString() + "$";
            speedPriceText.text = System.Math.Round(opened["speedPrice"], 2).ToString() + "$";
            damagePriceText.text = System.Math.Round(opened["damagePrice"], 2).ToString() + "$";

            newBallsPanel.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
