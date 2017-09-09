using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseObject
{
    public DamageMultiplier Multipliers;
    public float Health = 100f;
    public Vector2 Speed;

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
    }
}