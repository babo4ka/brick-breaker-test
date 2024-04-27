using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BrickScript;

public class BrickManager : MonoBehaviour {

    private const string BASEBRHPKEY = "baseBrickHP";
    private const string BIGBRHPKEY = "bigBrickHP";
    private const string TRIBRHPKEY = "triangleBrickHP";
    private const string HEXBRHPKEY = "hexBrickHP";
    private const string HPMULKEY = "brickHPMultiplier";

    private BonusManager bonusManager;

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

    private float hpMultiplier = 1.2f;
    private const float maxHpMultiplier = 9f;
    #endregion

    #region Delegates
    public delegate void LevelDone();
    public LevelDone levelDone;

    public delegate void DropCash(float amount);
    public DropCash dropCash;

    public delegate void ActBuff(BuffType type, BonusStats<float> stats, float duration);
    public ActBuff actBuff;
    #endregion

    #region StageReward
    private float stageRewardAmount;
    private float stageRewardMultiplier = 1f;

    private float passiveRewardAmount;
    private float passiveRewardMultiplier = 1f;
    #endregion

    #region Buffs
    private Dictionary<BuffType, float> buffsChance = new Dictionary<BuffType, float>
    {
        { BuffType.DIAMOND, 5f},
        { BuffType.CASH, 5f},
        { BuffType.SPEED, 0f }, { BuffType.DAMAGE, 0f}, { BuffType.CASHMULT, 0f}
    };

    private float cashBrickMultiplier = 10f;
    private float cashMultiplier = 1f;

    private BuffType ChooseBuff()
    {
        float diamondEdge = 0 + buffsChance[BuffType.DIAMOND] / 100;
        float cashEdge = diamondEdge + buffsChance[BuffType.CASH] / 100;
        float speedEdge = cashEdge + buffsChance[BuffType.SPEED] / 100;
        float damageEdge = speedEdge + buffsChance[BuffType.DAMAGE] / 100;
        float cashMultEdge = damageEdge + buffsChance[BuffType.CASHMULT] / 100;

        float roll = Random.Range(0f, 1f);

        if (roll > 0f && roll <= diamondEdge)
        {
            return BuffType.DIAMOND;
        }
        else if (roll > diamondEdge && roll <= cashEdge)
        {
            return BuffType.CASH;
        }
        else if (roll > cashEdge && roll <= speedEdge)
        {
            return BuffType.SPEED;
        }
        else if (roll > speedEdge && roll <= damageEdge)
        {
            return BuffType.DAMAGE;
        }
        else if (roll > damageEdge && roll <= cashMultEdge)
        {
            return BuffType.CASHMULT;
        }
        else
        {
            return BuffType.NONE;
        }
    }

    private void UpdateCard(CardType type, BonusStats<float> bs)
    {
        List<GameObject> bricks = GameObject.FindGameObjectsWithTag("Brick").ToList();
        switch (type)
        {
            case CardType.CASH:
                if (bs.activate)
                {
                    cashMultiplier *= bs.value;
                }
                else
                {
                    cashMultiplier /= bs.value;
                }
                
                foreach(GameObject brick in bricks)
                {
                    brick.GetComponent<BrickScript>().rewardMultiplier = cashMultiplier;
                }
                break;

            case CardType.CASHBRICK:
                if (bs.activate)
                {
                    cashBrickMultiplier *= bs.value;
                }
                else
                {
                    cashBrickMultiplier /= bs.value;
                }
                foreach (GameObject brick in bricks)
                {
                    brick.GetComponent<BrickScript>().cashBrickMultiplier = cashBrickMultiplier;
                }
                break;

            case CardType.CASHBRICKCHANCE:
                if (bs.activate)
                {
                    buffsChance[BuffType.CASH] += bs.value;
                }
                else
                {
                    buffsChance[BuffType.CASH] -= bs.value;
                }
                break;

            case CardType.SPEEDBUFF:
                if (bs.activate)
                {
                    buffsChance[BuffType.SPEED] += bs.value;
                }
                else
                {
                    buffsChance[BuffType.SPEED] -= bs.value;
                }
                break;

            case CardType.DAMAGEBUFF:
                if (bs.activate)
                {
                    buffsChance[BuffType.DAMAGE] += bs.value;
                }
                else
                {
                    buffsChance[BuffType.DAMAGE] -= bs.value;
                }
                break;

            case CardType.CASHMULTBUFF:
                if (bs.activate)
                {
                    buffsChance[BuffType.CASHMULT] += bs.value;
                }
                else
                {
                    buffsChance[BuffType.CASHMULT] -= bs.value;
                }
                break;

            case CardType.STAGECASH:
                if (bs.activate)
                {
                    stageRewardMultiplier *= bs.value;
                }
                else
                {
                    stageRewardMultiplier /= bs.value;
                }
                break;

            case CardType.PASSIVEINCOMEMUL:
                if (bs.activate)
                {
                    passiveRewardMultiplier *= bs.value;
                }
                else
                {
                    passiveRewardMultiplier /= bs.value;
                }
                break;
        }
    }
    #endregion

