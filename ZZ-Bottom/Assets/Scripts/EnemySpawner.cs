using System.Collections;
using System.Collections.Generic;
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

    public bool Active;

    private float m_lastSpawn;

    private void Start()
    {
        m_lastSpawn = Time.time;
    }

    private void Update()
    {
        if(Enemies.Count == 0 || !Active) return;
        var deltaTime = Time.time - m_lastSpawn;
        var spawnTime = SpawnTime + + Random.Range(-Variance, Variance);
        if (deltaTime >= spawnTime)
        {
            var index = Random.Range(0, Enemies.Count);
            var enemy = Instantiate(Enemies[index]);
            enemy.transform.position = transform.position;
            enemy.Speed = Velocity + Random.Range(-VelocityVariance, VelocityVariance);
            m_lastSpawn = Time.time;
        }
    }
}


