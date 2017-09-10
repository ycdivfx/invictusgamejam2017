using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Time")]
    public float SpawnTime = 5f;
    public float Variance = 1f;
    [Header("Enemies")]
    public List<Enemy> Enemies = new List<Enemy>();
    public float Velocity;
    public float VelocityVariance;
    [Header("Enemy Weapon")]
    public float WeaponDistance = 15f;
    public float BulletsPerMinute = 2;
    [Range(0f, 1f)]
    public float WeaponProbability = 0.5f;
    [Header("Logic")]
    public float PlayerDistance = 20f;
    public float OffscreenBuffer = 0.1f;
    public bool Active;

    private float m_lastSpawn;
    private PlayerController m_player;

    [Header("Gizmos")]
    public bool ShowGizmo;

    private void Start()
    {
        m_lastSpawn = Time.time;
        m_player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        var pt = Camera.main.WorldToViewportPoint(transform.position);
        Active = (pt.x > 1 + OffscreenBuffer || pt.x < 0 - OffscreenBuffer)  && transform.position.x - m_player.transform.position.x <= PlayerDistance;

        if(Enemies.Count == 0 || !Active) return;
        var deltaTime = Time.time - m_lastSpawn;
        var spawnTime = SpawnTime + + Random.Range(-Variance, Variance);
        if (deltaTime >= spawnTime)
        {
            var index = Random.Range(0, Enemies.Count);
            var enemy = Instantiate(Enemies[index]);
            enemy.transform.position = transform.position;
            enemy.Speed = Velocity + Random.Range(-VelocityVariance, VelocityVariance);
            var weapon = enemy.GetComponent<EnemyWeapon>();
            if (weapon)
            {
                weapon.Probability = WeaponProbability;
                weapon.Distance = WeaponDistance;
                weapon.BulletsPerMinute = BulletsPerMinute;
            }
            m_lastSpawn = Time.time;
        }
    }
}


