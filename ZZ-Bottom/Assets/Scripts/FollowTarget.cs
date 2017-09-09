using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform TargetToFollow;
    public float Drag = 0.05f;

    // Update is called once per frame
    private void FixedUpdate () {
	    if (!(TargetToFollow.position.x > transform.position.x)) return;

        var newPosition = new Vector3(TargetToFollow.position.x, transform.position.y, transform.position.z);
	    transform.position = Vector3.Lerp(transform.position, newPosition, Drag);
	}
}
