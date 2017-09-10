using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using zzbottom.helpers;

public class Enemy : BaseObject
{
    [Header("Health and Damage")]
    public DamageMultiplier Multipliers;
    [SerializeField]
    private float m_health = 5;
    public float MaxHealth = 5;

    [Header("General")]
    public int Score;
    public LayerMask PlayerBullets;
    public BulletType Type = BulletType.Normal;

    [Header("AI")]
    public float Speed;
    public bool RandomJumps;
    [Range(0f, 1f)]
    public float JumpProbability = 0.25f;
    public float JumpEvery = 5f;
    public Vector2 JumpForce = new Vector2(12, 5);

    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    private ContactFilter2D m_bulletsFilter;
    private float m_lastJumpTime;

    public float Health
    {   
        get { return m_health; }
        set
        {
            m_health = value;
            if (m_health <= 0)
            {
                SoundManager.Instance.PlaySfx(SoundManager.Instance.DieEnemy);
                Speed = 0;
                OnHealth();
                DestroyObject(gameObject);
                GameManager.Instance.AddScore(Type);
            }
        }
    }

    protected virtual void OnHealth()
    {
        
    }

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        m_targetVelocity = Vector2.left * Speed;

        bool flipSprite = (m_spriteRenderer.flipX ? (m_targetVelocity.x > 0.01f) : (m_targetVelocity.x < 0.01f));
        if (!flipSprite)
            m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
    }

    protected override void OnStart()
    {
        m_bulletsFilter.SetLayerMask(PlayerBullets);
        m_bulletsFilter.useLayerMask = true;
        Health = MaxHealth;
    }

    protected override void OnFixedUpdate()
    {
        var collisions = m_rb2D.GetContacts(m_bulletsFilter);
        if (collisions.Count == 0) return;
        collisions.ForEach(x => x.collider.gameObject.GetComponent<BaseBullet>().DoDamage(this));
        SoundManager.Instance.PlaySfx(SoundManager.Instance.HitEnemy);
    }

    protected override void OnUpdate()
    {
        if(!RandomJumps) return;
        var delta = Time.time - m_lastJumpTime;
        if (JumpProbability >= Random.value && delta >= JumpEvery)
        {
            m_rb2D.AddForce(JumpForce, ForceMode2D.Impulse);
            m_lastJumpTime = Time.time;
        }
    }
}