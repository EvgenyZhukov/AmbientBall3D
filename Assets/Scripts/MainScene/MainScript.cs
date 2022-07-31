using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using PlayerPrefsSavingMethods;
using System;

/// <summary>
/// Скрипт управления в меню, перемещает камеру. 
/// </summary>
public class MainScript : MonoBehaviour
{

    [Header("Скрипты")]
    public AudioScript audioScript;
    public SaveSettingsScript saveSettingsScript;
    public SaveGameScript saveGameScript;

    //Фиксированные позиции камеры в различных точках
    private float mainCamPosY = 56.5f;
    private float mainCamPosZ = -56;
    private float mainCamRotX = 52.7f;
    private float lvlCamPosY = 40;
    private float lvlCamPosZ = -82.55f;
    private float lvlCamRotX = 90;
    private float optionCamPosY = 76;
    private float optionCamPosZ = -20;
    private float optionCamRotX = 90;
    //Фиксированные коэфициенты скорости перемещения камеры
    private float camAccel = 4;
    private float masterSpeedPosY = 2;
    private float lvlSpeedPosZ = 3.2f;
    private float optionSpeedPosZ = 4.5f;
    private float lvlSpeedRotX = 3.7f;
    private float optionSpeedRotX = 3.82f;
    private float tempCamSpeed = 0;
    private int pressedButton = 0; //Номер нажатой кнопки навигации в меню
    [Header("Объекты сцены")]
    public GameObject cam;
    public GameObject main;
    public GameObject levels;
    public GameObject options;
    [Header("Кнопки")]
    public Button lineOptionsButton;
    public Button linePlayGameButton;
    public Button lineReturnInGameButton;
    public Button lineChangeLevelButton;
    public Button lineNoAdsButton;
    [Header("Переменные для работы с текстом")]
    public TMP_Text soundStatus;
    public TMP_Text musicStatus;
    public TMP_Text accelerometerText;
    public TMP_Text accelerometerTextOffOn;
    public TMP_Text controlTypeText;
    public TMP_Text starsNumber;
    public Material mainText;
    private Color turquoise = new Color(0, 241, 255, 255);
    private float glowPower;
    public float glowSpeed;
    public float glowMax;
    public float glowMin;
    public GameObject linePlayGame;
    public GameObject lineReturnInGame;
    public GameObject lineChangeLevel;
    public GameObject lineNoAds;
    public GameObject blocker;
    [Header("Настройки звука")]
    public AudioMixer MasterMixer;
    public AudioSource volumeChangeSound;
    public AudioSource errorSound;
    public AudioSource clickSound;
    public AudioSource camMoveSound;
    public Slider musicButtonSlider;
    public Slider soundButtonSlider;
    public float volumeStep = 3f;   //Цена деления при регулировке звука/музыки
    public bool soundMuted = false;
    public bool musicMuted = false;
    public float soundVolume = -20f;
    public float musicVolume = -20f;
    [Header("Настройки управления")]
    public bool accelerometerActive = false;
    public bool leftHandedControl = false;
    [Header("Переключение сцен")]
    public Fader faderMain;
    public GameObject panelObj;
    private float lvlStartTime = 1.2f;
    private bool inProgress = false;
    private bool inProgressTemp = false;
    public int scene;
    public int currentLvl;
    public bool levelSelected = false;
    public int starsTotal;


    void Start()
    {
        //PlayerPrefs.DeleteAll();
        faderMain.brighten = true;
        audioScript.fadeIn = true;

        EnableButtons();

        ProgressCheck();
        
        OptionsLoader();

    }

    void Update()
    {
        MainNavigation();
        TextGlow();
    }

