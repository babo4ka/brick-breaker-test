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
    private BonusType bonusType;

    void Start()
    {
        bonusManager = gameManager.GetComponent<BonusManager>();

        GetComponent<Button>().onClick.AddListener(ActivateBonus);
    }

    private void ActivateBonus()
    {
        if(isActive)
        {
            bonusManager.DeactivateBonus(bonusType);
            isActive = false;
        }
        else
        {
            bonusManager.ActivateBonus(bonusType);
            isActive = true;
        }
        
    }
}
