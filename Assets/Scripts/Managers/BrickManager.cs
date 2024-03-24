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
    private float _triangleBrickHp = 10f;
    private float _hexBrickHp = 15f;

    private float multiplier = 1.2f;
    private const float maxMultiplier = 9f;
    #endregion

    #region Delegates
    public delegate void LevelDone();
    public LevelDone levelDone;

    public delegate void DropCash(float amount);
    public DropCash dropCash;
    #endregion

    #region StageReward
    private float stageRewardAmount;

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
        dropCash?.Invoke(bs.reward);

        _currentBricks.Remove(brick);
        bs.destroyed -= OnBrickDestroyed;
        bs.dropCashByBall -= CashBallTrigger;

        if (_currentBricks.Count == 0) { 
            dropCash?.Invoke(stageRewardAmount);
            Destroy(currentLevelObject);
            levelDone?.Invoke();
        }
    }

    private void CashBallTrigger(float amount)
    {
        dropCash?.Invoke(amount);
    }

    private void SetBricks(List<GameObject> bricks)
    {
        _currentBricks.Clear();
        _currentBricks = bricks;
        stageRewardAmount = 0f;

        List<GameObject> balls = GameObject.FindGameObjectsWithTag("Ball").ToList();

        foreach(GameObject brick in _currentBricks)
        {
            foreach(GameObject ball in balls) {
                SubscribeBrick(ball, brick);
            }

            BrickScript bs = brick.GetComponent<BrickScript>();

            stageRewardAmount+= bs.reward;

            bs.destroyed += OnBrickDestroyed;
            bs.dropCashByBall += CashBallTrigger;


            switch (bs.type)
            {
                case BrickType.HORIZONTAL:
                    bs.health = _baseBrickHp;
                    break;

                case BrickType.VERTICAL:
                    bs.health = _bigBrickHp;
                    break;

                case BrickType.TRIANGLE:
                    bs.health = _triangleBrickHp;
                    break;

                case BrickType.HEX:
                    bs.health = _hexBrickHp;
                    break;
            }
        }

        stageRewardAmount *= 0.7f;
    }

    public void IncreaseHp(int level)
    {
        if (level != 0 && level % 50 == 0 && multiplier < maxMultiplier)
        {
            multiplier += 0.1f;
        }

        _baseBrickHp *= multiplier;
        _bigBrickHp *= multiplier;
        _triangleBrickHp *= multiplier;
        _hexBrickHp *= multiplier;
    }

    public void ResetBricks()
    {
        _baseBrickHp = 1f;
        _bigBrickHp = 5f;
        _triangleBrickHp = 10f;
        _hexBrickHp = 10f;
    }

    private void SubscribeBrick(GameObject ball, GameObject brick)
    {
        brick.GetComponent<BrickScript>().SubscribeToBall(ball.GetComponent<BallScript>());
    }

    private void UnsubscribeBrick(GameObject ball, GameObject brick)
    {
        brick.GetComponent<BrickScript>().UnsubscribeBall(ball.GetComponent<BallScript>());
    }

    public void SubscribeBricks(GameObject ball)
    {
        foreach(GameObject brick in _currentBricks)
        {
            SubscribeBrick(ball, brick);
        }
    }

    public void UnsubscribeBricks(GameObject ball)
    {
        foreach (GameObject brick in _currentBricks)
        {
            UnsubscribeBrick(ball, brick);
        }
    }

    void Start()
    {
    }
}
