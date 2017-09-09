using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public LuckyBullet LuckyBulletObject;
    public CrazyBullet CrazyBulletObject;
    public Vector2 BulletStartOffset;

    public float Distance;

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var bullet = Instantiate(LuckyBulletObject);
            bullet.transform.position = transform.position + new Vector3(BulletStartOffset.x, BulletStartOffset.y);
            bullet.Shoot();
        }
    }



    public void Shoot()
    {
        var enemies = FindObjectOfType<Enemy>();
    }
}