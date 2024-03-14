using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour {

    [SerializeField]
    private float _amount;

    [SerializeField]
    private TMP_Text softCashText;


    private void Start()
    {
        gameObject.GetComponent<BrickManager>().dropCash += AddCash;
        AddCash(110600f);
    }


    public void AddCash(float amount)
    {
        _amount += amount;
        CashUpdated();
    }


    public bool SpendCash(float amount)
    {
        if(_amount < amount)
        {
            return false;
        }

        _amount -= amount;
        CashUpdated();
        return true;
    }

    private void CashUpdated()
    {
        softCashText.text = _amount.ToString() + "$";
    }
}
