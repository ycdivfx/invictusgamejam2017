using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerWeapon : MonoBehaviour
{
    public BaseBullet LuckyBullet;
    public List<BaseBullet> CrazyBulletObject = new List<BaseBullet>();
    public Vector2 BulletStartOffset;
    [Header("Graphics")]
    public Sprite NormalBullet;
    public Sprite GoldBullet;
    public Sprite SilverBullet;
    public Sprite BronzeBullet;
    public float SweetDistance;
    public List<BulletStats> Stats;
    [Header("Debug")]
    public float shotAngle;
    public int index;
    public Enemy closerEnemy;
    public float prob;

    private PlayerPowerup m_powerups;

    private void Awake()
    {
        m_powerups = GetComponent<PlayerPowerup>();
        if (!m_powerups) m_powerups = gameObject.AddComponent<PlayerPowerup>();
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }


    private void Shoot()
    {
        SoundManager.Instance.PlaySfx(SoundManager.Instance.Shoot);
        shotAngle = Random.Range(-1f, 1f) * 40f;
        index = Random.Range(0, CrazyBulletObject.Count - 1);
        closerEnemy = FindObjectsOfType<Enemy>().ToList().OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).FirstOrDefault();


        if (closerEnemy != null && closerEnemy.transform.position.x > transform.position.x)
        {
            var enemyDistance = Vector2.Distance(closerEnemy.transform.position - new Vector3(0.5f, 0), transform.position + new Vector3(0.5f, 0));
            prob = Mathf.Clamp(enemyDistance / SweetDistance, 0f, 1.1f);
            var stat = Stats.FirstOrDefault(x => x.InRange(prob) == 0);
            if (stat != null)
                shotAngle = stat.Angle;
        }
        bool isLuckShot = Math.Abs(shotAngle) < float.Epsilon;
        if (isLuckShot)
        {
            Debug.Log("Luckshot");
        }
        var bullet = Instantiate(isLuckShot ? LuckyBullet : CrazyBulletObject[index]);

        bullet.GetComponent<SpriteRenderer>().sprite = NormalBullet;
        bullet.transform.position = transform.position + new Vector3(BulletStartOffset.x, BulletStartOffset.y);
        bullet.Damage = 1;
        bullet.Type = m_powerups.Use();
        switch (bullet.Type)
        {
            case BulletType.Normal:
                bullet.GetComponent<SpriteRenderer>().sprite = NormalBullet;
                bullet.name += " Normal";
                break;
            case BulletType.Gold:
                bullet.GetComponent<SpriteRenderer>().sprite = GoldBullet;
                bullet.name += " Gold";
                break;
            case BulletType.Silver:
                bullet.GetComponent<SpriteRenderer>().sprite = SilverBullet;
                bullet.name += " Silver";
                break;
            case BulletType.Bronze:
                bullet.GetComponent<SpriteRenderer>().sprite = BronzeBullet;
                bullet.name += " Bronze";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        //bullet.transform.localScale = new Vector3(2,2,2);
        bullet.GetComponent<Rigidbody2D>().rotation = shotAngle;
        bullet.Shoot(shotAngle);
    }
}

[Serializable]
public class BulletStats
{
    public BulletProblability Probability;
    public float Angle = 0f;

    public int InRange(float probability)
    {
        var result = -99;
        if (probability > Probability.Min && probability <= Probability.Max)
            result = 0;
        if (probability <= Probability.Min)
            result = -1;
        if (probability > Probability.Max)
            result = 1;
        return result;
    }
}

[Serializable]
public class BulletProblability
{
    [Range(-0.1f, 1.1f)]
    public float Min = 0f;
    [Range(-0.1f, 1.1f)]
    public float Max = 0f;
}