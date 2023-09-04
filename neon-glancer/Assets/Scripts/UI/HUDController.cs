using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    internal static HUDController instance;

    [SerializeField] Material HUDMaterial;
    [SerializeField] FadeMaterial FadeOutNextWaveText;

    [Header("Health Bar")]
    [SerializeField] Image healthBar;

    [Header("Cursor")]
    public Texture2D battleCursor;

    [Header("Waves")]
    [SerializeField] Text waveCounter;
    [SerializeField] Text nextWaveText;
    [SerializeField] Text nextWaveHint;

    [Header("Interact Hint")]
    public Text interactHint;

    [Header("Neon Counter")]
    [SerializeField] Text neonText;

    [Header("Announcement Text")]
    [SerializeField] Text AnnouncementText;

    [Header("Canvas")]
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject neonCanvas;
    [SerializeField] GameObject endGameCanvas;

    [HideInInspector] public int nextWaveCounter;
    bool updatingNextWaveCounter;

    void Awake()
    {
        instance = this;

        Cursor.SetCursor(HUDController.instance.battleCursor, new Vector2(16, 48), CursorMode.Auto);
    }

    void Start()
    {
        nextWaveCounter = 10;

        HUDMaterial.color = new Color(HUDMaterial.color.r, HUDMaterial.color.g, HUDMaterial.color.b, 0f);
        FadeOutNextWaveText = new FadeMaterial();

        UpdateNeonText();
    }

    void Update()
    {
        if (WaveController.instance.waveNumber == 10 && !WaveController.instance.waveActive)
        {
            mainCanvas.SetActive(false);
            neonCanvas.SetActive(false);
            endGameCanvas.SetActive(true);
            WaveController.instance.game_over = true;
        }

        // Update Announcement text
        if (WaveController.instance.waveNumber == 1 && !WaveController.instance.waveActive)
        {
            UpdateAnnouncementText("Upgrade Shop available! (left side)");
        }
        else if (WaveController.instance.waveNumber == 9 && !WaveController.instance.waveActive)
        {
            AnnouncementText.enabled = true;
            UpdateAnnouncementText("Prepare for last wave!");
        }

        // Fade in and out next wave timer and announcement text
        if (!FadeOutNextWaveText.fadingOut && !WaveController.instance.waveActive)
        {
            StartCoroutine(FadeOutNextWaveText.FadeOut(HUDMaterial));
        }
        /*else if (HUDMaterial.color.a == 1)
        {
            FadeOutNextWaveText = null;
        }*/
        else if (!FadeOutNextWaveText.fadingIn && WaveController.instance.waveActive)
        {
            StartCoroutine(FadeOutNextWaveText.FadeIn(HUDMaterial));
        }

        // Update next wave timer
        if (!WaveController.instance.waveActive && !updatingNextWaveCounter)
        {
            StartCoroutine(UpdateNextWaveText());
        }

        // Handle next wave hint visibility
        if (nextWaveCounter <= 3)
        {
            ToggleNextWaveHint(false);
        }
        else if (!WaveController.instance.waveActive && !nextWaveHint.enabled && WaveController.instance.waveNumber > 0)
        {
            ToggleNextWaveHint(true);
        }
        else if (WaveController.instance.waveActive && nextWaveHint.enabled)
        {
            ToggleNextWaveHint(false);
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)PlayerStats.instance.health / 100;
    }

    void UpdateWaveCounter()
    {
        waveCounter.text = $"Wave {WaveController.instance.waveNumber}";
    }

    public void ToggleNextWaveHint(bool toggle)
    {
        if (nextWaveHint.enabled != toggle)
        {
            nextWaveHint.enabled = toggle;
            nextWaveHint.GetComponentInParent<SineFadeMaterial>(true).enabled = toggle;
        }
    }

    public void UpdateNextWaveCounter(bool firstWave)
    {
        if (!firstWave)
        {
            nextWaveText.text = $"Next Wave in {nextWaveCounter}";
        }
        else
        {
            nextWaveText.text = $"Get ready!   {nextWaveCounter}";
        }
    }

    public void UpdateNeonText()
    {
        neonText.text = $"{PlayerStats.instance.neon}";
    }

    void UpdateAnnouncementText(string announcement)
    {
        AnnouncementText.text = announcement;
    }

    IEnumerator UpdateNextWaveText()
    {
        updatingNextWaveCounter = true;

        if (WaveController.instance.waveNumber != 0)
        {
            UpdateNextWaveCounter(false);

            if (!nextWaveText.enabled)
            {
                nextWaveText.enabled = true;
            }
        }
        else
        {
            UpdateNextWaveCounter(true);

            if (nextWaveCounter == 10)
            {
                yield return new WaitForSeconds(1f);
            }
        }

        if (nextWaveCounter > 0)
        {
            yield return new WaitForSeconds(1f);
            nextWaveCounter--;
        }
        else
        {
            yield return new WaitForSeconds(1f);

            WaveController.instance.SpawnWave();
            UpdateWaveCounter();
        }

        updatingNextWaveCounter = false;
    }
}