using UnityEngine;
using PlayerPrefsSavingMethods;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [Header("Скрипты")]
    public SaveLevelScript saveLevelScript;
    public SaveGameScript saveGameScript;
    public ScriptUI scriptUI;
    //public CameraController cameraController;
    public GameScript gameScript;
    //public PlayerController playerController;

    [Header("Объекты")]
    //public GameObject checkPoint;
    public ParticleSystem explode;
    bool locker = false;
    public GameObject activator;
    public AudioSource checkPointSound;
    public GameObject checkPointEffectSystem;
    public BoxCollider trigger;
    private Vector3 triggerStandartSize;
    private Vector3 triggerBigSize = new Vector3(10f, 10f, 10f);
    private bool bigSizeTriggerForm = false;

    void Awake()
    {
        saveLevelScript = FindObjectOfType<SaveLevelScript>();
        saveGameScript = FindObjectOfType<SaveGameScript>();
        scriptUI = FindObjectOfType<ScriptUI>();
        //cameraController = FindObjectOfType<CameraController>();
        gameScript = FindObjectOfType<GameScript>();
        //playerController = FindObjectOfType<PlayerController>();
        triggerStandartSize = trigger.size;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !locker)
        {
            SaveLoadData.SetStarsScore(SceneManager.GetActiveScene().buildIndex, gameScript.starsScore);

            if (activator.activeSelf)
            {
                activator.SetActive(false);
                checkPointSound.Play();
                scriptUI.gameSaved.text = "game saved...";
                //SaveLoadData.SaveCamAxisTemp(cameraController.X, cameraController.Y);
                SaveLoadData.SetInProgress(true);
                SaveLoadData.SaveCoordinates(transform.position.x, transform.position.y, transform.position.z);
                //Debug.Log(cameraController.X);
                //Debug.Log(cameraController.Y);
                //SaveLoadData.SetPropertiesFormNum(playerController.playerForm);
                saveLevelScript.saving = true;
                Invoke("SaveGame", 0.2f);
            }
            else
            {
                if (scriptUI.checkPointTextHider)
                {
                    scriptUI.checkPointTextHider = false;
                    scriptUI.gameSaved.text = " ";
                }
                else
                {
                    scriptUI.gameSaved.text = "game loaded...";
                }
                //saveLevelScript.saving = true;
                saveGameScript.saving = true;
                checkPointEffectSystem.SetActive(false);
            }

            //levelTextScript.progression = SaveLoadData.GetTextProgress();

            Invoke("Effect", 0.0f);
            Invoke("Off", 3f);

            scriptUI.gameSaved.transform.gameObject.SetActive(true);
            scriptUI.checkPointTextOn = true;
            locker = true;

            trigger.size = triggerBigSize;
            bigSizeTriggerForm = true;
        }
    }
    /// <summary>
    /// Эффект отключения системы частиц при активации чекпоинта
    /// </summary>
    void Effect()
    {
        var stars = explode.main;
        var fo = explode.forceOverLifetime;
        stars.loop = false;
        fo.enabled = true;
    }
    /// <summary>
    /// Отключает систему частиц при активации чекпоинта
    /// </summary>
    void Off()
    {
        checkPointEffectSystem.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && bigSizeTriggerForm)
        {
            RestoreCheckPoint();
        }
    }
    void RestoreCheckPoint()
    {
        activator.SetActive(true);
        locker = false;
        trigger.size = triggerStandartSize;
        bigSizeTriggerForm = false;
        Invoke("On", 3f);
    }
    void On()
    {
        checkPointEffectSystem.SetActive(true);
        var stars = explode.main;
        var force = explode.forceOverLifetime;
        stars.loop = true;
        force.enabled = false;
    }
    void SaveGame()
    {
        saveGameScript.saving = true;
    }
}