    #region Управление в меню
    /// <summary>
    /// Выполняет навигацию по меню и перемещение поворотом камеры
    /// </summary>
    private void MainNavigation()
    {
        switch (pressedButton)
        {
            case 0:
                break;
            case 1:
                if (cam.transform.position.y > lvlCamPosY)
                {
                    if (tempCamSpeed == 0) LevelsOn();
                    MoveCam(-masterSpeedPosY, -lvlSpeedPosZ, optionSpeedPosZ);
                }
                else if (cam.transform.position.y <= lvlCamPosY)
                {
                    StopCam(lvlCamPosY, lvlCamPosZ, lvlCamRotX);
                    EnableButtons();
                    MainOff();
                }
                break;
            case 2:
                if (cam.transform.position.y < mainCamPosY)
                {
                    if (tempCamSpeed == 0) MainOn();
                    MoveCam(masterSpeedPosY, lvlSpeedPosZ, -optionSpeedPosZ);
                }
                else if(cam.transform.position.y >= mainCamPosY)
                {
                    StopCam(mainCamPosY, mainCamPosZ, mainCamRotX);
                    EnableButtons();
                    LevelsOff();
                }
                break;
            case 3:
                if (cam.transform.position.y < optionCamPosY)
                {
                    if (tempCamSpeed == 0) OptionsOn();
                    MoveCam(masterSpeedPosY, lvlSpeedRotX, optionSpeedRotX);
                }
                else if (cam.transform.position.y >= optionCamPosY)
                {
                    StopCam(optionCamPosY, optionCamPosZ, optionCamRotX);
                    EnableButtons();
                    MainOff();
                }
                break;
            case 4:
                if (cam.transform.position.y > mainCamPosY)
                {
                    if (tempCamSpeed == 0) MainOn();
                    MoveCam(-masterSpeedPosY, -lvlSpeedRotX, -optionSpeedRotX);
                }
                else if (cam.transform.position.y <= mainCamPosY)
                {
                    StopCam(mainCamPosY, mainCamPosZ, mainCamRotX);
                    EnableButtons();
                    OptionsOff();
                }
                break;
            case 5:

                break;
        }
    }

    /// <summary>
    /// Останавливает камеру в заданных координатах и сбрасывает выбор действия
    /// </summary>
    private void StopCam(float yP, float zP, float xR)
    {
        cam.transform.position = new Vector3(24, yP, zP);
        cam.transform.rotation = Quaternion.Euler(xR, 0, -90);
        pressedButton = 0;
        tempCamSpeed = 0;
    }
    /// <summary>
    /// Передвигает и поворачивает камеру
    /// </summary>
    private void MoveCam(float yP, float zP, float xR)
    {
        tempCamSpeed += camAccel * Time.deltaTime;
        cam.transform.position += new Vector3(0, yP * tempCamSpeed * Time.deltaTime, zP * tempCamSpeed * Time.deltaTime);
        cam.transform.Rotate(new Vector3(0, xR * tempCamSpeed * Time.deltaTime, 0));
    }
    #endregion

    #region Сокрытие элементов меню
    /// <summary>
    /// Отключает отображение меню
    /// </summary>
    private void MainOff()
    {
        main.gameObject.SetActive(false);
    }
    /// <summary>
    /// Включает отображение меню
    /// </summary>
    private void MainOn()
    {
        main.gameObject.SetActive(true);
    }
    /// <summary>
    /// Отключает отображение перечня уровней
    /// </summary>
    private void LevelsOff()
    {
        levels.gameObject.SetActive(false);
    }
    /// <summary>
    /// Включает отображение перечня уровней
    /// </summary>
    private void LevelsOn()
    {
        levels.gameObject.SetActive(true);
    }
    /// <summary>
    /// Отключает отображение настроек
    /// </summary>
    private void OptionsOff()
    {
        options.gameObject.SetActive(false);
    }
    /// <summary>
    /// Включает отображение настроек
    /// </summary>
    private void OptionsOn()
    {
        options.gameObject.SetActive(true);
    }
    #endregion

    #region Управление текстом
    /// <summary>
    /// Пульсирующее свечение текста изменением свойств материала
    /// </summary>
    private void TextGlow()
    {
        mainText.SetFloat("_GlowPower", glowPower);
        glowPower = Mathf.Sin(Time.time * glowSpeed) * glowMax;

        if (glowPower < glowMin)
        {
            glowPower = glowMin;
        }
    }
    private void SoundTextChange()
    {
        soundStatus.color = soundMuted ? Color.red : turquoise;
    }
    private void MusicTextChange()
    {
        musicStatus.color = musicMuted ? Color.red : turquoise;
    }
    private void AccelerometerTextChange()
    {
        accelerometerText.color = accelerometerActive ? turquoise : Color.red;
        accelerometerTextOffOn.color = accelerometerActive ? turquoise : Color.red;
        accelerometerTextOffOn.text = accelerometerActive ? "ON" : "OFF";
    }
    private void ControlTextChange()
    {
        controlTypeText.text = leftHandedControl ? "LEFT-HANDED" : "RIGHT-HANDED";
    }
    #endregion

