using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float Damage = 10f;
    public float DestroyAfter = 2f;
    public float Speed = 10f;
    private float m_startTime;

    public void Shoot()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, 0);
        m_startTime = Time.time;
    }

    private void FixedUpdate()
    {
        var lifeTime = Time.time - m_startTime;
        if(lifeTime > DestroyAfter)
            Destroy(gameObject);
    }
}
