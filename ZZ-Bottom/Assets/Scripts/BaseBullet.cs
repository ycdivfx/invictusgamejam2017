using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float Damage = 10f;
    public float DestroyAfter = 2f;
    private float m_startTime;

    public void Shoot()
    {
        m_startTime = Time.time;
    }

    private void FixedUpdate()
    {
        var lifeTime = Time.time - m_startTime;
        if(lifeTime > DestroyAfter)
            Destroy(gameObject);
    }
}
