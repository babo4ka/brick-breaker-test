using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    #region Objects
    [SerializeField]
    private GameObject gameManager;

    public delegate void Destroyed(GameObject brick);
    public Destroyed destroyed;

    public delegate void DropCashByBall(float amount);
    public DropCashByBall dropCashByBall;


    [SerializeField]
    private List<BallScript> balls = new List<BallScript>();
    #endregion

    #region Data
    [SerializeField]
    private BrickType _type;

    public BrickType type
    {
        get { return _type; }
    }


    [SerializeField]
    private float _health;

    public float health {
        get { return _health; }
        set { _health = value; } 
    }


    private float poisoned = 1.0f;
    private float longDamaged = 0.0f;
    private float shield = 0.0f;



    private Dictionary<BrickType, float> rewards = new Dictionary<BrickType, float>
    {
        {BrickType.HORIZONTAL, 1f}, {BrickType.VERTICAL, 2f},
        {BrickType.TRIANGLE, 15f}, {BrickType.HEX, 25f}
    };
    private float _rewardMultiplier = 1f;
    private float _cashBrickMultiplier = 1f;

    public float cashBrickMultiplier
    {
        get { return _cashBrickMultiplier; }
        set { _cashBrickMultiplier = value; }
    }

    public float rewardMultiplier
    {
        get { return _rewardMultiplier; }
        set { _rewardMultiplier = value; }
    }

    public float reward
    {
        get { return CountCashReward();}
    }
    #endregion

    #region Buffs info
    private BuffType _buffOnBrick;
    public BuffType buffOnBrick
    {
        get { return _buffOnBrick; }
        set { _buffOnBrick = value; }
    }
    #endregion

    private float CountCashReward()
    {
        return rewards[_type] * _rewardMultiplier * _cashBrickMultiplier;
    }


    private void OnDestroy()
    {
        foreach (BallScript ball in balls)
        {
            ball.attack -= getDamage;
        }

        destroyed?.Invoke(gameObject);
    }

    #region Balls subs
    public void UnsubscribeBall(BallScript bs)
    {
        bs.attack -= getDamage;
        balls.Remove(bs);
    }

    public void SubscribeToBall(BallScript bs)
    {
        bs.attack += getDamage;
        balls.Add(bs);
    }
    #endregion

    #region Getting damage

    private void LongDamage()
    {
        this._health -= longDamaged * poisoned * (shield == 0f ? 1 : shield);

        if (_health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void CashBallTrigger(float damage)
    {
        dropCashByBall?.Invoke(damage * health);
    }

    private void getDamage(float damage, DamageType type, GameObject objToDamage)
    {
        if(gameObject == objToDamage)
        {
            switch (type)
            {
                case DamageType.DAMAGE:
                    this._health -= damage * poisoned * (shield==0f?1:shield);
                    break;

                case DamageType.POISON:
                    if(this.poisoned == 1.0f)
                    {
                        this.poisoned = damage;
                    }
                    break;

                case DamageType.FIRE:
                    this._health -= damage * poisoned * (shield == 0f ? 1 : shield);

                    this.longDamaged += damage * 0.5f;
                    InvokeRepeating(nameof(LongDamage), 0f, 1f);
                    break;

                case DamageType.LONGPOISON:
                    this.longDamaged += damage;
                    InvokeRepeating(nameof(LongDamage), 0f, 1f);
                    break;

                case DamageType.CASH:
                    CashBallTrigger(damage);
                    break;

                case DamageType.KILL:
                    this._health = 0;
                    break;
            }

            if (_health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
    #endregion

    private void UpdateRewardMultiplier(CardType type, BonusStats<float> bs)
    {
        if(type == CardType.CASH)
        {
            if (bs.activate)
            {
                _rewardMultiplier += bs.value;
            }
            else
            {
                _rewardMultiplier -= bs.value;
            }
        }
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        gameManager.GetComponent<BonusManager>().updateCard += UpdateRewardMultiplier;

        BonusStats<float> bs = gameManager.GetComponent<BonusManager>().GetCardValue(CardType.CASH);

        if(bs.activate)
        {
            _rewardMultiplier += bs.value;
        }

        if(gameManager.GetComponent<GameManager>().getLevel > 50)
        {
            if(Random.Range(0f, 1f) <= 0.05f)
            {
                shield = 0.55f;
            }
        }
    }
}
