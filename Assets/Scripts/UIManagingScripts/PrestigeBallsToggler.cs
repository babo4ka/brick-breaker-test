using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeBallsToggler : MonoBehaviour
{
    [SerializeField]
    private GameObject prestigeBallsManagingPanel;
    [SerializeField]
    private GameObject panelToOpen;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TogglePanel);
    }

    private void TogglePanel()
    {
        if(panelToOpen != null)
        {
            prestigeBallsManagingPanel.SetActive(true);
            panelToOpen.SetActive(true);
        }
        else
        {
            prestigeBallsManagingPanel.SetActive(false);
        }
    }

}
