using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string CURRENTLVLKEY = "currentLvl";

    [SerializeField]
    private GameObject topPrefab;
    [SerializeField]
    private GameObject bottomPrefab;
    [SerializeField]
    private GameObject leftPrefab;
    [SerializeField] 
    private GameObject rightPrefab;


    private int currentLevel = 1;

    public int getLevel
    {
        get { return currentLevel;}
    }


    void Start()
    {
        SaveLoadData<int> sld = new SaveLoadData<int>(CURRENTLVLKEY);
        currentLevel = sld.LoadData();

        GetComponent<BrickManager>().levelDone += OnLevelDone;
        GetComponent<BrickManager>().InstantiateLevel(currentLevel);

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Instantiate(topPrefab, new Vector2(0f, max.y+0.2f), Quaternion.identity);
        Instantiate(bottomPrefab, new Vector2(0f, min.y - 0.2f), Quaternion.identity);

        Instantiate(leftPrefab, new Vector2(min.x - 0.2f, 0f), Quaternion.identity);
        Instantiate(rightPrefab, new Vector2(max.x + 0.2f, 0f), Quaternion.identity);

    }



    private void OnLevelDone()
    {
        currentLevel++;
        GetComponent<BrickManager>().InstantiateLevel(currentLevel);

        SaveLoadData<int> sld = new SaveLoadData<int>(CURRENTLVLKEY, currentLevel);
        sld.SaveData();
    }
    
    public void ResetGame()
    {
        currentLevel = 1;
        
        SaveLoadData<int> sld = new SaveLoadData<int>(CURRENTLVLKEY, currentLevel);
        sld.SaveData();

        BrickManager bm = GetComponent<BrickManager>();
        bm.ResetBricks();
        GetComponent<BallManager>().ResetBalls();
        IncreaseValueScript[] ballManagingPanels = Resources.FindObjectsOfTypeAll<IncreaseValueScript>();
        foreach(IncreaseValueScript ivs in ballManagingPanels)
        {
            ivs.ResetValues();
        }

        GetComponent<UIManager>().ResetUI();

        

        GetComponent<CashManager>().ResetSoftCash();

        bm.InstantiateLevel(currentLevel);
    }

}
