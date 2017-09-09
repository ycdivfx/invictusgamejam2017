using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Bullet Stuff")]
    public BaseBullet BulletPrefab;
    public Sprite BulletSprite;
    public Vector2 BulletStartOffset;
    public float Damage;

    private void FixedUpdate()
    {
        //Shoot();
    }

    private void Shoot()
    {
        var bullet = Instantiate(BulletPrefab);
        bullet.GetComponent<SpriteRenderer>().sprite = BulletSprite;
        bullet.transform.position = transform.position + new Vector3(BulletStartOffset.x, BulletStartOffset.y);
        bullet.Damage = Damage;
        bullet.Type = BulletType.Normal;
    }
}
