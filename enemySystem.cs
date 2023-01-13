using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySystem : MonoBehaviour
{
    public static enemySystem instance;
    [SerializeField][Range(0, 10)] private float distance;
    [SerializeField][Range(0,10)] private float speed;
    private bool is_movingLeft;
    private float moving_left;
    private float moving_right;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        moving_left = transform.position.x - distance;
        moving_right = transform.position.x + distance;

    }
    // Update is called once per frame
    void Update()
    {
        enemyMouvement(speed);
    }

    private void enemyMouvement(float speed)
    {
        if (is_movingLeft)
        {
            if ( moving_left < transform.position.x)
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            } else
            {
                is_movingLeft = false;
            }

        } else
        {
            if (moving_right > transform.position.x)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else
            {
                is_movingLeft = true;
            }
        }
    }

   //public bool enemyDamagePlayer()
   // {
   //     if (is_damaged)
   //     {
   //         is_damaged = false;
   //         return true;
   //     } 
   //     else
   //     {
   //         return false;
   //     }
   // }
}
