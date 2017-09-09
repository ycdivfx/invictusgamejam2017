using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseObject
{
    public DamageMultiplier Multipliers;
    public float Health = 100f;
    public Vector2 Speed;
    public LayerMask PlayerBullets;

    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

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
    }

    protected override void OnFixedUpdate()
    {
        ContactPoint2D[] collisions = new ContactPoint2D[16];
        //int count = m_rb2D.GetContacts(collisions);

    }
}