using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject topPrefab;
    [SerializeField]
    private GameObject bottomPrefab;
    [SerializeField]
    private GameObject leftPrefab;
    [SerializeField] 
    private GameObject rightPrefab;


    private int currentLevel = 1;


    void Start()
    {
        gameObject.GetComponent<BrickManager>().levelDone += OnLevelDone;
        gameObject.GetComponent<BrickManager>().InstantiateLevel(currentLevel);

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
        gameObject.GetComponent<BrickManager>().InstantiateLevel(currentLevel);
    }

    public void ResetGame()
    {
        currentLevel = 1;
        BrickManager bm = gameObject.GetComponent<BrickManager>();
        bm.ResetBricks();
        GetComponent<BallManager>().ResetBalls();
        GetComponent<CashManager>().ResetSoftCash();

        bm.InstantiateLevel(currentLevel);
    }

}