    #region Кнопки навигиции по меню
    /// <summary>
    /// Переход из меню к выбору уровня
    /// </summary>
    public void ButtonMainToLevels()
    {
        pressedButton = 1;
        DisableButtons();
    }
    /// <summary>
    /// Переход от выбора уровня к меню
    /// </summary>
    public void ButtonLevelsToMain()
    {
        pressedButton = 2;
        DisableButtons();
    }
    /// <summary>
    /// Переход от меню к настройкам
    /// </summary>
    public void ButtonMainToOptions()
    {
        pressedButton = 3;
        DisableButtons();
    }
    /// <summary>
    /// Переход от настроек к меню
    /// </summary>
    public void ButtonOptionsToMain()
    {
        pressedButton = 4;
        DisableButtons();
        OptionsSaver();
    }
    /// <summary>
    /// Переход к покупке отключения рекламы
    /// </summary>
    public void ButtonNoAds()
    {
        DisableButtons();
        /////////////////////////////////////////////////////no ads
    }
    /*
    public void ButtonHelp()
    {
        //reserved
    }
    */
    /// <summary>
    /// Возврат к игре
    /// </summary>
    public void ButtonReturnInGame()
    {
        audioScript.fadeOut = true;
        pressedButton = 5;
        DisableButtons();
        Fade();
        Invoke("StartLevel", lvlStartTime);

    }

    #endregion

    #region Кнопки запуска уровней логика запуска уровней
    public void StartGame()
    {
        levelSelected = true;
        DisableButtons();
        audioScript.fadeOut = true;
        SaveLoadData.SetInProgressTemp(false);
        SaveLoadData.SetInProgress(false);
        SaveLoadData.SetFirstLevelLaunch(true);
        SaveLoadData.ResetCoordinates();
        SaveLoadData.ResetLives();
        SaveLoadData.ResetTextProgress();
        SaveLoadData.DelCamAxisTemp();
        SaveLoadData.SetScene(scene);
        Fade();
        Invoke("StartLevel", lvlStartTime);
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(scene);
    }
    private void Fade()
    {
        panelObj.SetActive(true);
        faderMain.fading = true;
    }
    private void ProgressCheck()
    {
        starsTotal = SaveLoadData.GetStarsTotal();
        starsNumber.text = Convert.ToString(starsTotal);
        inProgress = SaveLoadData.GetInProgress();
        inProgressTemp = SaveLoadData.GetInProgressTemp();
        currentLvl = SaveLoadData.GetLevelProgress();
        if (currentLvl == 0)
        {
            currentLvl = 1;
            SaveLoadData.SetLevelProgress(currentLvl);
        }

        switch (inProgress || inProgressTemp)
        {
            case true:
                //Включает режим "продолжить игру"
                scene = SaveLoadData.GetScene();
                linePlayGame.SetActive(false);
                lineReturnInGame.SetActive(true);
                lineChangeLevel.SetActive(true);
                break;
            default:
                //Выключает режим "продолжить игру"
                SaveLoadData.ResetScene();
                linePlayGame.SetActive(true);
                lineReturnInGame.SetActive(false);
                lineChangeLevel.SetActive(false);
                break;
        }
    }
    public void DisableButtons()
    {
        lineOptionsButton.interactable = false;
        linePlayGameButton.interactable = false;
        lineReturnInGameButton.interactable = false;
        lineChangeLevelButton.interactable = false;
        lineNoAdsButton.interactable = false;
        
        blocker.SetActive(true);
    }
    public void EnableButtons()
    {
        lineOptionsButton.interactable = true;
        linePlayGameButton.interactable = true;
        lineReturnInGameButton.interactable = true;
        lineChangeLevelButton.interactable = true;
        lineNoAdsButton.interactable = true;
        
        blocker.SetActive(false);
    }
    #endregion

