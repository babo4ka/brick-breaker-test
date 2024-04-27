using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour {

    private const string SOFTCASHKEY = "softCash";
    private const string SOFTCASHTOTALKEY = "softCashTotal";
    private const string HARDCASHKEY = "hardCash";

    private BrickManager brickManager;

    [SerializeField]
    private float _softCashAmount;
    private float softCashMultiplier = 1f;

    private float buffStartTime;
    private float buffEndTime;
    private bool buffActive = false;

    [SerializeField]
    private int _hardCashAmount;

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

        //AddSoftCash(1106000000f);
        //AddSoftCash(500f);
        LoadSoftCash();
        LoadHardCash();
        //AddHardCash(10000000);
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
        SaveSoftCash();
    }

    public void AddHardCash(int amount)
    {
        _hardCashAmount += amount;
        HardCashUpdated();
        SaveHardCash();
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
        ResetSoftCashFromBase();
    }

    public bool SpendHardCash(int amount)
    {
        if(_hardCashAmount < amount)
        {
            return false;
        }
        _hardCashAmount -= amount;
        HardCashUpdated();
        return true;
    }

    private void SaveSoftCash()
    {
        SaveLoadData<float> sld = new SaveLoadData<float>(SOFTCASHKEY, _softCashAmount);
        SaveLoadData<float> sldTotal = new SaveLoadData<float>(SOFTCASHTOTALKEY, _totalSoftCashEarned);
        sld.SaveData();
        sldTotal.SaveData();
    }

    private void ResetSoftCashFromBase()
    {
        SaveLoadData<float> sld = new SaveLoadData<float>(SOFTCASHKEY);
        SaveLoadData<float> sldTotal = new SaveLoadData<float>(SOFTCASHTOTALKEY);
        sld.RemoveData();
        sldTotal.RemoveData();
    }

    private void LoadSoftCash()
    {
        if (PlayerPrefs.HasKey(SOFTCASHKEY))
        {
            SaveLoadData<float> sld = new SaveLoadData<float>(SOFTCASHKEY);
            SaveLoadData<float> sldTotal = new SaveLoadData<float>(SOFTCASHTOTALKEY);
            float sc = sld.LoadData();
            float total = sldTotal.LoadData();
            _softCashAmount = sc;
            _totalSoftCashEarned = total;
            SoftCashUpdated();
        }
    }

    private void SaveHardCash()
    {
        SaveLoadData<int> sld = new SaveLoadData<int>(HARDCASHKEY, _hardCashAmount);
        sld.SaveData();
    }

    private void LoadHardCash()
    {
        if (PlayerPrefs.HasKey(HARDCASHKEY))
        {
            SaveLoadData<int> sld = new SaveLoadData<int>(HARDCASHKEY);
            int hc = sld.LoadData();
            _hardCashAmount = hc;
            HardCashUpdated();
        }
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
