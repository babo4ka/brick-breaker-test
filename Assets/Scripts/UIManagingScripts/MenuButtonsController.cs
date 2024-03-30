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
    private GameObject cardsManagingPanel;

    [SerializeField]
    private GameObject prestigeManagingPanel;

    [SerializeField]
    private Button ballsManagingButton;
    [SerializeField]
    private Button cardsManagingButton;
    [SerializeField]
    private Button prestigeManagingButton;

    private void Start()
    {
        ballsManagingButton.onClick.AddListener(BallManagingPanelToggle);
        cardsManagingButton.onClick.AddListener(CardManagingPanelToggle);
        //prestigeManagingButton.onClick.AddListener(PrestigeManagingPanelToggle);
    }

    private void CardManagingPanelToggle()
    {
        if(currentActivePanel == cardsManagingPanel)
        {
            cardsManagingPanel.GetComponent<UIPanel>().Close();
            currentActivePanel = null;
        }
        else
        {
            currentActivePanel?.GetComponent<UIPanel>().Close();
            cardsManagingPanel.GetComponent<UIPanel>().Open();
            currentActivePanel = cardsManagingPanel;
        }
    }

    private void BallManagingPanelToggle()
    {
        if(currentActivePanel == ballsManagingPanel)
        {
            ballsManagingPanel.GetComponent<UIPanel>().Close();
            currentActivePanel=null;
        }
        else
        {
            currentActivePanel?.GetComponent<UIPanel>().Close();
            ballsManagingPanel.GetComponent<UIPanel>().Open();
            currentActivePanel = ballsManagingPanel;
        }
    }

    private void PrestigeManagingPanelToggle()
    {
        if(currentActivePanel == prestigeManagingPanel)
        {
            prestigeManagingPanel.GetComponent<UIPanel>().Close();
            currentActivePanel = null;
        }
        else
        {
            currentActivePanel.GetComponent<UIPanel>().Close();
            prestigeManagingPanel.GetComponent<UIPanel>().Open();
            currentActivePanel = prestigeManagingPanel;
        }
    }
}
