using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallScript : MonoBehaviour
{
   
    public Rigidbody2D rigidbody { get;  set; }

    public Vector2 direction {  get;  set; }

    [SerializeField]
    private protected float speed {  get; set; }

    [SerializeField]
    private protected float damage {  get; set; }

    public delegate void Attack(float damage, DamageType type, GameObject gameObject);

    public Attack attack;


    public abstract void OnCollisionEnter2D(Collision2D collision);

    private protected void setTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = Random.Range(-1f, 1f);

        this.rigidbody.AddForce(force.normalized * this.speed);
    }

   
}
