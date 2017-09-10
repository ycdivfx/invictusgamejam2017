using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

        }
    }
    //   public Transform TargetToFollow;
    //   public float Drag = 0.05f;
    //   public float Buffer = 1f;

    //   private Vector3 m_newPosition;
    //   private Vector3 m_offset;

    //   private void Awake()
    //   {
    //       m_newPosition = transform.position;
    //   }

    //   private void Start()
    //   {
    //       m_offset = transform.position - TargetToFollow.transform.position;
    //   }

    //   // Update is called once per frame
    ////   private void LateUpdate() {
    ////    //if (!(TargetToFollow.position.x - transform.position.x > Buffer)) return;

    ////       m_newPosition = new Vector3(TargetToFollow.position.x, transform.position.y, transform.position.z);
    ////       m_newPosition = Vector3.Lerp(transform.position, m_newPosition, Drag);
    ////}

    //   private void FixedUpdate()
    //   {
    //       m_newPosition = new Vector3(TargetToFollow.position.x, transform.position.y, transform.position.z);
    //       m_newPosition = Vector3.Lerp(transform.position, m_newPosition, Drag);
    //       transform.position = m_newPosition;
    //   }


}
