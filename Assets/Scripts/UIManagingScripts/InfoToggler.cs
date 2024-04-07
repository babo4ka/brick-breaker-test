using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoToggler : MonoBehaviour
{
    [SerializeField]
    private PrestigeBonusType type;
    [SerializeField]
    private BallType ballType;

    [SerializeField]
    private GameManager gameManager;
    private PrestigeManager prestigeManager;

    [SerializeField]
    private GameObject infoPanel;
    [SerializeField]
    private GameObject increaseValueButton;
    [SerializeField]
    private TMP_Text descriptionText;
    [SerializeField]
    private TMP_Text nextValueText;



    private void Start()
    {
        prestigeManager = gameManager.GetComponent<PrestigeManager>();
        GetComponent<Button>().onClick.AddListener(ToggleInfoPanel);
    }

    private void ToggleInfoPanel()
    {
        infoPanel.SetActive(true);
        descriptionText.text = PrestigeBonusDescription.GetDescription(type);
        nextValueText.text = $"Next value: {prestigeManager.GetNextPrestigeBonusValue(ballType, type)}";
        increaseValueButton.GetComponent<IncreasePrestigeValue>().SetProps(ballType, type);
    }
}