    public void InstantiateLevel(int levelNum)
    {
        IncreaseHp(levelNum);
        currentLevelObject = Instantiate(levelPrefabs[2], new Vector2(0, 0), Quaternion.identity);
        SetBricks(GameObject.FindGameObjectsWithTag("Brick").ToList());
    }


    private void OnBrickDestroyed(GameObject brick)
    {
        BrickScript bs = brick.GetComponent<BrickScript>();
        dropCash?.Invoke(bs.reward);

        switch (bs.buffOnBrick)
        {
            case BuffType.DIAMOND:
                actBuff?.Invoke(bs.buffOnBrick, null, 0);
                break;
            case BuffType.SPEED:
            case BuffType.DAMAGE:
            case BuffType.CASHMULT:
                actBuff?.Invoke(bs.buffOnBrick, new BonusStats<float>(true, 2), 10f);
                break;

            default:
                break;
        }
        

        _currentBricks.Remove(brick);
        bs.destroyed -= OnBrickDestroyed;
        bs.dropCashByBall -= CashBallTrigger;

        if (_currentBricks.Count == 0) { 
            dropCash?.Invoke(stageRewardAmount * stageRewardMultiplier);
            Destroy(currentLevelObject);
            levelDone?.Invoke();
        }
    }

    private void CashBallTrigger(float amount)
    {
        dropCash?.Invoke(amount);
    }

    private void CountPassiveIncome()
    {
        dropCash?.Invoke(passiveRewardAmount * passiveRewardMultiplier);
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
            bs.buffOnBrick = ChooseBuff();
            bs.rewardMultiplier = cashMultiplier;
            if(bs.buffOnBrick == BuffType.CASH)
            {
                bs.cashBrickMultiplier = cashBrickMultiplier;
            }


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
        passiveRewardAmount = stageRewardAmount * 0.5f;
        InvokeRepeating(nameof(CountPassiveIncome), 0, 1);
    }

    public void IncreaseHp(int level)
    {
        if (level != 0 && level % 50 == 0 && hpMultiplier < maxHpMultiplier)
        {
            hpMultiplier += 0.1f;
            SaveLoadData<float> sld = new SaveLoadData<float>(HPMULKEY, hpMultiplier);
            sld.SaveData();
        }

        _baseBrickHp *= hpMultiplier;
        _bigBrickHp *= hpMultiplier;
        _triangleBrickHp *= hpMultiplier;
        _hexBrickHp *= hpMultiplier;

        List<SaveLoadData<float>> slds = new List<SaveLoadData<float>>
        {
            new SaveLoadData<float>(BASEBRHPKEY, _baseBrickHp),
            new SaveLoadData<float>(BIGBRHPKEY, _bigBrickHp),
            new SaveLoadData<float>(TRIBRHPKEY, _triangleBrickHp),
            new SaveLoadData<float>(HEXBRHPKEY, _hexBrickHp)
        };

        foreach (SaveLoadData<float> sld in slds)
        {
            sld.SaveData();
        }
    }

