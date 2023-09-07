using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour
{
    internal static WaveController instance;

    public int waveNumber = 0;
    public bool waveActive;

    public bool game_over;
    bool gameIsFinishing;

    [Header("Enemy Spawn")]
    [SerializeField] GameObject enemyPrefab;
    public static List<GameObject> enemyList = new List<GameObject>();

    int enemyAmount;

    bool playSecondMusic;
    bool playThirdMusic;

    void Awake()
    {
        instance = this;

        playSecondMusic = true;
        playThirdMusic = true;
    }

    void Start()
    {
        AudioManager.instance.PlayMusic(AudioManager.MusicClips.gameStartMusic1);
    }

    void Update()
    {

        if (waveActive && enemyList.Count == 0)
        {
            waveActive = false;
            HUDController.instance.nextWaveCounter = 30;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !waveActive && HUDController.instance.nextWaveCounter > 3 && waveNumber != 0)
        {
            SkipBreak();
        }

        if (game_over && !gameIsFinishing)
        {
            if (PlayerStats.instance.health <= 0)
            {
                StartCoroutine(FinishGame(5f));
            }
            else
            {
                StartCoroutine(FinishGame(10f));
            }
        }

        if (waveNumber == 1 && !waveActive && playSecondMusic)
        {
            AudioManager.instance.PlayMusic(AudioManager.MusicClips.gameStartMusic2);
            playSecondMusic = false;
        }

        if (waveNumber == 2 && !waveActive && playThirdMusic)
        {
            AudioManager.instance.musicAudioSource.loop = true;
            AudioManager.instance.PlayMusic(AudioManager.MusicClips.gameStartMusic3);
            playThirdMusic = false;
        }
    }

    public void SpawnWave()
    {
        waveNumber++;
        waveActive = true;

        if (waveNumber < 4)
        {
            enemyAmount = waveNumber * 3;
        }
        else if (waveNumber >= 4 && waveNumber <= 7)
        {
            enemyAmount = waveNumber * 4;
        }
        else
        {
            enemyAmount = 30;
        }

        Vector3 spawnPoint;
        for (int i = 0; i < enemyAmount; i++)
        {
            if (RandomPointOnNavMesh.RandomPoint(Vector3.zero, 35f, out spawnPoint))
            {
                GameObject enemyClone = Instantiate(enemyPrefab, spawnPoint, transform.rotation);
                enemyList.Add(enemyClone);
            }
        }
    }

    void SkipBreak()
    {
        HUDController.instance.nextWaveCounter = 3;
        HUDController.instance.UpdateNextWaveCounter(false);
    }

    IEnumerator FinishGame(float timer)
    {
        gameIsFinishing = true;

        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene("MainMenuScene");
        Cursor.SetCursor(null, new Vector2(16, 16), CursorMode.Auto);
        enemyList.Clear();

        gameIsFinishing = false;
    }

    void OnDestroy()
    {
        if (AudioManager.instance != null)
        {
            if (AudioManager.instance.musicAudioSource.loop)
            {
                AudioManager.instance.musicAudioSource.loop = false;
            }
            AudioManager.instance.musicAudioSource.Stop();
        }
    }
}
