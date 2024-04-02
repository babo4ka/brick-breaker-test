using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeBonusDescription : MonoBehaviour
{
    
    private static Dictionary<PrestigeBonusType, string> descriptions = new Dictionary<PrestigeBonusType, string>
    {
        {PrestigeBonusType.SPEED, "Increases ball speed." }, { PrestigeBonusType.DAMAGE, "Increases ball damage."}
    };

    public static string GetDescription(PrestigeBonusType type)
    {
        return descriptions[type];
    }
}
