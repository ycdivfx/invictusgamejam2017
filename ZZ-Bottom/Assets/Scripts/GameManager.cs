using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<Scene> Scenes;
    public int Score;

    public void Lost()
    {
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        
    }

    public void Win()
    {
        
    }

    public void AddScore(BulletType enemyType)
    {
        switch (enemyType)
        {
            case BulletType.Normal:
                Score += 5;
                break;
            case BulletType.Bronze:
                Score += 10;
                break;
            case BulletType.Silver:
                Score += 15;
                break;
            case BulletType.Gold:
                Score += 20;
                break;
        }
    }
}
