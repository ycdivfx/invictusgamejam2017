using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaboleBullet : BaseBullet
{
    protected override void OnShoot()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, Speed);
    }
}