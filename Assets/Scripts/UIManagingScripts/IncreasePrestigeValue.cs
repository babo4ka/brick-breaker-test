using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreasePrestigeValue : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    private PrestigeManager prestigeManager;

    [SerializeField]
    private BallType ballType;
    [SerializeField]
    private PrestigeBonusType prestigeBonusType;

    public void SetProps(BallType ballType, PrestigeBonusType prestigeBonusType)
    {
        this.ballType = ballType;
        this.prestigeBonusType = prestigeBonusType;
    }

    private void Start()
    {
        prestigeManager = gameManager.GetComponent<PrestigeManager>();
        GetComponent<Button>().onClick.AddListener(AddLevelToPrestige);
    }


    private void AddLevelToPrestige() { 
        prestigeManager.AddLevelToPrestigeBonus(ballType, prestigeBonusType);
    }
}
