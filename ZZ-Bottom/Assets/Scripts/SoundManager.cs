using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource m_audioSourceSFX;
    public AudioSource m_audioSourceMusic;
    public AudioClip Music;
    public AudioClip Shoot;
    public AudioClip HitChar;
    public AudioClip HitEnemy;
    public AudioClip DieEnemy;
    public AudioClip DieBoss;
    public AudioClip Jump;
    public AudioClip Walk;
    public AudioClip Click;
    public AudioClip PowerUp;
    public AudioClip Win;
    public AudioClip Lose;


    public void PlaySfx(AudioClip clip)
    {
        m_audioSourceSFX.clip = clip;
        m_audioSourceSFX.Play();
    }

    public void PlayMusic()
    {
        m_audioSourceMusic.clip = Music;
        m_audioSourceMusic.loop = true;
        m_audioSourceMusic.Play();
    }

    public void StopMusic()
    {
        m_audioSourceMusic.Stop();
    }
}
