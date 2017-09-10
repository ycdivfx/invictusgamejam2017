using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public List<string> Scenes;
    public Text ScoreText;
    public Image HPPlayer;
    public Image HPEnemy;
    public Image HPEnemyShield;
    public int Score;
    public int SceneIndex = 0;
    public float Timeout = 3f;

    public void PlayerHP(PlayerController player)
    {
        HPPlayer.fillAmount = player.Health / player.MaxHealth;
    }

    public void EnemyHP(Boss enemy)
    {
        HPEnemy.fillAmount = enemy.Health / enemy.MaxHealth;
        HPEnemyShield.fillAmount = enemy.PartsHealth();
    }

    public void Lost()
    {
        SoundManager.Instance.PlaySfx(SoundManager.Instance.Lose);
        StartCoroutine(LostRoutine());
    }

    public void Win()
    {
        SoundManager.Instance.PlaySfx(SoundManager.Instance.Win);
        StartCoroutine(WinRoutine());
    }

    private IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(Timeout);
        NextLevel();
    }

    private IEnumerator LostRoutine()
    {
        yield return new WaitForSeconds(Timeout);
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        SceneIndex++;
        if (SceneIndex < Scenes.Count)
            SceneManager.LoadScene(Scenes[SceneIndex]);
        else
        {
            SceneIndex = 0;
            SceneManager.LoadScene("Menu");
        }
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
        ScoreText.text = Score.ToString();
    }
}
