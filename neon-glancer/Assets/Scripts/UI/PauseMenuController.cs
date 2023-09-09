using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    internal static PauseMenuController instance;

    [SerializeField] Canvas pauseMenuScreen;

    public static bool gamePaused;

    [Header("Audio Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    void Awake()
    {
        instance = this;

        gamePaused = false;
        pauseMenuScreen.enabled = false;
        Time.timeScale = 1;

        musicSlider.onValueChanged.AddListener(delegate { AudioManager.instance.SetMusic(); });
        sfxSlider.onValueChanged.AddListener(delegate { AudioManager.instance.SetSFX(); });

        musicSlider.value = AudioManager.musicVolume;
        sfxSlider.value = AudioManager.sfxVolume;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerMovement.instance.canRotate && !WaveController.instance.game_over)
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (gamePaused)
        {
            gamePaused = false;
            pauseMenuScreen.enabled = false;
            Cursor.SetCursor(HUDController.instance.battleCursor, new Vector2(16, 48), CursorMode.Auto);
            Time.timeScale = 1;
        }
        else
        {
            gamePaused = true;
            pauseMenuScreen.enabled = true;
            Cursor.SetCursor(null, new Vector2(16, 16), CursorMode.Auto);
            Time.timeScale = 0;
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
