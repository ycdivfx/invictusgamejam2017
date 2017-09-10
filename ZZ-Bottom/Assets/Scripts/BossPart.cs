using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPart : MonoBehaviour
{
    [SerializeField]
    private float m_health;
    public float MaxHealth;

    public Boss Boss;

    public float Health
    {
        get { return m_health; }
        set
        {
            m_health = value;
            if (m_health <= 0)
            {
                transform.parent.GetComponent<Boss>().DestroyPart(this);
            }
            GameManager.Instance.EnemyHP(transform.parent.GetComponent<Boss>());
        }
    }

    private void Start()
    {
        m_health = MaxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogFormat("Part received contact with {0}", collision.gameObject.name);
        if (collision.gameObject.tag.ToLowerInvariant() == "bullet")
        {
            var bullet = collision.gameObject.GetComponent<BaseBullet>();
            Debug.LogFormat("Shoot boss part with damage {0}", bullet.Damage);
            Health -= bullet.Damage;
            Destroy(collision.gameObject);
        }
    }
}
