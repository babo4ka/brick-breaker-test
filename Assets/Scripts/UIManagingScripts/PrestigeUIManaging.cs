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
        openBuyPrestigePanel.onClick.AddListener(ToggleBuyPrestigePanel);
        closeBuyPrestigePanel.onClick.AddListener(ToggleBuyPrestigePanel);
        UpdatePrestigeInfo();
    }

    private void UpdatePrestigeInfo()
    {
        prestigeCashText.text = $"{prestigeManager.prestigeCash}";
        multiplierText.text = $"{prestigeManager.prestigeMultiplier}x";
    } 

    private void ToggleBuyPrestigePanel()
    {
        buyPrestigePanel.GetComponent<BuyPrestigePanel>().Toggle();
        prestigeManager = gameManager.GetComponent<PrestigeManager>();
    }
}
