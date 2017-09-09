using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using zzbottom.helpers;

public class Enemy : BaseObject
{
    public DamageMultiplier Multipliers;
    [SerializeField]
    private float m_health = 5;
    public Vector2 Speed;
    public LayerMask PlayerBullets;
    public float TimeoutBeforeRemove = 2f;

    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    private ContactFilter2D m_bulletsFilter;

    public float Health
    {
        get { return m_health; }
        set
        {
            m_health = value;
            if (m_health <= 0)
            {
                Speed = Vector2.zero;
                DestroyObject(gameObject);
            }
        }
    }

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        m_targetVelocity = Speed;

        bool flipSprite = (m_spriteRenderer.flipX ? (m_targetVelocity.x > 0.01f) : (m_targetVelocity.x < 0.01f));
        if (flipSprite)
        {
            m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
        }
    }

    protected override void OnStart()
    {
        m_bulletsFilter.SetLayerMask(PlayerBullets);
        m_bulletsFilter.useLayerMask = true;
    }

    protected override void OnFixedUpdate()
    {
        var collisions = m_rb2D.GetContacts(m_bulletsFilter);
        collisions.ForEach(x => x.collider.gameObject.GetComponent<BaseBullet>().DoDamage(this));
    }
}