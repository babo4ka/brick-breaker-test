using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsController : MonoBehaviour
{
    [SerializeField]
    private GameObject ballsManagingPanel;


    [SerializeField]
    private Button ballsManagingButton;


    private void Start()
    {
        ballsManagingButton.onClick.AddListener(BallManagingPanelToggle);
    }

    private void BallManagingPanelToggle()
    {
        if(ballsManagingPanel.activeSelf)
        {
            ballsManagingPanel.SetActive(false);
        }
        else
        {
            ballsManagingPanel.SetActive(true);
        }
    }
}
