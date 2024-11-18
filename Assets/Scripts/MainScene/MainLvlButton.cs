using UnityEngine;
using TMPro;
using System;
using PlayerPrefsSavingMethods;

public class MainLvlButton : MonoBehaviour
{
    private Color turquoise = new Color(0, 241, 255, 255);
    public TMP_Text lvlNumberText;
    public int lvlNumber;
    public MainScript MainScript;
    public AudioScriptMain audioScriptMain;

    public GameObject block;
    public Material lvlNotReady;
    public Material lvlReady;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public int stars;

    void Start()
    {
        MainScript = FindObjectOfType<MainScript>();
        audioScriptMain = FindObjectOfType<AudioScriptMain>();
        lvlNumber = Int32.Parse(lvlNumberText.text);

        lvlNumberText.color = SaveLoadData.GetLevelProgress() >= lvlNumber ? turquoise : Color.red;
        if (lvlNumber == 1)
        {
            lvlNumberText.color = turquoise;
        }

        block.GetComponent<MeshRenderer>().material = SaveLoadData.GetLevelProgress() > lvlNumber ? lvlReady : lvlNotReady;

        stars = SaveLoadData.GetStars(lvlNumber);

        switch (stars)
        {
            case 1:
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
            case 2:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
                break;
            case 3:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                break;
            default:
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
        }
    }

    public void StartLvlButton()
    {
        if (!MainScript.levelSelected)
        {
            if (MainScript.currentLvl >= lvlNumber)
            {
                audioScriptMain.clickSound.Play();
                MainScript.scene = lvlNumber;
                MainScript.StartGame();
            }
            else
            {
                audioScriptMain.errorSound.Play();
            }
        }
    }
}
