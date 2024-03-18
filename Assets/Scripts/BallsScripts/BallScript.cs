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
        return _damage * (_damageMultiplier==0?1:_damageMultiplier);
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


    void UpdateBonus(BonusType type, BonusStats<float> bs)
    {
        switch (type)
        {
            case BonusType.BALLDAMAGE:
                if (bs.activate)
                {
                    _damageMultiplier += bs.value;
                }
                else
                {
                    _damageMultiplier -= bs.value;
                }
                break;

            case BonusType.BALLSPEED:
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
        gameManager.GetComponent<BonusManager>().updateBonus += UpdateBonus;

        List<BonusStats<float>> bonuses = new List<BonusStats<float>>
        {
            gameManager.GetComponent<BonusManager>().GetBonusValue(BonusType.BALLDAMAGE),
            gameManager.GetComponent<BonusManager>().GetBonusValue(BonusType.BALLSPEED)
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
