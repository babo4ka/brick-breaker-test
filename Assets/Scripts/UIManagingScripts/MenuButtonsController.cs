using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsController : MonoBehaviour
{
    private GameObject currentActivePanel;

    [SerializeField]
    private GameObject ballsManagingPanel;
    [SerializeField]
    private GameObject newBallsPanel;

    [SerializeField]
    private GameObject cardsManagingPanel;


    [SerializeField]
    private Button ballsManagingButton;
    [SerializeField]
    private Button cardsManagingButton;

    private void Start()
    {
        ballsManagingButton.onClick.AddListener(BallManagingPanelToggle);
        cardsManagingButton.onClick.AddListener(CardManagingPanelToggle);
    }

    private void CardManagingPanelToggle()
    {
        if(cardsManagingPanel.activeSelf)
        {
            cardsManagingPanel.SetActive(false);
            currentActivePanel = null;
        }
        else
        {
            if(currentActivePanel != null)
            {
                currentActivePanel.SetActive(false);
            }
            cardsManagingPanel.SetActive(true);
            currentActivePanel = cardsManagingPanel;
        }
    }

    private void BallManagingPanelToggle()
    {
        if(ballsManagingPanel.activeSelf)
        {
            if (newBallsPanel.activeSelf)
            {
                newBallsPanel.SetActive(false);
            }
            ballsManagingPanel.SetActive(false);
            currentActivePanel = null;
        }
        else
        {
            if(currentActivePanel != null)
            {
                currentActivePanel.SetActive(false);
            }
            ballsManagingPanel.SetActive(true);
            currentActivePanel = ballsManagingPanel;
        }
    }
}
