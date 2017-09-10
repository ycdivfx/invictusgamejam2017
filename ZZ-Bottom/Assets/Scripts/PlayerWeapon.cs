using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerWeapon : MonoBehaviour
{
    public BaseBullet LuckyBullet;
    public List<BaseBullet> CrazyBulletObject = new List<BaseBullet>();
    public Vector2 BulletStartOffset;
    [Header("Weapon")]
    public float CooldownWhen = 5f;
    public float CooldownTime = 2f;
    public float DetectShoot = 1f;
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
    public bool IsCoolingDown;

    private PlayerPowerup m_powerups;
    private bool m_shoot = false;
    private float m_lastShootTime;
    private float m_shootStartTime;
    private float m_cooldownStartTime;
    private bool m_allowShoots = true;
    private SpriteRenderer m_renderer;
    private Sequence m_seq;

    private void Awake()
    {
        m_powerups = GetComponent<PlayerPowerup>();
        m_renderer = GetComponent<SpriteRenderer>();
        if (!m_powerups) m_powerups = gameObject.AddComponent<PlayerPowerup>();

        //m_seq = DOTween.Sequence();
        //m_seq.Append(m_renderer.DOFade(0.5f, 1f / CooldownTime));
        //m_seq.Append(m_renderer.DOFade(1f, 1f / CooldownTime));
        //m_seq.SetLoops(-1, LoopType.Yoyo);
        //m_seq.Kill();

    }

    private void Update()
    {
        var time = Time.time;
        if (Input.GetButtonDown("Fire1") && m_allowShoots)
        {
            Shoot();
            m_lastShootTime = time;
            if (!m_shoot)
            {
                m_shoot = true;
                m_shootStartTime = time;
            }
        }
        else if (time - m_lastShootTime > DetectShoot && m_allowShoots)
        {
            m_shoot = false;
        }

        if (m_shoot && time - m_shootStartTime >= CooldownWhen && m_allowShoots)
        {
            IsCoolingDown = true;
            m_seq = DOTween.Sequence();
            m_seq.Append(m_renderer.DOFade(0.5f, 0.2f));
            m_seq.Append(m_renderer.DOFade(1f, 0.2f));
            m_seq.SetLoops(-1, LoopType.Yoyo);
            m_seq.Play();
            m_cooldownStartTime = time;
            m_allowShoots = false;
        }
        if (!m_allowShoots && time - m_cooldownStartTime >= CooldownTime)
        {
            m_seq.Kill(true);
            IsCoolingDown = false;
            m_allowShoots = true;
            m_shoot = false;
        }
    }



    private void Shoot()
    {
        SoundManager.Instance.PlaySfx(SoundManager.Instance.Shoot);
        shotAngle = 40f;
        index = Random.Range(0, CrazyBulletObject.Count);

        var stat = CheckLuckyShot();
        if (stat != null)
            shotAngle = stat.Angle;

        bool isLuckShot = Math.Abs(shotAngle) < float.Epsilon;

        SoundManager.Instance.PlaySfx(isLuckShot ? SoundManager.Instance.LuckyShot : SoundManager.Instance.Shoot);

        if (!isLuckShot)
        {
            if (stat == null) shotAngle = Random.Range(-shotAngle, shotAngle);
            else
            {
                var neg = stat.UseNegative ? (Random.Range(0, 2) == 0 ? 1f : -1f) : 1f;
                shotAngle = Random.Range(stat.MinAngle, shotAngle) * neg;
            }
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

    public BulletStats CheckLuckyShot()
    {
        closerEnemy = FindObjectsOfType<Enemy>().ToList().OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).FirstOrDefault();
        if (closerEnemy != null && closerEnemy.transform.position.x > transform.position.x)
        {
            var enemyDistance = Vector2.Distance(closerEnemy.transform.position - new Vector3(0.5f, 0), transform.position + new Vector3(0.5f, 0));
            prob = Mathf.Clamp(enemyDistance / SweetDistance, 0f, 1.1f);
            var stat = Stats.FirstOrDefault(x => x.InRange(prob) == 0);
            return stat;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        //if (!ShowGizmo) return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + new Vector3(BulletStartOffset.x, BulletStartOffset.y), 0.25f);
    }
}

[Serializable]
public class BulletStats
{
    public BulletProblability Probability;
    public float Angle = 0f;
    public float MinAngle = 0f;
    public bool UseNegative;

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