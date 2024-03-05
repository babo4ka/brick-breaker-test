using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levelPrefabs;

    private int currentLevel;


    private GameObject currentLevelObject;
    private int currentLevelBricksCount;

    private float baseBrickHp = 1f;
    private float bigBrickHp = 5f;
    private float hexBrickHp = 10f;
    private float multiplier = 1.2f;
    private const float maxMultiplier = 9f;

    private void IncreaseHp()
    {
        if(currentLevel % 50 == 0 && multiplier < maxMultiplier)
        {
            multiplier += 0.1f;
        }

        baseBrickHp *= multiplier;
        bigBrickHp *= multiplier;
        hexBrickHp *= multiplier;
    }



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
            Destroy(currentLevelObject);
            IncreaseHp();
            InstantiateLevel(1);
        }
    }

    void InstantiateLevel(int levelNum)
    {
        currentLevelObject = Instantiate(levelPrefabs[levelNum], new Vector2(0, 0), Quaternion.identity);
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        currentLevelBricksCount = bricks.Length;

        foreach(GameObject brick in bricks)
        {
            BrickScript bs = brick.GetComponent<BrickScript>();
            bs.destroyed += decreaseBricksCount;
            switch(bs.type)
            {
                case 1:
                    bs.health = baseBrickHp;
                    break;

                case 2:
                    bs.health = bigBrickHp;
                    break;

                case 3:
                    bs.health = hexBrickHp;
                    break;
            }
        }
    }
}