    #region Настройка звука
    /// <summary>
    /// Увеличивает громкость звука при нажатии кнопик +
    /// </summary>
    public void SoundMax()
    {
        soundButtonSlider.value += volumeStep;
        MasterMixer.SetFloat("soundVolume", soundButtonSlider.value);
        soundVolume = soundButtonSlider.value;
        soundMuted = false;
        volumeChangeSound.Play();
        SoundTextChange();
    }
    /// <summary>
    /// Уменьшает громкость звука при нажатии кнопик -
    /// </summary>
    public void SoundMin()
    {
        soundButtonSlider.value -= volumeStep;
        MasterMixer.SetFloat("soundVolume", soundButtonSlider.value);
        soundVolume = soundButtonSlider.value;
        if (soundButtonSlider.value == -40f)
        {
            MasterMixer.SetFloat("soundVolume", -80f);
            soundVolume = soundButtonSlider.value;
            soundMuted = true;
        }
        volumeChangeSound.Play();
        SoundTextChange();
    }
    /// <summary>
    /// Увеличивает громкость музыки при нажатии кнопик +
    /// </summary>
    public void MusicMax()
    {
        musicButtonSlider.value += volumeStep;
        MasterMixer.SetFloat("musicVolume", musicButtonSlider.value);
        musicVolume = musicButtonSlider.value;
        musicMuted = false;
        MusicTextChange();
    }
    /// <summary>
    /// Уменьшает громкость музыки при нажатии кнопик -
    /// </summary>
    public void MusicMin()
    {
        musicButtonSlider.value -= volumeStep;
        MasterMixer.SetFloat("musicVolume", musicButtonSlider.value);
        musicVolume = musicButtonSlider.value;
        if (musicButtonSlider.value == -40f)
        {
            MasterMixer.SetFloat("musicVolume", -80f);
            musicVolume = musicButtonSlider.value;
            musicMuted = true;
        }
        MusicTextChange();
    }
    /// <summary>
    /// Меняет громкость звука при удержании ползунка
    /// </summary>
    public void SoundChangeMode()
    {
        if (soundButtonSlider.value == -40f && !soundMuted)
        {
            MasterMixer.SetFloat("soundVolume", -80f);
            soundVolume = soundButtonSlider.value;
            soundMuted = true;
        }
        else if (soundButtonSlider.value != -40f)
        {
            soundMuted = false;
            MasterMixer.SetFloat("soundVolume", soundButtonSlider.value);
            soundVolume = soundButtonSlider.value;
        }
        SoundTextChange();
    }
    /// <summary>
    /// Меняет громкость музыки при удержании ползунка
    /// </summary>
    public void MusicChangeMode()
    {
        if (musicButtonSlider.value == -40f && !musicMuted)
        {
            MasterMixer.SetFloat("musicVolume", -80f);
            musicVolume = musicButtonSlider.value;
            musicMuted = true;
        }
        else if (musicButtonSlider.value != -40f)
        {
            musicMuted = false;
            MasterMixer.SetFloat("musicVolume", musicButtonSlider.value);
            musicVolume = musicButtonSlider.value;
        }
        MusicTextChange();
    }
    public void SoundChangeSignal()
    {
        volumeChangeSound.Play();
    }
    #endregion

    #region Настройка управления

    public void ButtonAccelerometer()
    {
        accelerometerActive = !accelerometerActive;
        AccelerometerTextChange();
    }

    public void ButtonControl()
    {
        leftHandedControl = !leftHandedControl;
        ControlTextChange();
    }
    #endregion

    #region Загрузка и сохранение всех настроек
    public void OptionsLoader()
    {
            if (SaveLoadData.GetOptionsDataChecker())
            {
                SaveLoadData.GetOptions(out soundVolume, out musicVolume, out soundMuted, out musicMuted, out accelerometerActive, out leftHandedControl);

                MasterMixer.SetFloat("soundVolume", soundVolume);
                MasterMixer.SetFloat("musicVolume", musicVolume);

                //Установить положение ползунков и кнопок регулировки звука, режим настройки управления

                soundButtonSlider.value = soundVolume;
                musicButtonSlider.value = musicVolume;
            }
        
        SoundTextChange();
        MusicTextChange();
        AccelerometerTextChange();
        ControlTextChange();
    }
    private void OptionsSaver()
    {
        SaveLoadData.SetOptions(soundVolume, musicVolume, soundMuted, musicMuted, accelerometerActive, leftHandedControl);
        SaveLoadData.SetOptionsDataChecker(true);
        SaveLoadData.SetFirstLevelLaunch(true);//для того что бы показать изменившиеся настройки при возвращении в игру
        saveSettingsScript.saving = true;
    }
    #endregion
}