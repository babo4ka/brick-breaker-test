using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallScript : MonoBehaviour
{

    private protected GameObject gameManager;


    public Rigidbody2D rigidbody { get; set; }

    public Vector2 direction { get; set; }
    private Vector2 lastPos= Vector2.zero;

    [SerializeField]
    private float _speed = 0f;
    private protected float _speedMultiplier = 1f;
    [SerializeField]
    private float _damage;
    private protected float _damageMultiplier = 1f;

    private Dictionary<BuffType, float> buffStartTime = new Dictionary<BuffType, float>
    {
        {BuffType.SPEED, -1f}, {BuffType.DAMAGE, -1f}
    };
    private Dictionary<BuffType, float> buffEndTime = new Dictionary<BuffType, float>
    {
        {BuffType.SPEED, -1f}, {BuffType.DAMAGE, -1f}
    };
    private Dictionary<BuffType, bool> buffActive = new Dictionary<BuffType, bool>
    {
        {BuffType.SPEED, false}, {BuffType.DAMAGE, false}
    };

    private float _critDamage = 0f;

    public float critDamage
    {
        get { return _critDamage; }
        set { _critDamage = value; }
    }

    

    public float speed {
        get { return _speed; }
        set { UpdateSpeed(value); } 
    }

    public float damage {
        get { return _damage; }
        set { _damage = value; } 
    }

    public float damageMultiplier
    {
        get { return _damageMultiplier; }
        set { _damageMultiplier = value; }
    }

    private protected float CountDamage()
    {
        Debug.Log($"dmg mul {_damageMultiplier}");
        Debug.Log(_speedMultiplier);
        Debug.Log(_critDamage);
        return _damage * _damageMultiplier
            * Random.Range(0f, 1f) <= _critDamage / 100 ? 1.5f : 1f;
    }

    public delegate void Attack(float damage, DamageType type, GameObject gameObject);

    public Attack attack;



    public abstract void OnCollisionEnter2D(Collision2D collision);

    public void SetTrajectory()
    {
        Vector2 force = Vector2.zero;

        force.x = Random.Range(-1f, 1f);
        force.y = Random.Range(-1f, 1f);
        direction = force;
        
        rigidbody.AddForce(force.normalized * speed * _speedMultiplier);
    }

    public void UpdateSpeed(float newSpeed)
    {
        this._speed = newSpeed;
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(_speedMultiplier * newSpeed * direction.normalized);


        /*//newSpeed *= _speedMultiplier==0?1:_speedMultiplier;
        if (this._speed != 0f) {

            float diff = (newSpeed - this._speed) *(_speedMultiplier == 0 ? 1 : _speedMultiplier);

            rigidbody.AddForce(rigidbody.velocity.normalized * diff);
        }

        this._speed = newSpeed;*/
    }


    private void UpdateCard(CardType type, BonusStats<float> bs)
    {
        switch (type)
        {
            case CardType.BALLDAMAGE:
                if (bs.activate)
                {
                    _damageMultiplier *= bs.value;
                }
                else
                {
                    _damageMultiplier /= bs.value;
                }
                break;

            case CardType.BALLSPEED:
                if (bs.activate)
                {
                    _speedMultiplier *= bs.value;
                }
                else
                {
                    _speedMultiplier /= bs.value;
                }
                UpdateSpeed(this.speed);
                break;

            case CardType.CRITDAMAGE:
                if (bs.activate)
                {
                    if(critDamage == 0f)
                    {
                        critDamage += bs.value;
                    }
                    else
                    {
                        critDamage *= bs.value;
                    }
                }
                else
                {
                    if(critDamage == bs.value)
                    {
                        critDamage -= bs.value;
                    }
                    else
                    {
                        critDamage /= bs.value;
                    }
                }
                break;
        }
    }

    private void UpdateBuff(BuffType type, BonusStats<float> bs, float duration)
    {
        switch(type)
        {
            case BuffType.SPEED:
                if (bs.activate)
                {
                    _speedMultiplier *= bs.value;
                    buffActive[type] = true;
                    UpdateSpeed(this.speed);
                    AddTimeToBuff(type, 10f);
                }
                break;

            case BuffType.DAMAGE:
                _damageMultiplier *= bs.value;
                buffActive[type] = true;
                AddTimeToBuff(type, 10f);
                break;

            default:
                break;
        }


    }

    private protected abstract void UpdatePrestigeValue(BallType ballType,
        PrestigeBonusType prestigeBonusType,
        BonusStats<float> bs);

    private protected abstract void UpdatePrestigeValueInternal(PrestigeBonusType prestigeBonusType,
        BonusStats<float> bs);

    private void AddTimeToBuff(BuffType type, float duration)
    {
        if (buffActive[type])
        {
            buffEndTime[type] += duration;
        }
        else
        {
            buffStartTime[type] = Time.time;
            buffEndTime[type] = buffStartTime[type] + duration;
            buffActive[type] = true;
        }
    }

    private protected virtual void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<BonusManager>().updateCard += UpdateCard;
        gameManager.GetComponent<BrickManager>().actBuff += UpdateBuff;

        gameManager.GetComponent<PrestigeManager>().prestigeUpdate += UpdatePrestigeValue;

        List<BonusStats<float>> bonuses = new List<BonusStats<float>>
        {
            gameManager.GetComponent<BonusManager>().GetCardValue(CardType.BALLDAMAGE),
            gameManager.GetComponent<BonusManager>().GetCardValue(CardType.BALLSPEED)
        };


        if (bonuses[0].activate)
        {
            _damageMultiplier *= bonuses[0].value;
        }

        if (bonuses[1].activate)
        {
            _speedMultiplier *= bonuses[1].value;
        }
    }

    private void Update()
    {
        Vector2 position = transform.position;
        Vector2 nextPos = position * 2;

        direction = nextPos - position;
        /*Debug.Log(position);
        Debug.Log(nextPos);
        Debug.Log(direction);*/
        
        //lastPos = position;



        if (buffActive[BuffType.SPEED])
        {
            if (Time.time >= buffEndTime[BuffType.SPEED])
            {
                _speedMultiplier /= 2;
                UpdateSpeed(this.speed);
                buffActive[BuffType.SPEED] = false;
            }
        }

        if (buffActive[BuffType.DAMAGE])
        {
            if (Time.time >= buffEndTime[BuffType.DAMAGE])
            {
                _damageMultiplier /= 2;
                buffActive[BuffType.DAMAGE] = false;
            }
        }
    }
}
