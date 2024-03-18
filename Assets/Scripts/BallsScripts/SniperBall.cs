using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBall : BallScript
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.tag == "Brick")
        {
            attack?.Invoke(CountDamage(), DamageType.DAMAGE, collision.gameObject);
        }

        if(collision.gameObject.tag == "Wall")
        {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach(GameObject brick in bricks)
            {
                Vector3 diff = brick.transform.position - position;
                float currDistance = diff.sqrMagnitude;

                if(currDistance < distance)
                {
                    closest = brick;
                    distance = currDistance;
                }

            }

            Vector2 goal = closest.transform.position - position;
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(goal.normalized * speed);
        }
    }

}
