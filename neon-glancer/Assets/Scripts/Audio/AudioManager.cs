using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    internal static AudioManager instance;

    public enum MusicClips
    {
        mainMenuMusic,
        gameStartMusic1,
        gameStartMusic2,
        gameStartMusic3
    }

    public enum SoundEffects
    {
        buttonSelect,
        buttonHighlight,
        defaultBlaster,
        upgradedBlaster,
        enemyBlaster1,
        enemyBlaster2,
        enemyBlaster3,
        upgradeAuto,
        upgradeMovement,
        upgradeSideBlasters,
        upgradeDamage,
        upgradeRate,
        upgradeProjectile,
        buyMedKit,
        useMedKit,
        breakArmor
    }

    [Header("Music")]
    public AudioSource musicAudioSource;
    [SerializeField] AudioClip[] musicAudioClips;

    [Header("SFX")]
    [SerializeField] AudioSource[] sfxAudioSources;

    public static float musicVolume = 0.5f;
    public static float sfxVolume = 0.5f;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetMusic();
        SetSFX();
    }

    public void PlayMusic(MusicClips musicClip)
    {
        musicAudioSource.clip = musicAudioClips[(int)musicClip];
        musicAudioSource.Play();
    }

    public void PlaySFX(SoundEffects soundEffects)
    {        
        sfxAudioSources[(int)soundEffects].PlayOneShot(instance.sfxAudioSources[(int)soundEffects].clip);
    }

    public void SetMusic()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            musicVolume = MainMenuController.instance.musicSlider.value;
            musicAudioSource.volume = musicVolume;
        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            musicVolume = PauseMenuController.instance.musicSlider.value;
            musicAudioSource.volume = musicVolume;
        }
    }

    public void SetSFX()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            foreach (AudioSource val in sfxAudioSources)
            {
                sfxVolume = MainMenuController.instance.sfxSlider.value;
                val.volume = sfxVolume;
            }
        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            foreach (AudioSource val in sfxAudioSources)
            {
                sfxVolume = PauseMenuController.instance.sfxSlider.value;
                val.volume = sfxVolume;
            }
        }
    }
}
