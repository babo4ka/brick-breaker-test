using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickScript : MonoBehaviour
{

    [SerializeField]
    private float health;
    
    private float mul = 1.0f;

    [SerializeField]
    private List<GameObject> balls = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        balls.Clear();
        balls = GameObject.FindGameObjectsWithTag("Circle").ToList();

        foreach(GameObject ball in balls)
        {
            ball.GetComponent<BallScript>().attack += getDamage;
        }
    }


    private void getDamage(float damage, DamageType type, string name)
    {
        if(gameObject.name == name)
        {
            switch (type)
            {
                case DamageType.DAMAGE:
                    this.health -= damage * mul;
                    break;

                case DamageType.POISON:
                    if(this.mul == 1.0f)
                    {
                        this.mul += damage;
                    }
                    break;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
