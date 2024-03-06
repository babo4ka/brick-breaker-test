using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager
{
    private float _baseBrickHp = 1f;
    private float _bigBrickHp = 5f;
    private float _hexBrickHp = 10f;

    public float baseBrickHp
    {
        get { return _baseBrickHp; }
    }

    public float bigBrickHp
    {
        get { return _bigBrickHp; }
    }

    public float hexBrickHp
    {
        get { return _hexBrickHp; }
    }

    private float multiplier = 1.2f;
    private const float maxMultiplier = 9f;

    private List<GameObject> _currentBricks = new List<GameObject>();

    public List<GameObject> currentBricks
    {
        get { return _currentBricks;}
        set { SetBricks(value); }
    }

    public delegate void LevelDone();
    public LevelDone levelDone;

    private void OnBrickDestroyed(GameObject brick)
    {
        brick.GetComponent<BrickScript>().destroyed -= OnBrickDestroyed;

        _currentBricks.Remove(brick);

        if(_currentBricks.Count == 0) {
            levelDone?.Invoke();
        }
    }

    private void SetBricks(List<GameObject> bricks)
    {
        _currentBricks.Clear();
        _currentBricks = bricks;
        
        foreach(GameObject brick in _currentBricks)
        {
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
}
