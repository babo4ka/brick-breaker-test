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

        bonusesCountExpander.GetComponent<Button>().onClick.AddListener(ExpandBonusesCount);
        newBonusBuyer.GetComponent<Button>().onClick.AddListener(BuyNewBonus);
    }

    private void ExpandBonusesCount()
    {
        bonusManager.ExpandMaxBonuses();
    }

    private void BuyNewBonus()
    {
        bonusManager.OpenNewBonus();
    }
}
