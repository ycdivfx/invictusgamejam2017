using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zzbottom.helpers;

public class PlayerController : BaseObject
{

    public float MaxSpeed = 7;
    public float JumpTakeOffSpeed = 7;
    [SerializeField]
    private float m_health = 400f;
    public LayerMask EnemyBullets;
    public Vector2 DebugVelocity;

    private ContactFilter2D m_bulletsFilter;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    private PlayerWeapon m_playerWeapon;

    public float Health
    {
        get { return m_health; }
        set
        {
            m_health = value;
            if (m_health <= 0)
            {
                GameManager.Instance.Lost();
            }
        }
    }

    // Use this for initialization
    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        var move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Horizontal"))
        {
            SoundManager.Instance.PlaySfx(SoundManager.Instance.Walk);
            Debug.Log("Move");
        }

        if (Input.GetButtonDown("Jump") && m_grounded)
        {
            SoundManager.Instance.PlaySfx(SoundManager.Instance.Jump);
            m_velocity.y = JumpTakeOffSpeed;
            m_jumps++;
        }
        //else if (Input.GetButtonUp("Jump"))
        //{
        //    if (m_velocity.y > 0)
        //    {
        //        m_velocity.y = m_velocity.y * 0.5f;
        //    }
        //}
        if (Input.GetButtonDown("Jump") && !m_grounded && m_jumps < MaxJumps)
        {
            SoundManager.Instance.PlaySfx(SoundManager.Instance.Jump);
            m_velocity.y += JumpTakeOffSpeed;
            m_jumps++;
        }

        //bool flipSprite = (m_spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        //if (flipSprite)
        //{
        //    m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
        //}

        m_animator.SetBool("grounded", m_grounded);
        m_animator.SetFloat("velocityX", Mathf.Abs(m_velocity.x) / MaxSpeed);

        m_targetVelocity = move * MaxSpeed;
        DebugVelocity = m_velocity;
    }

    protected override void OnStart()
    {
        m_bulletsFilter.SetLayerMask(EnemyBullets);
        m_bulletsFilter.useLayerMask = true;
        m_playerWeapon = GetComponent<PlayerWeapon>();
    }

    protected override void OnFixedUpdate()
    {
        var collisions = m_rb2D.GetContacts(m_bulletsFilter);
        collisions.ForEach(x =>
        {
            SoundManager.Instance.PlaySfx(SoundManager.Instance.HitChar);
            var bullet = x.collider.gameObject.GetComponent<BaseBullet>();
            bullet.DoPlayer(this);
        });
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.ToLower() == "enemy")
        {
            SoundManager.Instance.PlaySfx(SoundManager.Instance.HitChar);
            var enemy = col.gameObject;
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(12, 5), ForceMode2D.Impulse);
            Health -= 10;
            enemy.GetComponent<Enemy>().Health -= 2;
         }
    }

    protected override void OnUpdate()
    {
        var stats = m_playerWeapon.CheckLuckyShot();
        var isLucky = stats != null && Math.Abs(stats.Angle) < float.Epsilon;
        if (m_animator.GetBool("lucky") == isLucky) return;
        m_animator.SetBool("lucky", isLucky);
    }

}
