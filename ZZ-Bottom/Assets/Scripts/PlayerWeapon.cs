using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public LuckyBullet LuckyBulletObject;
    public LazyBullet LazyBulletObject;
    public CrazyBullet CrazyBulletObject;
    public Vector2 BulletStartOffset;

    public float SweetDistance;

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        var enemies = FindObjectOfType<Enemy>();
        var bullet = Instantiate(CrazyBulletObject);
        bullet.transform.position = transform.position + new Vector3(BulletStartOffset.x, BulletStartOffset.y);
        bullet.Shoot();

    }
}