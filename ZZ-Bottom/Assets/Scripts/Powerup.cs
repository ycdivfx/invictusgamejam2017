using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PowerUpData
{
    public int NumberOfShoots = 2;
    public BulletType Type = BulletType.Normal;
}

public class Powerup : MonoBehaviour
{
    public PowerUpData Data;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogFormat("Trigger by {0}", collision.gameObject.tag);
        if (collision.gameObject.tag == "player")
        {
            var powerups = collision.gameObject.GetComponent<PlayerPowerup>();
            powerups.Powerups.Enqueue(this.Data);
            DestroyObject(gameObject);
        }
    }


}

public enum BulletType
{
    Normal,
    Gold,
    Silver,
    Bronze
}
