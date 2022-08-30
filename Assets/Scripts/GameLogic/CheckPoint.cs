using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefsSavingMethods;

public class CheckPoint : MonoBehaviour
{
    public SaveLevelScript saveLevelScript;
    public SaveGameScript saveGameScript;
    public ScriptUI scriptUI;
    //public LevelTextScript levelTextScript;

    public GameObject checkPoint;
    public ParticleSystem explode;
    public CameraController cameraController;
    bool locker = false;

    void Awake()
    {
        //levelTextScript = FindObjectOfType<LevelTextScript>();
        saveLevelScript = FindObjectOfType<SaveLevelScript>();
        cameraController = FindObjectOfType<CameraController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !locker)
        {
            SaveLoadData.SetInProgress(true);
            SaveLoadData.SaveCoordinates(transform.position.x, transform.position.y, transform.position.z);
            SaveLoadData.SaveCamAxisTemp(cameraController.X, cameraController.Y);
            saveLevelScript.saving = true;
            saveGameScript.saving = true;
            //levelTextScript.progression = SaveLoadData.GetTextProgress();

            Invoke("ChangeCheckPointText", 1.6f);

            Invoke("Effect", 0.0f);
            Invoke("Off", 3f);

            scriptUI.gameSaved.transform.gameObject.SetActive(true);
            scriptUI.checkPointTextOn = true;
            locker = true;
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
        checkPoint.SetActive(false);
    }
    void ChangeCheckPointText()
    {
        scriptUI.gameSaved.text = "game saved...";
        SaveLoadData.SetCheckpoitTextSaving(true);
    }
}
