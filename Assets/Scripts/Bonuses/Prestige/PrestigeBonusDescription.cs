using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeBonusDescription : MonoBehaviour
{
    
    private static Dictionary<PrestigeBonusType, string> descriptions = new Dictionary<PrestigeBonusType, string>
    {
        { PrestigeBonusType.SPEED, "Increases ball speed." }, { PrestigeBonusType.DAMAGE, "Increases ball damage."},
        { PrestigeBonusType.RADIUS, "Increases splash radius." }
    };

    public static string GetDescription(PrestigeBonusType type)
    {
        return descriptions[type];
    }
}
