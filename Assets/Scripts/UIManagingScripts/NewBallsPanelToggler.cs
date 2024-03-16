using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBallsPanelToggler : MonoBehaviour
{
    [SerializeField]
    private GameObject newBallsPanel;
    [SerializeField]
    private GameObject gameManager;
    private BallManager ballManager;
    [SerializeField]
    private TMP_Text newBallPriceText;

    #region Balls panels
    [SerializeField]
    private GameObject sniperPanel;
    [SerializeField]
    private GameObject splashPanel;
    [SerializeField]
    private GameObject poisonPanel;
    [SerializeField]
    private GameObject demoPanel;
    [SerializeField]
    private GameObject crushPanel;
    [SerializeField]
    private GameObject cashPanel;
    [SerializeField]
    private GameObject firePanel;

    private Dictionary<BallType, GameObject> ballPanels = new Dictionary<BallType, GameObject>();

    List<BallType> allowedTypes = new List<BallType>();
    #endregion

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TogglePanel);
        ballManager = gameManager.GetComponent<BallManager>();
        ballPanels.Add(BallType.SNIPER, sniperPanel);
        ballPanels.Add(BallType.SPLASH, splashPanel);
        ballPanels.Add(BallType.POISON, poisonPanel);
        ballPanels.Add(BallType.DEMO, demoPanel);
        ballPanels.Add(BallType.CRUSH, crushPanel);
        ballPanels.Add(BallType.CASH, cashPanel);
        ballPanels.Add(BallType.FIRE, firePanel);
    }

    private void TogglePanel()
    {
        if(newBallsPanel.activeInHierarchy) {
            newBallsPanel.SetActive(false);
        }
        else
        {
            newBallsPanel.SetActive(true);
            float nextStageprice = ballManager.NextStagePrice();
            newBallPriceText.text = nextStageprice.ToString() + "$";

            allowedTypes.Clear();
            allowedTypes = ballManager.AllowedTypes();

            foreach (BallType type in allowedTypes)
            {
                ballPanels[type].SetActive(true);
            }
        }
    }


}
