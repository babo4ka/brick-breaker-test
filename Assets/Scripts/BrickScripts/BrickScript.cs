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
    private float burning = 0.0f;
    private float shield = 0.0f;



    private Dictionary<BrickType, float> rewards = new Dictionary<BrickType, float>
    {
        {BrickType.HORIZONTAL, 1f}, {BrickType.VERTICAL, 2f},
        {BrickType.TRIANGLE, 15f}, {BrickType.HEX, 25f}
    };
    private float _rewardMultiplier;

    public float reward
    {
        get { return CountCashReward();}
    }
    #endregion

    #region Buffs info
    public delegate void BuffAction(BuffType type, float duration);
    public BuffAction buffAction;

    private Dictionary<BuffType, float> buffsChance = new Dictionary<BuffType, float>
    {
        { BuffType.DIAMOND, 5f},
        { BuffType.CASH, 5f},
        { BuffType.SPEED, 0f }, { BuffType.DAMAGE, 0f}, { BuffType.CASHMULT, 0f}
    };

    private BuffType _buffOnBrick;
    public BuffType buffOnBrick
    {
        get { return buffOnBrick; }
    }

    private float buffDuration = 10.0f;

    private void ActBuff()
    {
        switch (_buffOnBrick)
        {
            case BuffType.DIAMOND:
            case BuffType.SPEED:
            case BuffType.DAMAGE: 
            case BuffType.CASHMULT:
                buffAction?.Invoke(_buffOnBrick, buffDuration);
                break;

            default:
                break;
        }
    }

    private void ChooseBuff()
    {
        float diamondEdge = 0 + buffsChance[BuffType.DIAMOND]/100;
        float cashEdge = diamondEdge + buffsChance[BuffType.CASH]/100;
        float speedEdge = cashEdge + buffsChance[BuffType.SPEED]/100;
        float damageEdge = speedEdge + buffsChance[BuffType.DAMAGE]/100;
        float cashMultEdge = damageEdge + buffsChance[BuffType.CASHMULT]/100;

        float roll = Random.Range(0f, 1f);

        if(roll > 0f && roll <= diamondEdge)
        {
            _buffOnBrick = BuffType.DIAMOND;
        }else if (roll > diamondEdge && roll <= cashEdge)
        {
            _buffOnBrick = BuffType.CASH;
        }else if(roll > cashEdge && roll <= speedEdge)
        {
            _buffOnBrick = BuffType.SPEED;
        }else if (roll > speedEdge && roll <= damageEdge)
        {
            _buffOnBrick = BuffType.DAMAGE;
        }
        else if(roll > damageEdge && roll <= cashMultEdge)
        {
            _buffOnBrick = BuffType.CASHMULT;
        }
        else
        {
            _buffOnBrick = BuffType.NONE;
        }
        
    }
    #endregion

    private float CountCashReward()
    {
        return rewards[_type] * (_rewardMultiplier==0f?1:_rewardMultiplier) * (_buffOnBrick==BuffType.CASH?10:1);
    }


    private void OnDestroy()
    {
        ActBuff();
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

    private void Burn()
    {
        this._health -= burning * poisoned * (shield == 0f ? 1 : shield);

        if (_health <= 0f)
        {
            Destroy(gameObject);
        }
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
                    if(this.burning == 0.0f)
                    {
                        this.burning = damage * 0.5f;
                        InvokeRepeating("Burn", 0f, 1f);
                    }
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
        ChooseBuff();
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
