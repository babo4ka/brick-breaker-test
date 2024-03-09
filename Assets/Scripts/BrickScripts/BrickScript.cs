using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public const int BASIC = 1;
    public const int BIG = 2;
    public const int HEX = 3;

    private const float value = 1f;

    [SerializeField]
    private int _type;

    public int type {  get { return _type; } }

    [SerializeField]
    private float _health;

    
    public float health {
        get { return _health; }
        set { _health = value; } 
    }


    private float mul = 1.0f;

    [SerializeField]
    private List<BallScript> balls = new List<BallScript>();

    public delegate void Destroyed(GameObject brick);
    public Destroyed destroyed;


    private Dictionary<int, float> multipliers = new Dictionary<int, float>
    {
        {1, 1f}, {2, 2f}, {3, 25f}
    };

    public float reward
    {
        get { return value * multipliers[type];}
    }


    private void OnDestroy()
    {
        foreach (BallScript ball in balls)
        {
            ball.attack -= getDamage;
        }

        destroyed?.Invoke(gameObject);
    }

    public void SubscribeToBall(BallScript bs)
    {
        bs.attack += getDamage;
        balls.Add(bs);
    }

    private void getDamage(float damage, DamageType type, GameObject objToDamage)
    {
        if(gameObject == objToDamage)
        {
            switch (type)
            {
                case DamageType.DAMAGE:
                    this._health -= damage * mul;
                    break;

                case DamageType.POISON:
                    if(this.mul == 1.0f)
                    {
                        this.mul += damage;
                    }
                    break;
            }

            if (_health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
