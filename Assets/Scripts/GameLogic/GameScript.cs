using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefsSavingMethods;

public class GameScript : MonoBehaviour
{
    #region Задачи
    /*      
     *      Отредактировать затенение экрана и проявление/сокрытие текста при прохождении уровня
     *      
     *      Баги:
     *      1. не сохраняется поворот камеры при выходе в меню
     *      2. лаг со звездами при прохождении уровня
     *      3. Ошибка с последовательностью исчезновения стрелок в первом уровне (на развилке)
     *      
     *      
     *      
     *      
     *      Глобальные задачи
     *      1. Выполнить все 9 уровней
     *      2. Добавить возможность пополнения жизней просмотром рекламы
     *      3. Выполнить отключение отображения не попадающих в камеру объектов - ?
     *      4. Добавить звуковые эффекты и музыку
     *      
     *      Тесты:
     *      1. Протестировать сохраниение уровня. Возможна ли попытка загрузки при выходе из недоигранного уровня и начала другого нового уровня.
     *      2. Протестировать настройки управления. В том числе при самом первом запуске игры без захода в настройки.
     *      3. Протестировать акселерометр(метод сброса точки отсчета)
     *      4. Протестировать, что будет если закрыть игру при экране проигрыша
     *      5. Протестировать загрузку камеры после проигрыша и рестарта уровня
     *      6. Протестировать механизм уменьшения функционала на начальных уровнях
     *      7. Протестировать механику прохождения уровня на количество звезд
     *      8. Протестировать обрушаемую колонну, выход в меню во время обрушения
     */
    #endregion

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
