using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour {

    [SerializeField]
    private float _amount;


    private void Start()
    {
        gameObject.GetComponent<BrickManager>().dropCash += AddCash;
    }


    public void AddCash(float amount)
    {
        _amount += amount;
        Debug.Log(_amount);
    }


    public bool SpendCash(float amount)
    {
        if(_amount < amount)
        {
            return false;
        }

        _amount -= amount;
        return true;
    }
}
