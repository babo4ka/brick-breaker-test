using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadData<DT>
{
    private DT _value;
    private string _name;

    public DT value
    {
        get { return _value; }
        set { _value = value; }
    }

    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    public SaveLoadData(string name, DT value)
    {
        _name = name;
        _value = value;
    }

    public SaveLoadData(string name)
    {
        _name = name;
    }


    public void SaveData()
    {
       if(_value is int intVal)
       {
            PlayerPrefs.SetInt(_name, intVal);
       }
       else if(_value is float floatVal)
       {
            PlayerPrefs.SetFloat(_name, floatVal);
       }
       else if(_value is string srtVal)
       {
            PlayerPrefs.SetString(_name, srtVal);
       }

       PlayerPrefs.Save();
    }

    public DT LoadData()
    {
        if (PlayerPrefs.HasKey(_name))
        {
            if (_value is int)
            {
                _value = (DT)Convert.ChangeType(PlayerPrefs.GetInt(_name), typeof(DT));
            }
            else if (_value is float)
            {
                _value = (DT)Convert.ChangeType(PlayerPrefs.GetFloat(_name), typeof(DT));
            }
            else if (_value is string)
            {
                _value = (DT)Convert.ChangeType(PlayerPrefs.GetString(_name), typeof(DT));
            }

            return _value;
        }
        else
        {
            return default;
        }
          
    }
}
