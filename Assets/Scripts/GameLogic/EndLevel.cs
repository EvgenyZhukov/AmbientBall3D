using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefsSavingMethods;

public class EndLevel : MonoBehaviour
{
    [Header("Скрипты")]
    public SaveGameScript saveGameScript;
    public ScriptUI scriptUI;
    public Fader faderMainScript;
    public GameScript gameScript;
    public bool active = false;
    public GameObject player;
    public GameObject explosion;
    public GameObject endLevelEffect;
    private bool enter = false;
    private float moveToExitSpeed = 0.1f;
    private float moveToExitAcceleration = 0.03f;
    private float scaleCangeSpeed = 0.1f;
    private float scaleCangeAcceleration = 0.02f;
    private bool locker = false;
    private float x, y, z;
    private int stars = 0;
    private float starDelay = 0.5f;
    private float winScreenDelay = 0.5f;

    void Start()
    {
        x = player.transform.localScale.x;
        y = player.transform.localScale.y;
        z = player.transform.localScale.z;
    }

    void Update()
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
            moveToExitSpeed += moveToExitAcceleration;
            float step = moveToExitSpeed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, step);

            if (x > 0)
            {
                x -= scaleCangeSpeed * Time.deltaTime;
                y -= scaleCangeSpeed * Time.deltaTime;
                z -= scaleCangeSpeed * Time.deltaTime;
                player.transform.localScale = new Vector3(x, y, z);
                scaleCangeSpeed += scaleCangeAcceleration;
            }

            if (player.transform.position == transform.position && !locker)
            {
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

        if (SaveLoadData.GetContinuousTaken())
        {
            stars = 1;
        }
        else
        {
            if (gameScript.lives < 2)
            {
                stars = 2;
            }
            else if (gameScript.lives >= 2)
            {
                stars = 3;
            }
        }
        SaveLoadData.SetStars(SceneManager.GetActiveScene().buildIndex, stars); // сохранение количества звезд пройденого уровня

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
            case 1:
                scriptUI.gameFinished.text = "level finished! \n not bad! \n 1 star!";
                yield return new WaitForSeconds(starDelay);
                scriptUI.star1.gameObject.SetActive(true);
                yield break;
            case 2:
                scriptUI.gameFinished.text = "level finished! \n good! \n 2 stars!";
                yield return new WaitForSeconds(starDelay);
                scriptUI.star1.gameObject.SetActive(true);
                yield return new WaitForSeconds(starDelay);
                scriptUI.star2.gameObject.SetActive(true);
                yield break;
            case 3:
                scriptUI.gameFinished.text = "level finished! \n great! \n 3 stars!";
                yield return new WaitForSeconds(starDelay);
                scriptUI.star1.gameObject.SetActive(true);
                yield return new WaitForSeconds(starDelay);
                scriptUI.star2.gameObject.SetActive(true);
                yield return new WaitForSeconds(starDelay);
                scriptUI.star3.gameObject.SetActive(true);
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
        scriptUI.gameFinished.transform.gameObject.SetActive(true);  // включение текста
        scriptUI.startNextLevelText.transform.parent.gameObject.SetActive(true);
        scriptUI.endLevelTextOn = true;

        scriptUI.panelObjFader.SetActive(true);
        faderMainScript.fadingAlmost = true;
    }
}