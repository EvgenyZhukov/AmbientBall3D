using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayerPrefsSavingMethods;
using UnityEngine.SceneManagement;

public class ScriptUI : MonoBehaviour
{
    [Header("�������")]
    public GameScript GameScript;
    public PlayerController playerConroller;
    public Fader faderMainScript;
    public FixedJoystick JoystickCam;
    public FixedJoystick JoystickMove;
    public SaveLevelScript SaveLevelScript;
    public CameraController CameraController;
    [Header("�������")]
    public GameObject live1;
    public GameObject panelObjFader;
    public TMP_Text livesNumber;
    public GameObject pauseCanvas;

    public TMP_Text rightBottonText;
    public TMP_Text leftBottonText;
    public TMP_Text rightTopText;
    public TMP_Text leftTopText;
    public TMP_Text startButtonText;
    public TMP_Text startNextLevelText;

    public RectTransform rectTransformButtonA;
    public RectTransform rectTransformButtonB;

    public RectTransform rectTransformJoystickCam;
    public RectTransform rectTransformJoystickMove;
    public GameObject JoystickMoveObj;
    public Image joystickCamCircle;
    public Image joystickMoveCircle;
    public Image joystickCamStick;
    public Image joystickMoveStick;

    public RectTransform rightBottonRT;
    public RectTransform leftBottonRT;

    public Button jumpButton;
    public Button transformButton;

    public Button menuButton;
    public Button pauseButton;
    public Button startButton;
    public TMP_Text defeatText;
    public TMP_Text restartButtonText;
    public TMP_Text watchAdButtonText;
    public TMP_Text gameSaved;
    public TMP_Text gameFinished;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public GameObject defeatCanvas;
    [Header("UI")]
    public float screenChangeTime = 1.2f;
    public float changeStep = 0.04f;
    public bool pauseTextOn = false;
    public bool pauseTextOff = false;
    public bool checkPointTextOn = false;
    public bool endLevelTextOn = false;
    public bool accelerometerActive = false;
    public bool leftHandedControl = false;
    public bool offTextAfterNextLevelButton = false;

    void Start()
    {
        SaveLevelScript = FindObjectOfType<SaveLevelScript>();

        SetControl();
        SettingUI();
        SetTextUI();
        Reducer();

        LivesUI();

        FirstPause();
    }

    void FixedUpdate()
    {
        LivesAnimator();

        FaderPauseText();
        FaderPopupText();
    }

