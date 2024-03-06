using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Cash
    private CashManager cashManager = new CashManager();
    #endregion


    #region Bricks
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
    }

    
    void Update()
    {
        
    }

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