    public void ResetBricks()
    {
        _baseBrickHp = 1f;
        _bigBrickHp = 5f;
        _triangleBrickHp = 10f;
        _hexBrickHp = 10f;

        hpMultiplier = 1.2f;

        List<SaveLoadData<float>> slds = new List<SaveLoadData<float>>
        {
            new SaveLoadData<float>(BASEBRHPKEY),
            new SaveLoadData<float>(BIGBRHPKEY),
            new SaveLoadData<float>(TRIBRHPKEY),
            new SaveLoadData<float>(HEXBRHPKEY),

            new SaveLoadData<float>(HPMULKEY)
        };


        foreach (SaveLoadData<float> sld in slds)
        {
            sld.RemoveData();
        }
    }

    #region Bricks subscribing
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
    #endregion

    private void Start()
    {

        if (PlayerPrefs.HasKey(BASEBRHPKEY))
        {
            SaveLoadData<float> baseBtickHPsld = new SaveLoadData<float>(BASEBRHPKEY);
            _baseBrickHp = baseBtickHPsld.LoadData();
        }

        if (PlayerPrefs.HasKey(BIGBRHPKEY))
        {
            SaveLoadData<float> bigBtickHPsld = new SaveLoadData<float>(BIGBRHPKEY);
            _bigBrickHp = bigBtickHPsld.LoadData();
        }

        if (PlayerPrefs.HasKey(TRIBRHPKEY))
        {
            SaveLoadData<float> tirangleBtickHPsld = new SaveLoadData<float>(TRIBRHPKEY);
            _triangleBrickHp = tirangleBtickHPsld.LoadData();
        }

        if (PlayerPrefs.HasKey(HEXBRHPKEY))
        {
            SaveLoadData<float> hexBtickHPsld = new SaveLoadData<float>(HEXBRHPKEY);
            _hexBrickHp = hexBtickHPsld.LoadData();
        }

        if (PlayerPrefs.HasKey(HPMULKEY))
        {
            SaveLoadData<float> HPMulsld = new SaveLoadData<float>(HPMULKEY);
            hpMultiplier = HPMulsld.LoadData();
        }
        



        bonusManager = GetComponent<BonusManager>();

        bonusManager.updateCard += UpdateCard;

        BonusStats<float> bs = bonusManager.GetCardValue(CardType.CASH);
        if (bs.activate)
        {
            cashMultiplier *= bs.value;
        }

        bs = bonusManager.GetCardValue(CardType.CASHBRICK);
        if (bs.activate)
        {
            cashBrickMultiplier *= bs.value;
        }

        bs = bonusManager.GetCardValue(CardType.CASHBRICKCHANCE);
        if (bs.activate)
        {
            buffsChance[BuffType.CASH] += bs.value;
        }

        bs = bonusManager.GetCardValue(CardType.SPEEDBUFF);
        if (bs.activate)
        {
            buffsChance[BuffType.SPEED] += bs.value;
        }

        bs = bonusManager.GetCardValue(CardType.DAMAGEBUFF);
        if (bs.activate)
        {
            buffsChance[BuffType.DAMAGE] += bs.value;
        }

        bs = bonusManager.GetCardValue(CardType.CASHMULTBUFF);
        if (bs.activate)
        {
            buffsChance[BuffType.CASHMULT] += bs.value;
        }

        bs = bonusManager.GetCardValue(CardType.STAGECASH);
        if (bs.activate)
        {
            stageRewardMultiplier *= bs.value;
        }

        bs = bonusManager.GetCardValue(CardType.PASSIVEINCOMEMUL);
        if (bs.activate)
        {
            passiveRewardMultiplier *= bs.value;
        }
    }
}
