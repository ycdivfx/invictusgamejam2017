using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBullet : BaseBullet
{
    float x;
    float y;
    Vector2 velocity;
    float force;


    protected override void OnShoot()
    {
        x = Mathf.Cos(Mathf.Deg2Rad * m_angle);
        y = Mathf.Sin(Mathf.Deg2Rad * m_angle);

        Debug.Log(x);
        Debug.Log(y);

        velocity = new Vector2(x, y) * Speed;
        var force = velocity.magnitude;

        Debug.Log(force);
        GetComponent<Rigidbody2D>().velocity = new Vector2(x * force, y * force);

        /*
         velocity = new Vector2(x, y) * force;
                force = velocity.magnitude;

                Debug.Log(force);
                GetComponent<Rigidbody2D>().velocity = new Vector2(x * force, y * force);

                force -= 2f;
        */
    }
}
