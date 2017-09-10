using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using zzbottom.helpers;

public class Boss : Enemy
{
    public List<BossPart> Parts = new List<BossPart>();
    public float FloatImpulseDistance = 5f;

    protected override void OnStart()
    {
        base.OnStart();
        m_runFixed = false;
        Parts = GetComponentsInChildren<BossPart>().ToList();
    }

    protected override void OnHealth()
    {
        GameManager.Instance.Win();
    }

    protected override void OnFixedUpdate()
    {
        var cols = m_rb2D.Cast(Vector2.down).Where(x => x.collider.gameObject.tag == "terrain").ToList();
        var floatImpulse = cols.Count != 0 && cols.OrderBy(x => x.distance).First().distance < FloatImpulseDistance;
        if (floatImpulse)
        {
            m_rb2D.AddForce(Vector2.up * Random.value * Speed, ForceMode2D.Impulse);
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.ToLowerInvariant() == "bullet" && Parts.Count == 0)
        {
            var bullet = col.gameObject.GetComponent<BaseBullet>();
            Debug.LogFormat("Shoot boss with damage {0}", bullet.Damage);
            Health -= bullet.Damage;
            Destroy(col.gameObject);
        }
        GameManager.Instance.EnemyHP(this);
    }

    public void DestroyPart(BossPart bossPart)
    {
        Debug.Log("Destroy boss part");
        Parts.Remove(bossPart);
        Destroy(bossPart.gameObject);
    }

    public float PartsHealth()
    {
        var health = Parts.Sum(x => x.Health);
        var maxHealth = Parts.Sum(x => x.MaxHealth);
        return health / maxHealth;
    }
}
