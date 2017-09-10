using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Settings")]
    private bool music = true;
    private bool sfx = true;

    [Header("Audio Management UI Elements")]
    public AudioSource m_audioSourceSFX;
    public AudioSource m_audioSourceEnd;
    public Toggle toggleSFX;
    public Slider sliderSFX;

    public AudioSource m_audioSourceMusic;
    public Toggle toggleMusic;
    public Slider sliderMusic;

    [Header("Sounds")]
    public AudioClip GameMusic;
    public AudioClip MenuMusic;
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
    public AudioClip LuckyShot;


    public void PlaySfx(AudioClip clip)
    {
        m_audioSourceSFX.clip = clip;
        if (sfx)
            m_audioSourceSFX.Play();
    }

    public void PlayEnd(AudioClip clip)
    {
        m_audioSourceMusic.DOFade(0.5f, 0.2f);
        m_audioSourceSFX.DOFade(0.0f, 0.2f);
        m_audioSourceEnd.clip = clip;
        if (sfx)
            m_audioSourceEnd.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        m_audioSourceMusic.clip = clip;
        m_audioSourceMusic.loop = true;
        if (music)
            m_audioSourceMusic.Play();
    }

    public void MuteSFX()
    {
        m_audioSourceSFX.mute = !m_audioSourceSFX.mute;
    }
    public void MuteMusic()
    {
        m_audioSourceMusic.mute = !m_audioSourceMusic.mute;
    }

    public void VolumeSFX()
    {
        m_audioSourceSFX.volume = sliderSFX.value;
    }
    public void VolumeMusic()
    {
        m_audioSourceMusic.volume = sliderMusic.value;
    }
}
