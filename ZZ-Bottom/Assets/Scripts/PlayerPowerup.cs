using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zzbottom.helpers;

public class PlayerPowerup : MonoBehaviour
{
    public Queue<PowerUpData> Powerups = new Queue<PowerUpData>();
    public PowerUpData ActivePowerup;
    private Rigidbody2D m_rb2D;
    public LayerMask PowerupLayer;
    private ContactFilter2D m_powerupFilter;


    private void Awake()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_powerupFilter.SetLayerMask(PowerupLayer);
        m_powerupFilter.useLayerMask = true;
    }

    public BulletType Use()
    {
        if (Powerups.Count > 0 && ActivePowerup == null)
            ActivePowerup = Powerups.Dequeue();

        if (ActivePowerup == null) return BulletType.Normal;
        var powerup = ActivePowerup;
        powerup.NumberOfShoots--;
        if (powerup.NumberOfShoots == 0) ActivePowerup = null;
        return powerup.Type;
    }

    //private void FixedUpdate()
    //{
    //    var collisions = m_rb2D.GetContacts(m_powerupFilter);
    //    if(collisions.Count == 0) return;

    //    collisions.ForEach(x =>
    //    {
    //        Powerups.Enqueue(x.collider.GetComponent<Powerup>());
    //        DestroyObject(x.collider.gameObject);
    //    });
    //}
}
