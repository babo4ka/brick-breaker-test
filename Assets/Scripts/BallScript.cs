using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
   
    public Rigidbody2D rigidbody { get; private set; }

    public Vector2 direction {  get; private set; }

    [SerializeField]
    private float speed = 500f;

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
