using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ballManagingPanels = new List<GameObject>();
    [SerializeField]
    private GameObject ballManagingContent;


    public void ResetUI()
    {
        foreach (GameObject panel in ballManagingPanels)
        {
            panel.SetActive(false);
        }

        Vector2 min = ballManagingContent.GetComponent<RectTransform>().offsetMin;
        min.y = 29.81772f;
        ballManagingContent.GetComponent<RectTransform>().offsetMin = min;
    }
}
