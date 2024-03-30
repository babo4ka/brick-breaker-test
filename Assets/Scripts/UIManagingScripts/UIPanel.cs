using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> childPanels = new List<GameObject>();


    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        if (childPanels.Count > 0)
        {
            foreach (GameObject child in childPanels)
            {
                child.SetActive(false);
            }
        }

        gameObject.SetActive(false);
    }
}
