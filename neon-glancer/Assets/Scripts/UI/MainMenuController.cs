using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    internal static MainMenuController instance;

    [Header("Options")]
    [SerializeField] GameObject options;

    [Header("Credits")]
    [SerializeField] GameObject credits;

    [Header("Audio Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    void Awake()
    {
        instance = this;

        musicSlider.value = AudioManager.musicVolume;
        sfxSlider.value = AudioManager.sfxVolume;
    }

    void Start()
    {
        AudioManager.instance.PlayMusic(AudioManager.MusicClips.mainMenuMusic);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleOptions()
    {
        if (!options.activeSelf)
        {
            options.SetActive(true);
        }
        else
        {
            options.SetActive(false);
        }
    }

    public void ToggleCredits()
    {
        if (!credits.activeSelf)
        {
            credits.SetActive(true);
        }
        else
        {
            credits.SetActive(false);
        }
    }
}
