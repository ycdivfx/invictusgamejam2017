using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using zzbottom.helpers;

public class PlayerController : BaseObject
{

    public float MaxSpeed = 7;
    public float JumpTakeOffSpeed = 7;
    [SerializeField]
    private float m_health = 400f;

    public float MaxHealth = 400f;

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
            m_health = Mathf.Min(MaxHealth, m_health);
            if (m_health <= 0)
            {
                m_spriteRenderer.enabled = false;
                m_rb2D.simulated = false;
                GameObject.FindGameObjectsWithTag("enemy").ToList().ForEach(x =>
                {
                    var enemy = x.GetComponent<Enemy>();
                    if (enemy)
                        enemy.enabled = false;
                    var rg = x.GetComponent<Rigidbody2D>();
                    if(rg)
                        x.GetComponent<Rigidbody2D>().simulated = false;
                });
                enabled = false;
                GameManager.Instance.Lost();
                //GameObject.FindGameObjectsWithTag("boss").ToList().ForEach(Destroy);
                //Destroy(gameObject);
            }
            GameManager.Instance.PlayerHP(this);
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
        }

        bool jumpKey = Input.GetButtonDown("Jump");
        if (jumpKey && m_grounded)
        {
            SoundManager.Instance.PlaySfx(SoundManager.Instance.Jump);
            m_velocity.y = JumpTakeOffSpeed;
            m_jumps++;
        }

        if (jumpKey && !m_grounded && m_jumps < MaxJumps)
        {
            SoundManager.Instance.PlaySfx(SoundManager.Instance.Jump);
            m_velocity.y += JumpTakeOffSpeed;
            m_jumps++;
        }

        //bool flipSprite = (m_spriteRenderer.flipX ? (move.x > 0.0f) : (move.x < 0.0f));
        //if (flipSprite && move.x != 0)
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
        Health = MaxHealth;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.ToLower() == "enemy")
        {
            SoundManager.Instance.PlaySfx(SoundManager.Instance.HitChar);
            var enemy = col.gameObject;
            var rbd = enemy.GetComponent<Rigidbody2D>();
            if (m_grounded && m_rb2D.velocity.y < 0)
            {
                enemy.layer = LayerMask.NameToLayer("EnemiesTemp");
                enemy.GetComponent<Enemy>().StartCoroutine(SetEnemyToLayer(enemy, "Enemy", 3f));
            }
                //Physics2D.IgnoreCollision(m_rb2D.col, rbd);
            rbd.AddForce(new Vector2(12, 5), ForceMode2D.Impulse);
            Health -= 10;
            enemy.GetComponent<Enemy>().Health -= 2;
        }
        else if (col.gameObject.tag.ToLowerInvariant() == "enemy_bullet")
        {
            var bullet = col.gameObject.GetComponent<BaseBullet>();
            Debug.LogFormat("Shoot player with damage {0}", bullet.Damage);
            Health -= bullet.Damage;
            Destroy(col.gameObject);
        }
        GameManager.Instance.PlayerHP(this);
    }

    private IEnumerator SetEnemyToLayer(GameObject enemy, string layer, float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        enemy.layer = LayerMask.NameToLayer("Player");

    }

    protected override void OnUpdate()
    {
        var stats = m_playerWeapon.CheckLuckyShot();
        var isLucky = stats != null && Math.Abs(stats.Angle) < float.Epsilon;
		var willLucky = m_animator.GetBool ("lucky");
        if (willLucky == isLucky) return;
        m_animator.SetBool("lucky", isLucky);
    }
}
