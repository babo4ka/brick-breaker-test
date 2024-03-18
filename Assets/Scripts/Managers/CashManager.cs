using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour {

    [SerializeField]
    private float _softCashAmount;
    [SerializeField]
    private float _hardCashAmount;

    [SerializeField]
    private TMP_Text softCashText;
    [SerializeField]
    private TMP_Text hardCashText;


    private void Start()
    {
        gameObject.GetComponent<BrickManager>().dropCash += AddSoftCash;
        AddSoftCash(1106000000f);
    }


    public void AddSoftCash(float amount)
    {
        _softCashAmount += amount;
        SoftCashUpdated();
    }

    public void AddHardCash(float amount)
    {
        _hardCashAmount += amount;
        HardCashUpdated();
    }


    public bool SpendSoftCash(float amount)
    {
        if(_softCashAmount < amount)
        {
            return false;
        }

        _softCashAmount -= amount;
        SoftCashUpdated();
        return true;
    }

    public bool SpendHardCash(float amount)
    {
        if(_hardCashAmount < amount)
        {
            return false;
        }
        _hardCashAmount -= amount;
        HardCashUpdated();
        return true;
    }

    private void SoftCashUpdated()
    {
        softCashText.text = _softCashAmount.ToString() + "$";
    }

    private void HardCashUpdated()
    {
        hardCashText.text = _hardCashAmount.ToString();
    }
}
