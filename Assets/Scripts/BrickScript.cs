using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickScript : MonoBehaviour
{

    [SerializeField]
    private float health;

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


    private void getDamage(float damage, string name)
    {
        if(gameObject.name == name)
        {
            this.health -= damage;
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
