using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefsSavingMethods;

public class EndLevel : MonoBehaviour
{
    [Header("Скрипты")]
    public SaveGameScript saveGameScript;
    public ScriptUI scriptUI;
    public FaderNew faderMainScript;
    public GameScript gameScript;
    public bool active = false;
    public GameObject player;
    public GameObject explosion;
    public GameObject endLevelEffect;
    private bool enter = false;
    private float moveToExitSpeed = 0.1f;
    private float moveToExitAcceleration = 0.05f;
    private bool locker = false;
    private int stars = 0;
    private float starDelay = 0.5f;
    private float winScreenDelay = 0.5f;
    private float playerScale = 1;
    public AudioSource starShowSound;
    public AudioSource endLevel;
    private TextVisibilityManager textVisibilityManager;

    private void Start()
    {
        textVisibilityManager = FindObjectOfType<TextVisibilityManager>();

    }
    void FixedUpdate()
    {
        EnterInEndGamePortal();
    }
    /// <summary>
    /// Переход на новый уровень
    /// </summary>
    /// <param name="other">Колайдер модели игрока</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !active)
        {
            GameProgressionSaving();
            SaveLoadData.ResetCoordinates();    // Сбросы временных данных
            SaveLoadData.ResetLives();
            SaveLoadData.ResetTextProgress();
            SaveLoadData.ResetStarsScoreTemp();
            SaveLoadData.ResetStarsScore(SceneManager.GetActiveScene().buildIndex + 1);
            SaveLoadData.SetInProgress(false);
            SaveLoadData.SetInProgressTemp(false);
            SaveLoadData.SetFirstLevelLaunch(true);
            SaveLoadData.DelCamAxisTemp();

            scriptUI.DeactivateUI();

            enter = true;   // активирует режим входа в портал
            player.GetComponent<Rigidbody>().useGravity = false; // выключает гравитацию для модели игрока

            active = true;  // блокировщик повтора метода
        }
    }
    /// <summary>
    /// Вход в портал перехода на следующий уровень
    /// </summary>
    void EnterInEndGamePortal()
    {
        if (enter)
        {
            playerScale = Vector3.Distance(player.transform.position, transform.position);
            if (playerScale > 1)
            {
                playerScale = 1;
            }

            moveToExitSpeed += moveToExitAcceleration;
            float step = moveToExitSpeed;
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, step * Time.deltaTime);

            player.transform.localScale = new Vector3(playerScale, playerScale, playerScale);
            
            if (player.transform.position == transform.position && !locker)
            {
                endLevel.Play();
                Instantiate(explosion, player.transform.position, Quaternion.identity);  // Воспроизводит эффект перехода на следующий уровень
                endLevelEffect.SetActive(false);
                player.gameObject.SetActive(false); // Выключает модель игрока

                StartCoroutine(StarsInitiation());
                Invoke("WinGameScreen", winScreenDelay);

                locker = true;
            }
        }
    }
    /// <summary>
    /// Сохранение прогресса прохождения игры (завершение уровня)
    /// </summary>
    void GameProgressionSaving()
    {
        int maxLvl = SaveLoadData.GetLevelProgress();
        if (maxLvl == gameScript.scene)
        {
            gameScript.scene++;
            SaveLoadData.SetLevelProgress(gameScript.scene);
        }
        else
        {
            gameScript.scene++;
        }
        SaveLoadData.SetScene(gameScript.scene);       // Записывает номер запускаемой сцены следующего уровня

        switch (gameScript.starsScore)
        {
            case 0:
                stars = 0;
                break;
            case 1:
                stars = 1;
                break;
            case 2:
                stars = 2;
                break;
            case 3:
                stars = 3;
                break;
            default:
                stars = gameScript.starsScore;
                break;
        }

        if (SaveLoadData.GetStars(SceneManager.GetActiveScene().buildIndex) < stars)
        {
            SaveLoadData.SetStars(SceneManager.GetActiveScene().buildIndex, stars); // сохранение количества звезд пройденого уровня
        }

        saveGameScript.saving = true;   // сохранение игры в файл
    }
    /// <summary>
    /// Отображение звезд успеха после завершения уровня
    /// </summary>
    /// <returns></returns>
    private IEnumerator StarsInitiation ()
    {
        switch (stars)
        {
            case 0:
                scriptUI.gameFinished.text = "level finished! \n no stars \n were collected.";
                yield break;
            case 1:
                scriptUI.gameFinished.text = "level finished! \n not bad! \n 1 star!";
                yield return new WaitForSeconds(starDelay);
                scriptUI.star1.gameObject.SetActive(true);
                StarSound();
                yield break;
            case 2:
                scriptUI.gameFinished.text = "level finished! \n good! \n 2 stars!";
                yield return new WaitForSeconds(starDelay);
                scriptUI.star1.gameObject.SetActive(true);
                StarSound();
                yield return new WaitForSeconds(starDelay);
                scriptUI.star2.gameObject.SetActive(true);
                StarSound();
                yield break;
            case 3:
                scriptUI.gameFinished.text = "level finished! \n great! \n all 3 stars!";
                yield return new WaitForSeconds(starDelay);
                scriptUI.star1.gameObject.SetActive(true);
                StarSound();
                yield return new WaitForSeconds(starDelay);
                scriptUI.star2.gameObject.SetActive(true);
                StarSound();
                yield return new WaitForSeconds(starDelay);
                scriptUI.star3.gameObject.SetActive(true);
                StarSound();
                yield break;
            default:
                break;
        }
    }
    /// <summary>
    /// Отображение победного экрана
    /// </summary>
    void WinGameScreen()
    {
        textVisibilityManager.HideTextObjects();
        scriptUI.gameFinished.transform.gameObject.SetActive(true);  // включение текста
        scriptUI.startNextLevelText.transform.parent.gameObject.SetActive(true);
        scriptUI.endLevelTextOn = true;

        scriptUI.panelObjFader.SetActive(true);
        faderMainScript.fadingAlmost = true;

        scriptUI.FaderSemiHide();
    }
    void StarSound()
    {
        starShowSound.Play();///звук появления звезды
        starShowSound.pitch += 0.3f;
    }
}