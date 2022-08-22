using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointScript : MonoBehaviour
{
    public GameObject self;
    private bool locker = false;
    public LevelTextScript levelTextScript;

    void Start()
    {
        levelTextScript = FindObjectOfType<LevelTextScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !locker)
        {
            int i = SaveLoadData.GetTextProgress();
            i++;
            SaveLoadData.SetTextProgress(i);
            locker = true;

            levelTextScript.progression = i;
            levelTextScript.textOff = true;
        }
    }
}