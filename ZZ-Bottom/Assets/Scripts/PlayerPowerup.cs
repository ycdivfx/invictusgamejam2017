using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{
    public Queue<Powerup> Powerups = new Queue<Powerup>();
    public Powerup ActivePowerup;
}
