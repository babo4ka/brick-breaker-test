using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
   
    public Rigidbody2D rigidbody { get; private set; }

    public Vector2 direction {  get; private set; }

    [SerializeField]
    private float speed = 500f;

    [SerializeField]
    private float damage = 10f;

    public delegate void Attack(float damage, string name);

    public Attack attack;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision == null) return;

        if(collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(damage, collision.gameObject.name);
        }
    }


    void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();       
    }

    void Start()
    {
        setTrajectory();
    }

    private void setTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = Random.Range(-1f, 1f);

        this.rigidbody.AddForce(force.normalized * this.speed);
    }

    
    void Update()
    {
        
    }
}
