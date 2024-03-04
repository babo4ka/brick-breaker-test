using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levelPrefabs;

    private GameObject currentLevel;
    private int currentLevelBricksCount;


    void Start()
    {
        InstantiateLevel(0);
    }

    
    void Update()
    {
        
    }

    private void decreaseBricksCount(GameObject brick)
    {
        currentLevelBricksCount--;
        brick.GetComponent<BrickScript>().destroyed -= decreaseBricksCount;

        if(currentLevelBricksCount == 0)
        {
            Destroy(currentLevel);
            InstantiateLevel(1);
        }
    }

    void InstantiateLevel(int levelNum)
    {
        currentLevel = Instantiate(levelPrefabs[levelNum], new Vector2(0, 0), Quaternion.identity);
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        currentLevelBricksCount = bricks.Length;

        foreach(GameObject brick in bricks)
        {
            brick.GetComponent<BrickScript>().destroyed += decreaseBricksCount;
        }
    }
}
