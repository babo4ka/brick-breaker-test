using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusActivateToggler : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    private BonusManager bonusManager;

    private bool isActive = false;

    [SerializeField]
    private CardType bonusType;

    void Start()
    {
        bonusManager = gameManager.GetComponent<BonusManager>();

        GetComponent<Button>().onClick.AddListener(ActivateCard);
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
            bonusManager.ActivateCard(bonusType);
            isActive = true;
        }
        
    }
}
