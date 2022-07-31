using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefsSavingMethods;

public class StartLevel : MonoBehaviour
{
    public SaveLevelScript saveLevelScript;
    public SaveGameScript saveGameScript;

    public GameObject checkPoint;
    public CameraController CameraController;

    void Start()
    {
        saveLevelScript = FindObjectOfType<SaveLevelScript>();
        SaveLoadData.SetTextProgress(0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaveLoadData.SetContinuousTaken(false);
            SaveLoadData.SetInProgress(true);
            SaveLoadData.SaveCoordinates(transform.position.x, transform.position.y, transform.position.z);
            SaveLoadData.SaveCamAxisTemp(CameraController.X, CameraController.Y);
            saveLevelScript.saving = true;
            saveGameScript.saving = true;
            Invoke("Off", 0f);
        }
    }
    /// <summary>
    /// Отключает систему частиц при активации чекпоинта
    /// </summary>
    void Off()
    {
        checkPoint.SetActive(false);
    }
}
