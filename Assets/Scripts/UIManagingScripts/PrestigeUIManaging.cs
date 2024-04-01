using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeUIManaging : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;

    private PrestigeManager prestigeManager;

    [SerializeField]
    private GameObject buyPrestigePanel;

    [SerializeField]
    private Button openBuyPrestigePanel;
    [SerializeField]
    private Button closeBuyPrestigePanel;

    [SerializeField]
    private TMP_Text prestigeCashText;
    [SerializeField]
    private TMP_Text multiplierText;

    private void Start()
    {
        prestigeManager = gameManager.GetComponent<PrestigeManager>();
        openBuyPrestigePanel.onClick.AddListener(ToggleBuyPrestigePanel);
        closeBuyPrestigePanel.onClick.AddListener(ToggleBuyPrestigePanel);
        UpdatePrestigeInfo();

        prestigeManager.prestiged += UpdatePrestigeInfo;
    }


    private void UpdatePrestigeInfo()
    {
        prestigeCashText.text = $"{prestigeManager.prestigeCash}";
        multiplierText.text = $"Current multiplier: {System.Math.Round(prestigeManager.prestigeMultiplier, 2)}x";
    } 

    private void ToggleBuyPrestigePanel()
    {
        buyPrestigePanel.GetComponent<BuyPrestigePanel>().Toggle();
        prestigeManager = gameManager.GetComponent<PrestigeManager>();
    }
}
