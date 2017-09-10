using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Bullet Stuff")]
    public BaseBullet BulletPrefab;
    public Sprite BulletSprite;
    public Vector2 BulletStartOffset;
    public float Damage;
    public float Distance;
    public float BulletsPerMinute = 2;
    [Range(0f,1f)]
    public float Probability = 0.5f;

    [Header("Gizmo")]
    public bool ShowGizmo;

    private PlayerController m_player;
    private float m_lastShot;
    private SpriteRenderer m_renderer;
    private bool m_flip;
    private int m_direction;

    private void Start()
    {
        m_player = FindObjectOfType<PlayerController>();
        m_renderer = GetComponent<SpriteRenderer>();
        m_lastShot = Time.time;
        Probability = Mathf.Clamp(Probability, 0, 1);
    }

    private void Update()
    {
        m_flip = !m_renderer.flipX;
        m_direction = m_flip ? -1 : 1;
        var deltaTime = Time.time - m_lastShot;
        var rate = 60f / BulletsPerMinute;
        if (deltaTime >= rate && Vector3.Distance(transform.position, m_player.transform.position) <= Distance)
        {
            Debug.Log("Shooting.");
            if(Random.value >= 1 - Probability)
                Shoot();
            m_lastShot = Time.time;
        }
    }

    private void Shoot()
    {
        var bullet = Instantiate(BulletPrefab);
        bullet.GetComponent<SpriteRenderer>().sprite = BulletSprite;
        bullet.transform.position = transform.position + new Vector3(BulletStartOffset.x, BulletStartOffset.y);
        bullet.Damage = Damage;
        bullet.Type = BulletType.Normal;
        bullet.GetComponent<Rigidbody2D>().rotation = m_flip ? 180 : 0;
        bullet.Speed *= m_direction;
        bullet.Shoot(0);
    }

    private void OnDrawGizmos()
    {
        if(!ShowGizmo) return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + new Vector3(BulletStartOffset.x, BulletStartOffset.y), 0.25f);

        Gizmos.DrawLine(transform.position, transform.position - new Vector3(Distance * m_direction, 0));
    }
}
