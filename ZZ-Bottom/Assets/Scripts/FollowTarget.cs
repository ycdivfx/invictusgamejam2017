using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform TargetToFollow;
    public float Drag = 0.05f;
    public float Buffer = 1f;

    private Vector3 m_newPosition;

    private void Awake()
    {
        m_newPosition = transform.position;
    }


    // Update is called once per frame
    private void Update() {
	    if (!(TargetToFollow.position.x - transform.position.x > Buffer)) return;

        m_newPosition = new Vector3(TargetToFollow.position.x, transform.position.y, transform.position.z);
        m_newPosition = Vector3.Lerp(transform.position, m_newPosition, Drag);
	}

    private void FixedUpdate()
    {
        transform.position = m_newPosition;
    }


}