    /// <summary>
    /// ������ ������ � ����
    /// </summary>
    public void MainButton()
    {
        SaveLevelScript.saving = true;      ////////////////////////////���������� ������
        DeactivateUI();
        panelObjFader.SetActive(true);
        faderMainScript.fading = true;
        Invoke("GoToMain", screenChangeTime);
    }
    /// <summary>
    /// �������� ������� ��������� ������ � ��������� ����
    /// </summary>
    public void GoToMain()
    {
        SaveLoadData.SetInProgressTemp(true);
        SaveLoadData.SaveCoordinatesTemp(GameScript.player.transform.position.x, GameScript.player.transform.position.y, GameScript.player.transform.position.z);
        SaveLoadData.SaveCamAxisTemp(CameraController.X, CameraController.Y);

        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// ������ �����
    /// </summary>
    public void PauseButton()
    {
        pauseCanvas.SetActive(true);
        panelObjFader.SetActive(true);

        faderMainScript.fadingHalf = true;
        pauseTextOn = true;

        DeactivateUI();

        Invoke("Pause", 0.6f);
    }
    /// <summary>
    /// ������ ������ ����(������ �����)
    /// </summary>
    public void StartGameButton()
    {
        faderMainScript.brighten = true;
        pauseTextOff = true;
        startButton.interactable = false;

        Resume();
    }
    /// <summary>
    /// ������ ������ ���������� ������
    /// </summary>
    public void StartNextLevelButton()
    {
        offTextAfterNextLevelButton = true;
    }
    /// <summary>
    /// ������ ���� �� �����, ���������� ������ ������ ����
    /// </summary>
    private void Pause()
    {
        startButton.interactable = true;
        Time.timeScale = 0f;
    }
    /// <summary>
    /// ������������ ���� ����� �����
    /// </summary>
    private void Resume()
    {
        Time.timeScale = 1f;
        ResetAccelerometerZero();

        Invoke("OffPauseCanvas", 0.6f);
    }
    /// <summary>
    /// ��������� ������������ ��� ��������� ��� ������������ ������
    /// </summary>
    public void ResetAccelerometerZero()
    {
        playerConroller.correct = Input.acceleration.y;
    }
    /// <summary>
    /// ��������� ����� �����, ���������� ������ UI
    /// </summary>
    private void OffPauseCanvas()
    {
        ActivateUI();
        pauseCanvas.SetActive(false);
    }
    /// <summary>
    /// �������� ��������� ������������ - ����������
    /// </summary>
    private void ActivateUI()
    {
        if (JoystickMoveObj)
        {
            joystickMoveStick.raycastTarget = true;
            joystickMoveCircle.raycastTarget = true;
        }
        joystickCamStick.raycastTarget = true;
        joystickCamCircle.raycastTarget = true;

        //////////// ��������� ��������� ������ ��� �� ����� ���� ��������
        if (jumpButton.transform.parent.gameObject.activeSelf)
        {
            jumpButton.interactable = true;

        }
        if (transformButton.transform.parent.gameObject.activeSelf)
        {
            transformButton.interactable = true;
        }

        menuButton.interactable = true;
        pauseButton.interactable = true;
    }
    /// <summary>
    /// ��������� ��������� ������������ - ����������
    /// </summary>
    private void DeactivateUI()
    {
        if (JoystickMoveObj)
        {
            joystickMoveStick.raycastTarget = false;
            joystickMoveCircle.raycastTarget = false;
        }
        joystickCamStick.raycastTarget = false;
        joystickCamCircle.raycastTarget = false;

        if (jumpButton.transform.parent.gameObject.activeSelf)
        {
            jumpButton.interactable = false;

        }
        if (transformButton.transform.parent.gameObject.activeSelf)
        {
            transformButton.interactable = false;
        }

        menuButton.interactable = false;
        pauseButton.interactable = false;
        startButton.interactable = false;
    }
    /// <summary>
    /// ������ ����������� ������
    /// </summary>
    public void RestartGameButton()
    {
        Time.timeScale = 1f;
        SaveLoadData.SetFirstLevelLaunch(true);
        SaveLoadData.ResetCoordinates();
        SaveLoadData.ResetTextProgress();
        SaveLoadData.DelCamAxisTemp();
        SaveLoadData.SetCheckpoitTextSaving(true);
        RestartLevel();
    }
    /// <summary>
    /// ������ ��������� ������� �� �����
    /// </summary>
    public void WatchAdButton()
    {
        SaveLoadData.SetContinuousTaken(true);
        Time.timeScale = 1f;
        //////////////////////////////////////////////////////////////////////// ������ ��������� �������
        RestartLevel();
    }
    /// <summary>
    /// ���������� ������
    /// </summary>
    public void RestartLevel()
    {
        SaveLoadData.ResetLives();

        faderMainScript.fading = true;
        pauseTextOff = true;

        Invoke("ReloadScene", 0.6f);
    }
    /// <summary>
    /// ������������� �������
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(GameScript.scene);
    }
    /// <summary>
    /// ��������� ������ ������
    /// </summary>
    public void LivesAnimator()
    {
        live1.transform.Rotate(new Vector3(0.09f, 0.12f, 0));
    }
    /// <summary>
    /// ������ ���������� ������ � ���������� ������������
    /// </summary>
    public void LivesUI()
    {
        int lives = SaveLoadData.GetLives();    // ��������� ���������� ������
        switch (lives)
        {
            case 0:
                livesNumber.text = "0";
                break;
            case 1:
                livesNumber.text = "1";
                break;
            case 2:
                livesNumber.text = "2";
                break;
            case 3:
                livesNumber.text = "3";
                break;
            default:
                livesNumber.text = "E";
                break;
        }
    }
    /// <summary>
    /// ����������� ��������� ������������ �������� ��������
    /// </summary>
    public void SettingUI()
    {
        if (accelerometerActive)
        {
            JoystickMoveObj.gameObject.SetActive(false);
        }
        else
        {
            JoystickMoveObj.gameObject.SetActive(true);
        }

        if (!leftHandedControl)
        {
            rectTransformJoystickCam.anchorMin = new Vector2(0, 0);
            rectTransformJoystickCam.anchorMax = new Vector2(0, 0);
            if (JoystickMoveObj)
            {
                rectTransformJoystickMove.anchorMin = new Vector2(1, 0);
                rectTransformJoystickMove.anchorMax = new Vector2(1, 0);
            }

            rectTransformButtonA.anchorMin = new Vector2(0, 1);
            rectTransformButtonA.anchorMax = new Vector2(0, 1);

            rectTransformButtonB.anchorMin = new Vector2(1, 1);
            rectTransformButtonB.anchorMax = new Vector2(1, 1);
        }
        else
        {
            rectTransformJoystickCam.anchorMin = new Vector2(1, 0);
            rectTransformJoystickCam.anchorMax = new Vector2(1, 0);

            float posXjoystickCam = rectTransformJoystickCam.anchoredPosition3D.x;

            rectTransformJoystickCam.anchoredPosition3D = new Vector3(rectTransformJoystickMove.anchoredPosition3D.x, rectTransformJoystickCam.anchoredPosition3D.y, rectTransformJoystickCam.anchoredPosition3D.z);

            if (JoystickMoveObj)
            {
                rectTransformJoystickMove.anchorMin = new Vector2(0, 0);
                rectTransformJoystickMove.anchorMax = new Vector2(0, 0);

                rectTransformJoystickMove.anchoredPosition3D = new Vector3(posXjoystickCam, rectTransformJoystickMove.anchoredPosition3D.y, rectTransformJoystickMove.anchoredPosition3D.z);
            }

            rectTransformButtonA.anchorMin = new Vector2(1, 1);
            rectTransformButtonA.anchorMax = new Vector2(1, 1);

            float posXbuttonA = rectTransformButtonA.anchoredPosition3D.x;

            rectTransformButtonA.anchoredPosition3D = new Vector3(rectTransformButtonB.anchoredPosition3D.x, rectTransformButtonA.anchoredPosition3D.y, rectTransformButtonA.anchoredPosition3D.z);

            rectTransformButtonB.anchorMin = new Vector2(0, 1);
            rectTransformButtonB.anchorMax = new Vector2(0, 1);

            rectTransformButtonB.anchoredPosition3D = new Vector3(posXbuttonA, rectTransformButtonB.anchoredPosition3D.y, rectTransformButtonB.anchoredPosition3D.z);
        }

        if (SaveLoadData.GetCheckpoitTextSaving())
        {
            gameSaved.text = "game saved...";
        }
        else
        {
            gameSaved.text = "game loaded...";
        }
    }
    /// <summary>
    /// ��������� ��������� ����������
    /// </summary>
    public void SetControl()
    {
        SaveLoadData.GetControlOptions(out accelerometerActive, out leftHandedControl);
    }
    /// <summary>
    /// ���������� ����� � ���������� ������������ �������� ��������
    /// </summary>
    public void SetTextUI()
    {

        switch (!leftHandedControl)
        {
            case true:
                rightTopText.text = "transform";
                leftTopText.text = "jump";
                if (!accelerometerActive)
                {
                    rightBottonText.text = "move control";
                    leftBottonText.text = "camera control";
                }
                else
                {
                    rightBottonText.text = "pause resets zero position of accelerometer";
                    rightBottonRT.anchoredPosition3D = new Vector3(-450, 200, 0);
                    rightBottonRT.sizeDelta = new Vector2(750, 200);
                    leftBottonText.text = "camera control";
                }
                break;
            default:
                rightTopText.text = "jump";
                leftTopText.text = "transform";
                if (!accelerometerActive)
                {
                    rightBottonText.text = "camera control";
                    leftBottonText.text = "move control";
                }
                else
                {
                    rightBottonText.text = "camera control";
                    leftBottonText.text = "pause resets zero position of accelerometer";
                    leftBottonRT.anchoredPosition3D = new Vector3(450, 200, 0);
                    leftBottonRT.sizeDelta = new Vector2(750, 200);
                }
                break;
        }
    }
    /// <summary>
    /// ������ ����� ��� ����� ������ ������� ������
    /// </summary>
    private void FirstPause()
    {
        bool levelLaunchCheck = SaveLoadData.GetFirstLevelLaunch();
        if (levelLaunchCheck)
        {
            DeactivateUI();
            pauseCanvas.SetActive(true);
            pauseTextOn = true;
            SaveLoadData.SetFirstLevelLaunch(false);
            Invoke("Pause", 0.6f);
        }
    }
    /// <summary>
    /// ������ ����� ������ (������ ��� ���������� � ��������)
    /// </summary>
    private void FaderTextOn(TMP_Text textObj, string tag)
    {
        if (textObj.color.a < 1)
        {
            float aText = textObj.color.a;
            aText += changeStep;
            textObj.color = new Color(textObj.color.r, textObj.color.g, textObj.color.b, aText);
        }
        else if (textObj.color.a >= 0 && tag == "pauseText")
        {
            pauseTextOn = false;
        }
        else if (textObj.color.a >= 0 && tag == "checkPointText")
        {
            checkPointTextOn = false;
        }
        else if (textObj.color.a >= 0 && tag == "endLevelText")
        {
            endLevelTextOn = false;
        }
    }
    private void FaderTextOff(TMP_Text textObj, string tag)
    {
        if (textObj.color.a > 0)
        {
            float aText = textObj.color.a;
            aText -= changeStep;
            textObj.color = new Color(textObj.color.r, textObj.color.g, textObj.color.b, aText);
        }
        else if (textObj.color.a <= 0 && tag == "pauseText")
        {
            pauseTextOff = false;
        }
        else if (textObj.color.a <= 0 && tag == "checkPointText")
        {
            gameSaved.transform.gameObject.SetActive(false);
        }
        else if (textObj.color.a <= 0 && tag == "endLevelText")
        {
            gameFinished.transform.gameObject.SetActive(false);
        }
    }
    private void FaderPauseText()
    {
        switch ((pauseTextOn, pauseTextOff))
        {
            case (true, false):
                if (rightBottonText) FaderTextOn(rightBottonText, "pauseText");
                if (leftBottonText) FaderTextOn(leftBottonText, "pauseText");
                if (rightTopText) FaderTextOn(rightTopText, "pauseText");
                if (leftTopText) FaderTextOn(leftTopText, "pauseText");
                if (startButtonText) FaderTextOn(startButtonText, "pauseText");
                if (defeatText) FaderTextOn(defeatText, "pauseText");
                if (restartButtonText) FaderTextOn(restartButtonText, "pauseText");
                if (watchAdButtonText) FaderTextOn(watchAdButtonText, "pauseText");
                break;
            case (false, true):
                if (rightBottonText) FaderTextOff(rightBottonText, "pauseText");
                if (leftBottonText) FaderTextOff(leftBottonText, "pauseText");
                if (rightTopText) FaderTextOff(rightTopText, "pauseText");
                if (leftTopText) FaderTextOff(leftTopText, "pauseText");
                if (startButtonText) FaderTextOff(startButtonText, "pauseText");
                if (defeatText) FaderTextOff(defeatText, "pauseText");
                if (restartButtonText) FaderTextOff(restartButtonText, "pauseText");
                if (watchAdButtonText) FaderTextOff(watchAdButtonText, "pauseText");
                break;
            default:
                break;
        }
    }
    private void FaderPopupText()
    {
        if (gameSaved.transform.gameObject)
        {
            switch (checkPointTextOn)
            {
                case (true):
                    FaderTextOn(gameSaved, "checkPointText");
                    break;
                case (false):
                    FaderTextOff(gameSaved, "checkPointText");
                    break;
            }
        }
        if (gameFinished.transform.gameObject)
        {
            switch (endLevelTextOn)
            {
                case (true):
                    FaderTextOn(gameFinished, "endLevelText");
                    FaderTextOn(startNextLevelText, "endLevelText");
                    break;
                case (false):
                    if (offTextAfterNextLevelButton)
                    {
                        FaderTextOff(gameFinished, "endLevelText");
                        FaderTextOff(startNextLevelText, "endLevelText");
                    }
                    break;
            }
        }
    }
    /// <summary>
    /// ��������� ���������� �� ��������� �������
    /// </summary>
    private void Reducer()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        switch (sceneNumber)
        {
            case 1:
                jumpButton.transform.gameObject.SetActive(false);
                transformButton.transform.gameObject.SetActive(false);
                rightTopText.transform.gameObject.SetActive(false);
                leftTopText.transform.gameObject.SetActive(false);
                break;
            case 2:
                transformButton.transform.gameObject.SetActive(false);
                if (!leftHandedControl)
                {
                    rightTopText.transform.gameObject.SetActive(false);
                }
                else
                {
                    leftTopText.transform.gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}