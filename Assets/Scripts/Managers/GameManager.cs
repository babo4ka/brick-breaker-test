using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int currentLevel = 1;


    void Start()
    {
        gameObject.GetComponent<BrickManager>().levelDone += OnLevelDone;
        gameObject.GetComponent<BrickManager>().InstantiateLevel(currentLevel);
    }



    private void OnLevelDone()
    {
        currentLevel++;
        gameObject.GetComponent<BrickManager>().InstantiateLevel(currentLevel);
    }

}
