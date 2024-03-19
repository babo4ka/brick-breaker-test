using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsController : MonoBehaviour
{
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
        }
        else
        {
            cardsManagingPanel.SetActive(true);
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
        }
        else
        {
            ballsManagingPanel.SetActive(true);
        }
    }
}
