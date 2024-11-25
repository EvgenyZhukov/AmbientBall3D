﻿using BayatGames.SaveGameFree;
using PlayerPrefsSavingMethods;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLevelScript : MonoBehaviour
{
    public LevelData levelData;
    public string identifier;
    public bool saving = false;
    public bool loading = false;
    public int numbersLevelObj = 0;

    void Awake()
    {
        identifier = "ambientBallSavedLevelData_" + SceneManager.GetActiveScene().buildIndex;

        if (!SaveLoadData.GetFirstLevelLaunch())
        {
            loading = true;
        }
    }

    void Start()
    {
       numbersLevelObj = DefineLevelDataArrays();

        if (loading)
        {
            Load();
            loading = false;
        }
    }

    void Update()
    {
        if (saving)
        {
            StartCoroutine(SaveLevelAsync());
            saving = false;
        }
    }

    #region Сохраняемый класс
    [System.Serializable]
    public class LevelData
    {
        #region Массивы данных объектов активного окружения уровня
        public Vector3[] positions;
        public Quaternion[] rotations;
        public bool[] activities;
        public Vector3[] velocities;
        public Vector3[] angularVelocities;

        public int textProgress;
        public int starsScore;
        #endregion

    }
    #endregion

    /// <summary>
    /// Сохрание данных объектов уровня
    /// </summary>
    IEnumerator SaveLevelAsync()
    {
        Transform[] levelObjList = GetLevelObgList();
        int i = 0;
        foreach (Transform child in levelObjList)
        {
            levelData.positions[i] = child.position;
            levelData.rotations[i] = child.rotation;
            levelData.activities[i] = child.gameObject.activeSelf;

            if (child.GetComponent<Rigidbody>() && !child.GetComponent<Rigidbody>().isKinematic)
            {
                levelData.velocities[i] = child.GetComponent<Rigidbody>().velocity;
                levelData.angularVelocities[i] = child.GetComponent<Rigidbody>().angularVelocity;
            }

            i++;
        }

        levelData.textProgress = SaveLoadData.GetTextProgress();
        levelData.starsScore = SaveLoadData.GetStarsScore(SceneManager.GetActiveScene().buildIndex);

        yield return new WaitForEndOfFrame(); // Ждем конец кадра, чтобы избежать статтеринга

        SaveGame.Save<LevelData>(identifier, levelData);
        Debug.Log("level_saved!");
    }

    /// <summary>
    /// Загрузка данных объектов уровня
    /// </summary>
    public void Load()
    {
        //numbersLevelObj = DefineLevelDataArrays();
        levelData = SaveGame.Load<LevelData>(
            identifier,
            new LevelData());

        Transform[] levelObjList = GetLevelObgList();
        int i = 0;
        foreach (Transform child in levelObjList)
        {
            child.position = levelData.positions[i];
            //Debug.Log(child.position);
            child.rotation = levelData.rotations[i];
            //Debug.Log(child.rotation);
            child.gameObject.SetActive(levelData.activities[i]);
            //Debug.Log(child.gameObject.activeSelf);
            if (child.GetComponent<Rigidbody>())
            {
                if (child.GetComponent<Rigidbody>().isKinematic == false)
                {
                    child.GetComponent<Rigidbody>().velocity = levelData.velocities[i];
                    child.GetComponent<Rigidbody>().angularVelocity = levelData.angularVelocities[i];
                }
            }
            i++;
        }
        SaveLoadData.SetTextProgress(levelData.textProgress);
        SaveLoadData.SetStarsScore(SceneManager.GetActiveScene().buildIndex, levelData.starsScore);
        Debug.Log("level_loaded!");
    }

    /// <summary>
    /// Вычисляет количество сохраняемых объектов
    /// </summary>
    public int DefineLevelDataArrays()
    {
        Transform[] levelObjList = GetLevelObgList();
        foreach (Transform child in levelObjList)
        {
            numbersLevelObj++;
        }
        Debug.Log("Numbers of saved objects: " + numbersLevelObj);
        Array.Resize(ref levelData.positions, numbersLevelObj);
        Array.Resize(ref levelData.rotations, numbersLevelObj);
        Array.Resize(ref levelData.activities, numbersLevelObj);
        Array.Resize(ref levelData.velocities, numbersLevelObj);
        Array.Resize(ref levelData.angularVelocities, numbersLevelObj);
        return numbersLevelObj;
    }
    /// <summary>
    /// Возвращает список сохраняемых объектов уровня
    /// </summary>
    public Transform[] GetLevelObgList()
    {
        Transform[] levelObjList = GetComponentsInChildren<Transform>(true);
        return levelObjList;
    }
}