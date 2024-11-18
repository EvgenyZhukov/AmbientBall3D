using PlayerPrefsSavingMethods;
using UnityEngine;

public class ArrowPointScript : MonoBehaviour
{
    public GameObject self;
    private bool locker = false;
    private LevelTextScript levelTextScript;
    public int arrowNumber;

    void Awake()
    {
        levelTextScript = FindObjectOfType<LevelTextScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !locker)
        {
            SaveLoadData.SetTextProgress(arrowNumber);
            levelTextScript.progression = arrowNumber;
            levelTextScript.textOff = true;
            locker = true;
        }
    }
}