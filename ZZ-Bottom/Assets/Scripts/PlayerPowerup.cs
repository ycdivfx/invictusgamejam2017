using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using zzbottom.helpers;

public class PlayerPowerup : MonoBehaviour
{
    public Queue<PowerUpData> Powerups = new Queue<PowerUpData>();
    public PowerUpData ActivePowerup;
    public LayerMask PowerupLayer;
    private ContactFilter2D m_powerupFilter;

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
        GameManager.Instance.AmmoCount(this);
        return powerup.Type;
    }
}
