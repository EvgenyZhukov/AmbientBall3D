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
    private bool enter = false;
    private float moveToExitSpeed = 0.1f;
    private float scaleSpeed = 0.1f;
    private bool locker = false;
    private float x, y, z;
    private int stars;
    private float starDelay = 0.03f;

    private void Start()
    {
        x = player.transform.localScale.x;
        y = player.transform.localScale.y;
        z = player.transform.localScale.z;
    }

    /// <summary>
    /// Метод переключения уровня
    /// </summary>
    /// <param name="other">Триггер финиша</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !active)
        {
            scriptUI.gameFinished.transform.gameObject.SetActive(true);  // включение текста
            scriptUI.endLevelTextOn = true;

            enter = true;

            player.GetComponent<Rigidbody>().useGravity = false;

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

            
            SaveLoadData.SetStars(SceneManager.GetActiveScene().buildIndex, stars);

            SaveLoadData.ResetCoordinates();
            SaveLoadData.ResetLives();
            SaveLoadData.ResetTextProgress();

            SaveLoadData.SetScene(gameScript.scene);       // Записывает номер запускаемой сцены следующего уровня
            saveGameScript.saving = true;

            Invoke("Fade", 0.8f);
            //Invoke("NextLevel", 2f);  // Следующий уровень

            active = true;
        }
    }

    void Update()
    {
        if (enter)
        {
            moveToExitSpeed += 0.03f;
            float step = moveToExitSpeed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, step);
            
            if (x > 0)
            {
                x -= scaleSpeed * Time.deltaTime;
                y -= scaleSpeed * Time.deltaTime;
                z -= scaleSpeed * Time.deltaTime;
                player.transform.localScale = new Vector3(x, y, z);
                scaleSpeed += 0.02f;
            }
            
            if (player.transform.position == transform.position && !locker)
            {
                Instantiate(explosion, player.transform.position, Quaternion.identity);  // Воспроизводит эффект перехода на следующий уровень

                player.gameObject.SetActive(false); // Выключает модель игрока
                
                WinScreen();
                locker = true;
            }
        }
    }

    /// <summary>
    /// Метод загрузки следующего уровня
    /// </summary>
    void NextLevel()
    {
        SceneManager.LoadScene(gameScript.scene);
    }
    void Fade()
    {
        scriptUI.panelObjFader.SetActive(true);
        faderMainScript.fading = true;
    }

    void WinScreen()
    {
        if (SaveLoadData.GetContinuousTaken())
        {
            stars = 1;
            scriptUI.gameFinished.text = "level finished! \n not bad! \n 1 star!";
            Invoke("OnStar_1", starDelay);
        }
        else
        {
            if (gameScript.lives >= 2)
            {
                stars = 3;
                scriptUI.gameFinished.text = "level finished! \n great! \n 3 stars!";
                Invoke("OnStar_1", starDelay);
                Invoke("OnStar_2", starDelay * 2);
                Invoke("OnStar_3", starDelay * 3);
            }
            else if (gameScript.lives < 2)
            {
                stars = 2;
                scriptUI.gameFinished.text = "level finished! \n good! \n 2 stars!";
                Invoke("OnStar_1", starDelay);
                Invoke("OnStar_2", starDelay * 2);
            }
        }
    }

    void OnStar_1()
    {
        scriptUI.star1.gameObject.SetActive(true);
    }
    void OnStar_2()
    {
        scriptUI.star2.gameObject.SetActive(true);
    }
    void OnStar_3()
    {
        scriptUI.star3.gameObject.SetActive(true);
    }
}