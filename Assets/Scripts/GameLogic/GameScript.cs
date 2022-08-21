using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefsSavingMethods;

public class GameScript : MonoBehaviour
{

    [Header("Скрипты")]
    public ScriptUI scriptUI;
    public Fader faderMainScript;
    public SaveLevelScript SaveLevelScript;

    [Header("Логика игры")]
    public GameObject explosion;
    public int lives;               // Количество жизней
    public GameObject player;
    public int scene;               //Номер сцены текущего уровня
    public bool inProgressTemp = false;
    
    void Start()
    {
        scene = SaveLoadData.GetScene();    // Загружает номер сцены
        lives = SaveLoadData.GetLives();    // Загружает количество жизней

        inProgressTemp = SaveLoadData.GetInProgressTemp();

        float x, y, z;
            if (inProgressTemp)
            {
                SaveLoadData.LoadCoordinatesTemp(out x, out y, out z);
                inProgressTemp = false;
                SaveLoadData.SetInProgressTemp(inProgressTemp);
            }
            else
            {
                SaveLoadData.LoadCoordinates(out x, out y, out z);
            }
            // Если есть жизни то загружает на чекпоинте
            player.transform.position = new Vector3(x, y, z);
    }

    /// <summary>
    /// Границы игровой области
    /// </summary>
    /// <param name="other">Коллайдер того что вышло из границ</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag =="Player")
        {
            Instantiate(explosion, other.transform.position, Quaternion.identity);  // При выходе из границ взрыв
            other.gameObject.SetActive(false); // Выключает модель игрока

            lives -= 1;                             // Отнимает жизнь
            SaveLoadData.SetLives(lives);     // Запоминает количество жизней
            SaveLoadData.SetCheckpoitTextSaving(false);

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
    }

    private void Defeat()
    {
        scriptUI.defeatCanvas.SetActive(true);
        scriptUI.pauseTextOn = true;
        scriptUI.Invoke("Pause", 0.6f);
        scriptUI.Invoke("DeactivateUI", 0.6f);
    }
}
