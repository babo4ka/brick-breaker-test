using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallScript : MonoBehaviour
{

    private protected GameObject gameManager;

   
    public Rigidbody2D rigidbody { get;  set; }

    public Vector3 direction {  get;  set; }
    private Vector3 lastPos;

    [SerializeField]
    private float _speed = 0f;
    private float _speedMultiplier;
    [SerializeField]
    private float _damage;
    private float _damageMultiplier;

    private float _critDamage;

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
        Debug.Log(_damageMultiplier);
        Debug.Log(_speedMultiplier);
        Debug.Log(_critDamage);
        return _damage * (_damageMultiplier == 0 ? 1 : _damageMultiplier)
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
        
        rigidbody.AddForce(force.normalized * speed * (_speedMultiplier == 0 ? 1 : _speedMultiplier));
    }

    public void UpdateSpeed(float newSpeed)
    {
        this._speed = newSpeed;
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(direction.normalized * newSpeed * (_speedMultiplier == 0 ? 1 : _speedMultiplier));


        /*//newSpeed *= _speedMultiplier==0?1:_speedMultiplier;
        if (this._speed != 0f) {

            float diff = (newSpeed - this._speed) *(_speedMultiplier == 0 ? 1 : _speedMultiplier);

            rigidbody.AddForce(rigidbody.velocity.normalized * diff);
        }

        this._speed = newSpeed;*/
    }


    void UpdateCard(CardType type, BonusStats<float> bs)
    {
        switch (type)
        {
            case CardType.BALLDAMAGE:
                if (bs.activate)
                {
                    _damageMultiplier += bs.value;
                }
                else
                {
                    _damageMultiplier -= bs.value;
                }
                break;

            case CardType.BALLSPEED:
                if (bs.activate)
                {
                    _speedMultiplier += bs.value;
                }
                else
                {
                    _speedMultiplier -= bs.value;
                }
                UpdateSpeed(this.speed);
                break;
        }
    }

    void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<BonusManager>().updateCard += UpdateCard;

        List<BonusStats<float>> bonuses = new List<BonusStats<float>>
        {
            gameManager.GetComponent<BonusManager>().GetCardValue(CardType.BALLDAMAGE),
            gameManager.GetComponent<BonusManager>().GetCardValue(CardType.BALLSPEED)
        };


        if (bonuses[0].activate)
        {
            _damageMultiplier += bonuses[0].value;
        }

        if (bonuses[1].activate)
        {
            _speedMultiplier += bonuses[1].value;
        }
    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        if(lastPos != null)
        {
            direction = position - lastPos;
        }
        lastPos = position;
    }
}
