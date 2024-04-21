using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private const string BALLMNGNGPNLHEIGHTKEY = "ballManagingHeight";

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

        SaveLoadData<float> sld = new SaveLoadData<float>(BALLMNGNGPNLHEIGHTKEY, min.y);
        sld.SaveData();

        ballManagingContent.GetComponent<RectTransform>().offsetMin = min;
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey(BALLMNGNGPNLHEIGHTKEY))
        {
            SaveLoadData<float> sld = new SaveLoadData<float>(BALLMNGNGPNLHEIGHTKEY);
            float y = sld.LoadData();
            Vector2 min = ballManagingContent.GetComponent<RectTransform>().offsetMin;
            min.y = y;

            ballManagingContent.GetComponent<RectTransform>().offsetMin = min;
        }
    }
}
