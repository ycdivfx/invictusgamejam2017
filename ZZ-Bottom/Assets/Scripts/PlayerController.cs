using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseObject {

    public float MaxSpeed = 7;
    public float JumpTakeOffSpeed = 7;
    public float Health = 400f;

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

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            Velocity.y = JumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (Velocity.y > 0)
            {
                Velocity.y = Velocity.y * 0.5f;
            }
        }

        bool flipSprite = (m_spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
        }

        m_animator.SetBool("grounded", Grounded);
        m_animator.SetFloat("velocityX", Mathf.Abs(Velocity.x) / MaxSpeed);

        TargetVelocity = move * MaxSpeed;
    }
}
