using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusExpander : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    private BonusManager bonusManager;

    [SerializeField]
    private GameObject bonusesCountExpander;
    [SerializeField]
    private GameObject newBonusBuyer;

    void Start()
    {
        bonusManager = gameManager.GetComponent<BonusManager>();

        bonusesCountExpander.GetComponent<Button>().onClick.AddListener(ExpandCardsCount);
        newBonusBuyer.GetComponent<Button>().onClick.AddListener(BuyNewBonus);
    }

    private void ExpandCardsCount()
    {
        bonusManager.ExpandMaxCards();
    }

    private void BuyNewBonus()
    {
        bonusManager.OpenNewCard();
    }
}
