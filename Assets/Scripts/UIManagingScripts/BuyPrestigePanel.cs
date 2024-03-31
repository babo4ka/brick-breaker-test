using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyPrestigePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    private CashManager cashManager;
    private PrestigeManager prestigeManager;

    [SerializeField]
    private TMP_Text totalEarnedText;
    [SerializeField]
    private TMP_Text rewardText;

    [SerializeField]
    private Button prestigeButton;


    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    void Start()
    {
        cashManager = gameManager.GetComponent<CashManager>();
        prestigeManager = gameManager.GetComponent <PrestigeManager>();

        prestigeButton.onClick.AddListener(Prestige);

        UpdateTotalEarned();
    }

    private void UpdateTotalEarned()
    {
        totalEarnedText.text = $"Total earned: {cashManager.totalSoftCashEarned}$";
        rewardText.text = $"Reward: {prestigeManager.prestigeBonus}";
    }

    private void Prestige()
    {
        prestigeManager.PrestigeGame();
    }

}
