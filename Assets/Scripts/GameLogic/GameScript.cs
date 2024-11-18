using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefsSavingMethods;

public class GameScript : MonoBehaviour
{

    [Header("Скрипты")]
    public ScriptUI scriptUI;
    public FaderNew faderMainScript;
    public SaveLevelScript SaveLevelScript;
    public SaveGameScript saveGameScript;
    public LevelTextScript levelTextScript;
    public AudioScript audioScript;
    private TextVisibilityManager textVisibilityManager;
    private InterstitialAd interstitialAd;
    private BackgroundMusicManager backgroundMusicManager;

    [Header("Логика игры")]
    public GameObject explosion;
    public int lives;               // Количество жизней
    public GameObject player;
    public int scene;               //Номер сцены текущего уровня
    public bool inProgressTemp = false;
    public int starsScore = 0;

    private void Awake()
    {
        audioScript = FindObjectOfType<AudioScript>();
        saveGameScript = FindObjectOfType<SaveGameScript>();
        levelTextScript = FindObjectOfType<LevelTextScript>();
        textVisibilityManager = FindObjectOfType<TextVisibilityManager>();
        interstitialAd = FindObjectOfType<InterstitialAd>();
        backgroundMusicManager = FindObjectOfType<BackgroundMusicManager>();
    }
    void Start()
    {
        //int currentFrame = Time.frameCount;
        //Debug.Log("Current Frame: " + currentFrame);

        backgroundMusicManager.audioSource.time = SaveLoadData.GetMusicTime();

        scene = SaveLoadData.GetScene();    // Загружает номер сцены
        lives = SaveLoadData.GetLives();    // Загружает количество жизней
        

        inProgressTemp = SaveLoadData.GetInProgressTemp();

        float x, y, z;

        if (scene == 9)
        {
            SaveLoadData.LoadCoordinates(out x, out y, out z);


            if (inProgressTemp)
            {
                starsScore = SaveLoadData.GetEndlessScoreTemp();
            }
            else
            {
                starsScore = SaveLoadData.GetStarsEndlessMode();
                z = starsScore * 50;
            }
            lives = 0;
        }
        else
        {
            if (inProgressTemp)
            {
                SaveLoadData.LoadCoordinatesTemp(out x, out y, out z);
                inProgressTemp = false;
                SaveLoadData.SetInProgressTemp(inProgressTemp);
                starsScore = SaveLoadData.GetStarsScoreTemp();
                SaveLoadData.ResetStarsScoreTemp();
            }
            else
            {
                SaveLoadData.LoadCoordinates(out x, out y, out z);
                starsScore = SaveLoadData.GetStarsScore(SceneManager.GetActiveScene().buildIndex);
            }
        }
            
            // Если есть жизни то загружает на чекпоинте
            player.transform.position = new Vector3(x, y, z);

        if (lives < 0)
        {
            if (!interstitialAd.IsAdLoaded())
            {
                // Реклама не загружена, выполняем загрузку
                interstitialAd.LoadAd();
            }
            if (levelTextScript != null)
            {
                levelTextScript.textOnLaunch = false;
            }
            player.gameObject.SetActive(false);
            Invoke("Fade", 1.2f);  // переход
            Invoke("Defeat", 1.2f);
        }

        faderMainScript.brighten = true;

        Resources.UnloadUnusedAssets();
    }
    /// <summary>
    /// Границы игровой области
    /// </summary>
    /// <param name="other">Коллайдер того что вышло из границ</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
        {
            KillPlayer();
        }
    }
    void KillPlayer ()
    {
        Instantiate(explosion, player.transform.position, Quaternion.identity);  // При выходе из границ взрыв
        audioScript.defeatSound.Play();
        player.gameObject.SetActive(false); // Выключает модель игрока
        if (!interstitialAd.IsAdLoaded())
        {
            // Реклама не загружена, выполняем загрузку
            interstitialAd.LoadAd();
        }
        lives -= 1;                             // Отнимает жизнь
        SaveLoadData.SetLives(lives);     // Запоминает количество жизней
        saveGameScript.saving = true;
        scriptUI.FaderFullHide();
        SaveLoadData.SetMusicTime(backgroundMusicManager.audioSource.time);

        Invoke("Fade", 0.8f);  // переход
        if (lives >= 0)
        {
            Invoke("Restart", 2f);  // Перезапуск сцены
        }
        else
        {
            Invoke("Defeat", 0.8f);
        }
    }
    /// <summary>
    /// Метод перезапуска уровня
    /// </summary>
    private void Restart()
    {
        SceneManager.LoadScene(scene);
    }

    private void Fade()
    {
        scriptUI.panelObjFader.SetActive(true);
        faderMainScript.fading = true;
        audioScript.FadeOut();
    }

    private void Defeat()
    {
        textVisibilityManager.HideTextObjects();
        scriptUI.defeatCanvas.SetActive(true);
        scriptUI.pauseTextOn = true;
        scriptUI.Invoke("Pause", 0.6f);
        scriptUI.Invoke("DeactivateUI", 0.6f);
        scriptUI.FaderSemiHide();
    }
}