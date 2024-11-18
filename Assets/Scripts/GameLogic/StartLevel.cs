using UnityEngine;
using PlayerPrefsSavingMethods;

public class StartLevel : MonoBehaviour
{
    public SaveLevelScript saveLevelScript;
    public SaveGameScript saveGameScript;
    public GameScript gameScript;

    public GameObject startPoint;
    public CameraController cameraController;

    void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        saveLevelScript = FindObjectOfType<SaveLevelScript>();

        if (!SaveLoadData.GetInProgress())
        {
            SaveLoadData.SetTextProgress(0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaveLoadData.SetContinuousTaken(false);
            SaveLoadData.SetInProgress(true);
            SaveLoadData.SaveCoordinates(transform.position.x, transform.position.y, transform.position.z);
            //SaveLoadData.SaveCamAxisTemp(cameraController.X, cameraController.Y);
            saveLevelScript.saving = true;
            saveGameScript.saving = true;
            Invoke("Off", 0f);
        }
    }
    /// <summary>
    /// Отключает стартовый чекпоинт
    /// </summary>
    void Off()
    {
        startPoint.SetActive(false);
    }
}
