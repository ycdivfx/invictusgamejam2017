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

    public void AddScore(int score)
    {
        
    }
}
