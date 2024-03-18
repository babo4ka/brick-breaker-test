using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallScript : MonoBehaviour
{
    private GameObject gameManager;

   
    public Rigidbody2D rigidbody { get;  set; }

    public Vector2 direction {  get;  set; }

    [SerializeField]
    private float _speed = 0f;
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
        
        rigidbody.AddForce(force.normalized * speed);

    }

    public void UpdateSpeed(float newSpeed)
    {
        if (this._speed != 0f) {
            float diff = newSpeed - this._speed;

            rigidbody.AddForce(rigidbody.velocity.normalized * diff);
        }

        this._speed = newSpeed;
        
    }

    void UpdateBonus(BonusType type, BonusStats<float> bs)
    {
        if(type == BonusType.BALLDAMAGE)
        {
            if (bs.activate)
            {
                _damageMultiplier += bs.value;
            }
            else
            {
                _damageMultiplier -= bs.value;
            }
        }
    }

    void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<BonusManager>().updateBonus += UpdateBonus;

        BonusStats<float> mul = gameManager.GetComponent<BonusManager>().GetBonusValue(BonusType.BALLDAMAGE);

        if (mul.activate)
        {
            _damageMultiplier += mul.value;
        }
    }


}
