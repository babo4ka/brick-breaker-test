using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeBonusWrapper : MonoBehaviour
{
    private PrestigeBonusType type;

    private PrestigeBonus prestigeBonus;

    private PrestigeBonusWrapper _left;
    private PrestigeBonusWrapper _right;

    public PrestigeBonusWrapper left
    {
        get { return _left; }
        set { _left = value; }
    }

    public PrestigeBonusWrapper right
    {
        get { return _right; }
        set { _right = value; }
    }


    public PrestigeBonusWrapper(PrestigeBonusType type)
    {
        this.type = type;
    }


    public float GetPrestigeBonusValue(PrestigeBonusType type)
    {
        if(type == this.type)
        {
            return prestigeBonus.value;
        }

        if (prestigeBonus.level < 3 || _left == null)
        {
            return -1f;
        }

        float value = _left.GetPrestigeBonusValue(type);

        if(value == -1f && _right != null)
        {
            value = _right.GetPrestigeBonusValue(type);
        }

        return value;
    }

    public float GetNextPrestigeBonusValue(PrestigeBonusType type)
    {
        if(type == this.type)
        {
            return prestigeBonus.value + prestigeBonus.levelStep;
        }

        float value = -1f;

        if(_left != null)
        {
            value = _left.GetNextPrestigeBonusValue(type);
        }

        if (value == -1f && _right != null)
        {
            value = _right.GetNextPrestigeBonusValue(type);
        }

        return value;
    }

    public bool AddLevelToPrestigeBonus(PrestigeBonusType type)
    {
        if(type == this.type)
        {
            prestigeBonus.AddLevel();
            return true;
        }

        if(prestigeBonus.level < 3 || _left == null)
        {
            return false;
        }
        
        bool added = _left.AddLevelToPrestigeBonus(type);

        if(!added && _right != null) {
            added = _right.AddLevelToPrestigeBonus(type);
        }

        return added;

    }
}
