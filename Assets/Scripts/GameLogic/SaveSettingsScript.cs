using BayatGames.SaveGameFree;
using PlayerPrefsSavingMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettingsScript : MonoBehaviour
{
    public SettingData settingData;
    public string identifier = "ambientBallSavedSettingData";
    public bool saving = false;
    public bool loading = false;
    
    void Awake()
    {
        if (!SaveLoadData.GetInProgressTemp())
        {
            LoadSettingsData();
        }
    }
    
    void Update()
    {
        if (saving)
        {
            Invoke("SaveSettingsData", 0f);
            saving = false;
        }
    }

    #region Сохраняемый класс
    [System.Serializable]
    public class SettingData
    {
        public float soundVolume;
        public float musicVolume;
        public bool soundMuted;
        public bool musicMuted;
        public bool controlAccelerometer;
        public bool controlJoystick;

        public bool checker;
    }
    #endregion

    private void SaveSettingsData()
    {
        SaveLoadData.GetOptions(out settingData.soundVolume, out settingData.musicVolume, out settingData.soundMuted, out settingData.musicMuted, out settingData.controlAccelerometer, out settingData.controlJoystick);
        settingData.checker = SaveLoadData.GetOptionsDataChecker();

        SaveGame.Save<SettingData>(identifier, settingData);
        Debug.Log("settings_saved!");
    }
    private void LoadSettingsData()
    {
        settingData = SaveGame.Load<SettingData>(
            identifier,
            new SettingData());

        SaveLoadData.SetOptions(settingData.soundVolume, settingData.musicVolume, settingData.soundMuted, settingData.musicMuted, settingData.controlAccelerometer, settingData.controlJoystick);
        SaveLoadData.SetOptionsDataChecker(settingData.checker);

        Debug.Log("settings_loaded!");
    }
}