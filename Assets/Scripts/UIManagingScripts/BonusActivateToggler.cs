using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusActivateToggler : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    private BonusManager bonusManager;

    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private TMP_Text countText;

    private bool isActive = false;

    [SerializeField]
    private CardType bonusType;

    private Card currentCard;

    void Start()
    {
        bonusManager = gameManager.GetComponent<BonusManager>();

        GetComponent<Button>().onClick.AddListener(ActivateCard);

        bonusManager.updateCardInfo += UpdateCardInfo;
    }

    private void UpdateCardInfo(CardType type, Card card)
    {
        if(type == bonusType)
        {
            currentCard = card;
            UpdateInfo();
        }
    }

    private void UpdateInfo()
    {
        levelText.text = currentCard.level.ToString();
        countText.text = $"{currentCard.count}/{currentCard.NextLevelPrice()}";
    }

    private void ActivateCard()
    {
        if(isActive)
        {
            bonusManager.DeactivateCard(bonusType);
            isActive = false;
        }
        else
        {
            if(bonusManager.ActivateCard(bonusType)) isActive = true;
        }
        
    }
}
