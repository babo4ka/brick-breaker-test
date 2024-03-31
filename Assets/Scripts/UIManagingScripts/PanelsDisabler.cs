using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsDisabler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> panels = new List<GameObject>();

    private void OnDisable()
    {
        foreach (GameObject p in panels)
        {
            p.SetActive(false);
        }
    }
}
