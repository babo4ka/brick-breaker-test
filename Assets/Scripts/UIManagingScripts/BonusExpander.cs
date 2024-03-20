using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusExpander : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    private BonusManager bonusManager;

    [SerializeField]
    private List<GameObject> cardsTogglersList;

    private Dictionary<CardType, int> numberByType = new Dictionary<CardType, int>
    {
        {CardType.BALLDAMAGE, 0}, {CardType.BALLSPEED, 1}, {CardType.CRITDAMAGE, 2},
        {CardType.CASHBRICKCHANCE, 3}, {CardType.CASHBRICK, 4}, {CardType.CASH, 5},
        {CardType.RADIUS, 6}, {CardType.STAGECASH, 7}
    };

    [SerializeField]
    private GameObject bonusesCountExpander;
    [SerializeField]
    private GameObject newBonusBuyer;

    [SerializeField]
    private TMP_Text activeCardsText;
    private int maxCards;
    private int activeCards;

    void Start()
    {
        bonusManager = gameManager.GetComponent<BonusManager>();
        activeCards = bonusManager.ActiveCardsCount();
        maxCards = bonusManager.MaxCardsCount();
        
        SetCardsCountText();

        bonusesCountExpander.GetComponent<Button>().onClick.AddListener(ExpandCardsCount);
        newBonusBuyer.GetComponent<Button>().onClick.AddListener(BuyNewBonus);
    }

    private void SetCardsCountText()
    {
        activeCardsText.text = activeCards.ToString() + "/" + maxCards.ToString();
    }

    private void ExpandCardsCount()
    {
        maxCards = bonusManager.ExpandMaxCards();
        SetCardsCountText();
    }

    private void BuyNewBonus()
    {
        CardType type = bonusManager.OpenNewCard();
        cardsTogglersList[numberByType[type]].SetActive(true);

        activeCards = bonusManager.ActiveCardsCount();
    }
}
