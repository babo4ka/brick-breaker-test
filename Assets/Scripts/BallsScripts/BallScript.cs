using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallScript : MonoBehaviour
{
   
    public Rigidbody2D rigidbody { get;  set; }

    public Vector2 direction {  get;  set; }

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _damage;

    public float speed {
        get { return _speed; }
        set { _speed = value; } 
    }

    public float damage {
        get { return _damage; }
        set { _damage = value; } 
    }

    public delegate void Attack(float damage, DamageType type, GameObject gameObject);

    public Attack attack;


    public abstract void OnCollisionEnter2D(Collision2D collision);

    public void setTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = Random.Range(-1f, 1f);

        rigidbody.AddForce(force.normalized * speed * 100f);
    }

   
}
