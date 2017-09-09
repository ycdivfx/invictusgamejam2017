using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public LuckyBullet LuckyBulletObject;
    public CrazyBullet CrazyBulletObject;

    public float Distance;

    public void Shoot()
    {
        var enemies = FindObjectOfType<Enemy>();
    }
}