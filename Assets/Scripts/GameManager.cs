using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Cash fields
    private CashManager cashManager = new CashManager();
    #endregion


    #region Bricks fields
    private BrickManager brickManager = new BrickManager();
    #endregion

    [SerializeField]
    private GameObject[] levelPrefabs;

    private int currentLevel;

    private GameObject currentLevelObject;



    void Start()
    {
        brickManager.levelDone += OnLevelDone;
        InstantiateLevel(0);
        brickManager.dropCash += ClaimCash;
    }

    
    void Update()
    {
        
    }

    #region Cash methods
    private void ClaimCash(float amount)
    {
        cashManager.AddCash(amount);
    }
    #endregion

    private void OnLevelDone()
    {
        Destroy(currentLevelObject);
        currentLevel++;
        brickManager.IncreaseHp(currentLevel);
        InstantiateLevel(1);
    }


    void InstantiateLevel(int levelNum)
    {
        currentLevelObject = Instantiate(levelPrefabs[levelNum], new Vector2(0, 0), Quaternion.identity);
        List<GameObject> bricks = GameObject.FindGameObjectsWithTag("Brick").ToList();
        brickManager.currentBricks = bricks;

    }
}
