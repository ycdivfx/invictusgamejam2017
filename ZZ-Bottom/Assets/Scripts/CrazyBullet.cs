using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyBullet : BaseBullet
{
    protected override void OnShoot()
    {
        var x = Mathf.Cos(Mathf.Deg2Rad * m_angle);
        var y = Mathf.Sin(Mathf.Deg2Rad * m_angle);
        var velocity = new Vector2(x, y).normalized * Speed;
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void Update()
    {
        //var deltaTime = (int)((Time.time - m_startTime) % 0.5f);
        //if (deltaTime != 1)
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0, Speed);

    }
}
