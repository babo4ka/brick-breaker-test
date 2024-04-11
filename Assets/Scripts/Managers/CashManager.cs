using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour {

    private BrickManager brickManager;

    [SerializeField]
    private float _softCashAmount;
    private float softCashMultiplier = 1f;

    private float buffStartTime;
    private float buffEndTime;
    private bool buffActive = false;

    [SerializeField]
    private float _hardCashAmount;

    [SerializeField]
    private TMP_Text softCashText;
    [SerializeField]
    private TMP_Text hardCashText;

    private float _totalSoftCashEarned;

    public float totalSoftCashEarned
    {
        get { return _totalSoftCashEarned; }
        set { _totalSoftCashEarned = value; }
    }


    private void Start()
    {
        brickManager = GetComponent<BrickManager>();
        brickManager.dropCash += AddSoftCash;
        brickManager.actBuff += GetHardByBuff;

        AddSoftCash(1106000000f);
        AddHardCash(10000000);
    }

    private void Update()
    {
        if(buffActive)
        {
            if(Time.time >= buffEndTime)
            {
                softCashMultiplier /= 2;
                buffActive = false;
            }
        }
    }

    private void GetHardByBuff(BuffType type, BonusStats<float> bs, float dur)
    {
        if(type == BuffType.DIAMOND)
        {
            AddHardCash(1);
        }
        else if (type == BuffType.CASHMULT)
        {
            if (bs.activate)
            {
                softCashMultiplier *= bs.value;
                AddTimeToBuff(10f);
            }
        }
    }


    private void AddTimeToBuff(float duration)
    {
        if (buffActive)
        {
            buffEndTime += duration;
        }
        else
        {
            buffStartTime = Time.time;
            buffEndTime = Time.time + duration;
            buffActive = true;
        }
        
    }


    private void AddSoftCash(float amount)
    {
        _softCashAmount += amount * softCashMultiplier;
        _totalSoftCashEarned += amount * softCashMultiplier;
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

    public void ResetSoftCash()
    {
        _softCashAmount = 0f;
        _totalSoftCashEarned = 0f;
        SoftCashUpdated();
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
