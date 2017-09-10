using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{


    [Header("Ammo")]
    public HorizontalLayoutGroup AmmoLayout;
    public GameObject AmmoDummy;
    public Sprite GoldBullet;
    public Sprite SilverBullet;
    public Sprite BronzeBullet;

    [Header(" ")]
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

    public void AmmoCount(PlayerPowerup powerup)
    {
        var dttt = new List<GameObject>();
        for (int i = 0; i < AmmoLayout.transform.childCount; i++)
            dttt.Add(AmmoLayout.transform.GetChild(i).gameObject);
        dttt.ForEach(Destroy);


        var powerUps = new List<PowerUpData>();
        powerUps.AddRange(powerup.Powerups.ToArray());
        if (powerup.ActivePowerup != null && !powerUps.Contains(powerup.ActivePowerup))
            powerUps.Add(powerup.ActivePowerup);
        Debug.Log(powerUps.Count);
        foreach (var item in powerUps)
        {
            Debug.Log("number of shoots " + item.NumberOfShoots);
            for (int i = 1; i <= item.NumberOfShoots; i++)
            {
                GameObject obj = Instantiate(AmmoDummy, AmmoLayout.transform);
                switch (item.Type)
                {
                    case BulletType.Gold:
                        obj.GetComponent<Image>().sprite = GoldBullet;
                        break;
                    case BulletType.Silver:
                        obj.GetComponent<Image>().sprite = SilverBullet;
                        break;
                    case BulletType.Bronze:
                        obj.GetComponent<Image>().sprite = BronzeBullet;
                        break;
                }
            }
        }
    }

    public void Lost()
    {
        SoundManager.Instance.PlayEnd(SoundManager.Instance.Lose);
        StartCoroutine(LostRoutine());
    }

    public void Win()
    {
        SoundManager.Instance.PlayEnd(SoundManager.Instance.Win);
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
