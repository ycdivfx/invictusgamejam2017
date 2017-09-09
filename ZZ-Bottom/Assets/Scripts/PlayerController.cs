using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseObject
{

    public float MaxSpeed = 7;
    public float JumpTakeOffSpeed = 7;
    public float Health = 400f;
    public Vector2 DebugVelocity;

    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

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
}
