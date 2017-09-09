using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyBullet : BaseBullet
{
    protected override void OnShoot()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, Speed);
    }

    private void Update()
    {
        var deltaTime = (int)((Time.time - m_startTime) % 0.5f);
        if (deltaTime != 1)
            GetComponent<Rigidbody2D>().gravityScale = 10f;
    }
}
