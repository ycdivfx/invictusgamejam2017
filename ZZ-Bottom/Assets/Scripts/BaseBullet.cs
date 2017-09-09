﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float Damage = 10f;
    public float DestroyAfter = 2f;
    public float Speed = 10f;
    protected float m_startTime;

    public void Shoot()
    {
        m_startTime = Time.time;
        OnShoot();
    }

    protected virtual void OnShoot()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, 0);
    }

    private void FixedUpdate()
    {
        var lifeTime = Time.time - m_startTime;
        if(lifeTime > DestroyAfter)
            Destroy(gameObject);
    }
}
