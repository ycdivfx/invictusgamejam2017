using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public float Damage = 1;
    public float DestroyAfter = 2f;
    public float Speed = 10f;
    protected float m_startTime;
    protected float m_angle = 0f;

    public void Shoot(float shotAngle)
    {
        m_startTime = Time.time;
        m_angle = shotAngle;
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

    public void DoDamage(Enemy enemy)
    {
        enemy.Health -= Damage;
        Destroy(gameObject);
    }
}
