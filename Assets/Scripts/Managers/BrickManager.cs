using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickManager : MonoBehaviour {

    #region Level objects
    [SerializeField]
    private GameObject[] levelPrefabs;

    private GameObject currentLevelObject;
    private List<GameObject> _currentBricks = new List<GameObject>();
    #endregion

    #region Bricks HP
    private float _baseBrickHp = 1f;
    private float _bigBrickHp = 5f;
    private float _hexBrickHp = 10f;

    private float multiplier = 1.2f;
    private const float maxMultiplier = 9f;
    #endregion

    #region Delegates
    public delegate void LevelDone();
    public LevelDone levelDone;

    public delegate void DropCash(float amount);
    public DropCash dropCash;
    #endregion

    public void InstantiateLevel(int levelNum)
    {
        IncreaseHp(levelNum);
        currentLevelObject = Instantiate(levelPrefabs[1], new Vector2(0, 0), Quaternion.identity);
        SetBricks(GameObject.FindGameObjectsWithTag("Brick").ToList());
    }


    private void OnBrickDestroyed(GameObject brick)
    {
        BrickScript bs = brick.GetComponent<BrickScript>();
       // bs.destroyed -= OnBrickDestroyed;
        dropCash?.Invoke(bs.reward);

        _currentBricks.Remove(brick);

        if(_currentBricks.Count == 0) { 
            Destroy(currentLevelObject);
            levelDone?.Invoke();
        }
    }

    private void SetBricks(List<GameObject> bricks)
    {
        _currentBricks.Clear();
        _currentBricks = bricks;

        List<GameObject> balls = GameObject.FindGameObjectsWithTag("Ball").ToList();

        foreach(GameObject brick in _currentBricks)
        {
            foreach(GameObject ball in balls) {
                SubscribeBrick(ball, brick);
            }

            BrickScript bs = brick.GetComponent<BrickScript>();
            bs.destroyed += OnBrickDestroyed;

            switch (bs.type)
            {
                case 1:
                    bs.health = _baseBrickHp;
                    break;

                case 2:
                    bs.health = _bigBrickHp;
                    break;

                case 3:
                    bs.health = _hexBrickHp;
                    break;
            }
        }
    }

    public void IncreaseHp(int level)
    {
        if (level != 0 && level % 50 == 0 && multiplier < maxMultiplier)
        {
            multiplier += 0.1f;
        }

        _baseBrickHp *= multiplier;
        _bigBrickHp *= multiplier;
        _hexBrickHp *= multiplier;
    }

    private void SubscribeBrick(GameObject ball, GameObject brick)
    {
        brick.GetComponent<BrickScript>().SubscribeToBall(ball.GetComponent<BallScript>());
    }

    public void SubscribeBricks(GameObject ball)
    {
        foreach(GameObject brick in _currentBricks)
        {
            SubscribeBrick(ball, brick);
        }
    }
}
