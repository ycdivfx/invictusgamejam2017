using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.WebCam;

public class BaseObject : MonoBehaviour
{
    public float MinGroundNormalY = .65f;
    public float GravityModifier = 1f;
    public int MaxJumps = 2;

    protected Vector2 m_targetVelocity;
    protected bool m_grounded;
    protected Vector2 m_groundNormal;
    protected Rigidbody2D m_rb2D;
    protected Vector2 m_velocity;
    protected ContactFilter2D m_contactFilter;
    protected RaycastHit2D[] m_hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> m_hitBufferList = new List<RaycastHit2D>(16);
    protected int m_jumps;

    protected const float MinMoveDistance = 0.001f;
    protected const float ShellRadius = 0.01f;

    private void OnEnable()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_contactFilter.useTriggers = false;
        m_contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        m_contactFilter.useLayerMask = true;
        OnStart();
    }

    protected virtual void OnStart()
    { }

    private void Update()
    {
        m_targetVelocity = Vector2.zero;
        ComputeVelocity();
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        
    }

    protected virtual void ComputeVelocity()
    {
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();

        m_velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        m_velocity.x = m_targetVelocity.x;

        m_grounded = false;

        var deltaPosition = m_velocity * Time.deltaTime;

        var moveAlongGround = new Vector2(m_groundNormal.y, -m_groundNormal.x);

        var move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    protected virtual void OnFixedUpdate()
    {
        
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = m_rb2D.Cast(move, m_contactFilter, m_hitBuffer, distance + ShellRadius);
            m_hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                m_hitBufferList.Add(m_hitBuffer[i]);
            }

            foreach (var hit2D in m_hitBufferList)
            {
                var currentNormal = hit2D.normal;
                if (currentNormal.y > MinGroundNormalY)
                {
                    m_grounded = true;
                    m_jumps = 0;
                    if (yMovement)
                    {
                        m_groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                var projection = Vector2.Dot(m_velocity, currentNormal);
                if (projection < 0)
                {
                    m_velocity = m_velocity - projection * currentNormal;
                }

                var modifiedDistance = hit2D.distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        m_rb2D.position = m_rb2D.position + move.normalized * distance;
    }
}