using System.Collections;
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
    public AudioScriptMain audioScriptMain;
    public SaveSettingsScript saveSettingsScript;
    //public SaveGameScript saveGameScript;

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
    private float camAccel = 3;
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
    public Button lineLevelsButton;
    public Button lineEndlessGameButton;
    public Button lineOptionsButton;
    public Button lineReturnInGameButton;

    public Button lineDeleteGameProgress;
    [Header("Переменные для работы с текстом")]
    public TMP_Text soundStatus;
    public TMP_Text musicStatus;
    public TMP_Text accelerometerText;
    //public TMP_Text graphicsQualityText;
    public TMP_Text controlTypeText;
    public TMP_Text starsNumber;
    public TMP_Text nextLevelsButtonToEpisode2;
    public TMP_Text deleteGame; //line0

    public TMP_Text textAmbient; // line1
    public TMP_Text textBall; // line2

    public TMP_Text textLevels; //line1
    public TMP_Text textEndlessGame; //line2
    public TMP_Text textOptions; //line3
    public TMP_Text textReturnInGame; //line4

    public TMP_Text starsRecordEndlessLevel;
    public Material mainText;
    private Color turquoise = new Color(0, 241, 255, 255);
    private float glowPower;
    public float glowSpeed;
    public float glowMax;
    public float glowMin;
    public GameObject Levels;
    public GameObject EndlessGame;
    public GameObject Options;
    public GameObject ReturnInGame;

    public GameObject lineAmbient;
    public GameObject lineBall;
    public GameObject blocker;
    [Header("Настройки графики")]
    public bool graphicsHigh = true;
    [Header("Настройки звука")]
    public AudioMixer MasterMixer;
    public Slider musicButtonSlider;
    public Slider soundButtonSlider;
    public float volumeStep = 3f;   //Цена деления при регулировке звука/музыки
    public bool soundMuted = false;
    public bool musicMuted = false;
    public float soundVolume = 0f;
    public float musicVolume = 0f;
    [Header("Настройки управления")]
    public bool accelerometerActive = false;
    public bool leftHandedControl = false;
    [Header("Переключение сцен")]
    public FaderNewMain faderMainScript;
    public GameObject panelObj;
    private float lvlStartTime = 1.2f;
    private bool inProgress = false;
    private bool inProgressTemp = false;
    public int scene;
    public int currentLvl;
    public bool levelSelected = false;
    public int starsTotal;
    public int starsEndlessModeTotal;
    public GameObject needMoreStarsToEpisode2;
    //public GameObject needMoreStarsToEndlessMode;
    public GameObject nextToEpisode2;
    //public GameObject endlessModeButton;
    //public GameObject endlessModeTextActive;
    public bool confirmDeleteGameProgress = false;
    public bool gameNameVisible = false;
    public bool startGameVisible = true;
    private float textFadeSpeed = 0.02f;
    public int starsForEpisode2 = 0;
    public int starsForEndlessMode = 0;

    void Start()
    {



        faderMainScript.brighten = true;

        EnableButtons();

        ProgressCheck();
        
        OptionsLoader();

        if (!inProgress)
        {
             TextGameName();
        }
        else
        {
            lineAmbient.SetActive(false);
            lineBall.SetActive(false);
        }

        MuteStarter();
    }

    void FixedUpdate()
    {
        FadeGameName();
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
                //go to episode 2
                break;
            case 6:
                //back to episode 1
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
        accelerometerText.text = accelerometerActive ? "accelerometer ON" : "accelerometer OFF";
    }
    private void GraphicTextChange()
    {
        //graphicsQualityText.text = graphicsHigh ? "high quality" : "low quality";
    }
    private void ControlTextChange()
    {
        controlTypeText.text = leftHandedControl ? "LEFT-HANDED" : "RIGHT-HANDED";
    }
    private void TextGameName()
    {
            lineAmbient.SetActive(true);
            lineBall.SetActive(true);

            InvisibleText(textLevels);
            InvisibleText(textEndlessGame);
            InvisibleText(textOptions);
            InvisibleText(textReturnInGame);

        StartCoroutine(LineTextChanger());
    }
    private IEnumerator LineTextChanger()
    {
        blocker.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        gameNameVisible = true;
        yield return new WaitForSeconds(1);
        startGameVisible = false;
        yield return new WaitForSeconds(0.5f);
        blocker.SetActive(false);
        yield break;
    }
    private void FadeGameName()
    {
        if (gameNameVisible)
        {
            FaderTextOff(textAmbient);
            FaderTextOff(textBall);
        }
        if (!startGameVisible)
        {
            FaderTextOn(textLevels);
            FaderTextOn(textEndlessGame);
            FaderTextOn(textOptions);
            FaderTextOn(textReturnInGame);
        }
    }
    public void FaderTextOn(TMP_Text textObj)
    {
        if (textObj.color.a < 1)
        {
            float aText = textObj.color.a;
            aText += textFadeSpeed;
            textObj.color = new Color(textObj.color.r, textObj.color.g, textObj.color.b, aText);
        }
        else
        {
            startGameVisible = true;
        }
    }
    public void FaderTextOff(TMP_Text textObj)
    {
        if (textObj.color.a > 0)
        {
            float aText = textObj.color.a;
            aText -= textFadeSpeed;
            textObj.color = new Color(textObj.color.r, textObj.color.g, textObj.color.b, aText);
        }
        else if (textObj.color.a <= 0)
        {
            lineAmbient.SetActive(false);
            lineBall.SetActive(false);
            gameNameVisible = false;
        }
    }
    private void InvisibleText(TMP_Text textObj)
    {
        textObj.color = new Color(textObj.color.r, textObj.color.g, textObj.color.b, 0);
    }
    #endregion

    #region Кнопки навигиции по меню
    /// <summary>
    /// Переход из меню к выбору уровня
    /// </summary>
    public void ButtonMainToLevels()
    {
        audioScriptMain.clickSound.Play();
        pressedButton = 1;
        DisableButtons();
    }
    /// <summary>
    /// Переход от выбора уровня к меню
    /// </summary>
    public void ButtonLevelsToMain()
    {
        audioScriptMain.clickSound.Play();
        pressedButton = 2;
        DisableButtons();
    }
    /// <summary>
    /// Переход от меню к настройкам
    /// </summary>
    public void ButtonMainToOptions()
    {
        audioScriptMain.clickSound.Play();
        pressedButton = 3;
        DisableButtons();
    }
    /// <summary>
    /// Переход от настроек к меню
    /// </summary>
    public void ButtonOptionsToMain()
    {
        audioScriptMain.clickSound.Play();
        pressedButton = 4;
        DisableButtons();
        OptionsSaver();
    }
    public void ButtonEpisode1ToEpisode2()
    {
        if (starsTotal >= starsForEpisode2 && SaveLoadData.GetLevelProgress() >= 9)
        {
            audioScriptMain.clickSound.Play();
            pressedButton = 5;
            DisableButtons();
        }
        else
        {
            audioScriptMain.errorSound.Play();
        }
    }
    public void ButtonEpisode2ToEpisode1()
    {
        audioScriptMain.clickSound.Play();
            pressedButton = 6;
            DisableButtons();
    }
    /// <summary>
    /// Переход к покупке отключения рекламы
    /// </summary>
    public void ButtonNoAds()
    {
        audioScriptMain.clickSound.Play();
        DisableButtons();
        /////////////////////////////////////////////////////no ads
    }
    public void ButtonDeleteGame()
    {
        audioScriptMain.clickSound.Play();
        deleteGame.text = "really?";

        if (confirmDeleteGameProgress)
        {
            PlayerPrefs.DeleteAll();
            //saveGameScript.saving = true;
            //saveSettingsScript.saving = true;
            Fade();
            Invoke("ReloadMain", lvlStartTime);
        }
        confirmDeleteGameProgress = true;
    }
    /// <summary>
    /// Возврат к игре
    /// </summary>
    public void ButtonReturnInGame()
    {
        audioScriptMain.clickSound.Play();
        audioScriptMain.fadeOut = true;
        pressedButton = 5;
        DisableButtons();
        Fade();
        Invoke("StartLevel", lvlStartTime);
    }
    #endregion

    #region Кнопки запуска уровней логика запуска уровней
    public void StartGame()
    {
        SaveLoadData.SetMusicTime(0);
        audioScriptMain.clickSound.Play();
        levelSelected = true;
        DisableButtons();
        audioScriptMain.fadeOut = true;
        SaveLoadData.SetContinuousTaken(false);
        SaveLoadData.SetInProgressTemp(false);
        SaveLoadData.SetInProgress(false);
        SaveLoadData.SetFirstLevelLaunch(true);
        SaveLoadData.ResetCoordinates();
        SaveLoadData.ResetLives();
        SaveLoadData.ResetTextProgress();
        SaveLoadData.DelCamAxisTemp();
        SaveLoadData.SetScene(scene);
        SaveLoadData.ResetStarsScore(scene);
        Fade();
        Invoke("StartLevel", lvlStartTime);
    }
    public void ButtonStartEndlessMode()
    {
        scene = 9;
        audioScriptMain.clickSound.Play();
        levelSelected = true;
        DisableButtons();
        audioScriptMain.fadeOut = true;
        SaveLoadData.SetContinuousTaken(false);
        SaveLoadData.SetInProgressTemp(false);
        SaveLoadData.SetInProgress(false);
        SaveLoadData.SetFirstLevelLaunch(true);
        SaveLoadData.ResetCoordinates();
        SaveLoadData.ResetLives();
        SaveLoadData.ResetTextProgress();
        SaveLoadData.DelCamAxisTemp();
        SaveLoadData.SetScene(scene);
        SaveLoadData.ResetStarsScore(scene);
        SaveLoadData.ResetEndlessScoreTemp();
        //SaveLoadData.ResetStarsEndlessMode();
        Fade();
        Invoke("StartLevel", lvlStartTime);
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(scene);
    }
    public void ReloadMain()
    {
        SceneManager.LoadScene(0);
    }
    private void Fade()
    {
        panelObj.SetActive(true);
        faderMainScript.fading = true;
    }
    private void ProgressCheck()
    {
        starsTotal = SaveLoadData.GetStarsTotal();
        starsNumber.text = Convert.ToString(starsTotal);
        starsEndlessModeTotal = SaveLoadData.GetStarsEndlessModeTotal();
        starsRecordEndlessLevel.text = Convert.ToString(starsEndlessModeTotal);
        inProgress = SaveLoadData.GetInProgress();
        inProgressTemp = SaveLoadData.GetInProgressTemp();
        currentLvl = SaveLoadData.GetLevelProgress();

        /*
        if (starsTotal >= starsForEpisode2 && SaveLoadData.GetLevelProgress() >= 9)
        {
            nextLevelsButtonToEpisode2.color = turquoise;
            needMoreStarsToEpisode2.SetActive(false);
            nextToEpisode2.SetActive(true);
        }
        else
        {
            nextLevelsButtonToEpisode2.color = Color.red;
            needMoreStarsToEpisode2.SetActive(true);
            nextToEpisode2.SetActive(false);
        }
        */
        /*
        if (starsTotal >= starsForEndlessMode)
        {
            needMoreStarsToEndlessMode.SetActive(false);
            endlessModeTextActive.SetActive(true);
            endlessModeButton.SetActive(true);
        }
        else
        {
            needMoreStarsToEndlessMode.SetActive(true);
            endlessModeTextActive.SetActive(false);
            endlessModeButton.SetActive(false);
        }
        */


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
                Levels.SetActive(true);
                EndlessGame.SetActive(true);
                Options.SetActive(true);
                ReturnInGame.SetActive(true);
                break;
            default:
                //Выключает режим "продолжить игру"
                SaveLoadData.ResetScene();
                Levels.SetActive(true);
                EndlessGame.SetActive(true);
                Options.SetActive(true);
                ReturnInGame.SetActive(false);
                break;
        }
    }
    public void DisableButtons()
    {
        lineDeleteGameProgress.interactable = false;
        lineLevelsButton.interactable = false;
        lineEndlessGameButton.interactable = false;
        lineOptionsButton.interactable = false;
        lineReturnInGameButton.interactable = false;
        
        blocker.SetActive(true);
    }
    public void EnableButtons()
    {
        lineDeleteGameProgress.interactable = true;
        lineLevelsButton.interactable = true;
        lineEndlessGameButton.interactable = true;
        lineOptionsButton.interactable = true;
        lineReturnInGameButton.interactable = true;
        
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
        audioScriptMain.jumpSound.Play();
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
        audioScriptMain.jumpSound.Play();
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
        audioScriptMain.jumpSound.Play();
    }

    void MuteStarter()
    { 
        if (soundButtonSlider.value == -40f)
        {
            MasterMixer.SetFloat("soundVolume", -80f);
            soundVolume = soundButtonSlider.value;
            soundMuted = true;
        }
        if (musicButtonSlider.value == -40f)
        {
            MasterMixer.SetFloat("musicVolume", -80f);
            musicVolume = musicButtonSlider.value;
            musicMuted = true;
        }
    }
    #endregion

    #region Настройка управления

    public void ButtonAccelerometer()
    {
        audioScriptMain.clickSound.Play();
        accelerometerActive = !accelerometerActive;
        AccelerometerTextChange();
    }
    public void ButtonGrapchic()
    {
        audioScriptMain.clickSound.Play();
        graphicsHigh = !graphicsHigh;
        GraphicTextChange();
    }

    public void ButtonControl()
    {
        audioScriptMain.clickSound.Play();
        leftHandedControl = !leftHandedControl;
        ControlTextChange();
    }
    #endregion

    #region Загрузка и сохранение всех настроек
    public void OptionsLoader()
    {
            if (SaveLoadData.GetOptionsDataChecker())
            {
                SaveLoadData.GetOptions(out soundVolume, out musicVolume, out soundMuted, out musicMuted, out leftHandedControl, out accelerometerActive, out graphicsHigh);

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
        SaveLoadData.SetOptions(soundVolume, musicVolume, soundMuted, musicMuted, leftHandedControl, accelerometerActive, graphicsHigh);
        SaveLoadData.SetOptionsDataChecker(true);
        SaveLoadData.SetControlChange(true);//для того что бы показать изменившиеся настройки при возвращении в игру
        saveSettingsScript.saving = true;
    }
    #endregion
}