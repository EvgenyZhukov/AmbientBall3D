using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointScript : MonoBehaviour
{
    public GameObject self;
    private bool locker = false;
    public LevelTextScript levelTextScript;
    public int arrowNumber;

    void Start()
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