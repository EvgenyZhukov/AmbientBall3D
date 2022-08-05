using BayatGames.SaveGameFree;
using PlayerPrefsSavingMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveGameScript : MonoBehaviour
{
    public GameData gameData;
    public string identifier = "ambientBallSavedGameData";
    public bool saving = false;
    public bool loading = false;

    void Awake()
    {
        if (!SaveLoadData.GetInProgressTemp() && SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadGameData();
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {

        }
    }
    void Update()
    {
        if (saving)
        {
            Invoke("SaveGameData", 0f);
            saving = false;
        }
    }

    #region Сохраняемый класс
    [System.Serializable]
    public class GameData
    {
        public int lives;
        public float xPos, yPos, zPos;
        public int maxLevel;
        public bool inProgress;
        public int[] stars = new int[10];
        public int starsTotal;
    }
    #endregion

    private void SaveGameData()
    {
        gameData.lives = SaveLoadData.GetLives();
        SaveLoadData.LoadCoordinates(out gameData.xPos, out gameData.yPos, out gameData.zPos);
        gameData.maxLevel = SaveLoadData.GetLevelProgress();
        gameData.inProgress = SaveLoadData.GetInProgress();

        int stars = 0;
        int i = 0;
        foreach (int item in gameData.stars)
        {
            gameData.stars[i] = SaveLoadData.GetStars(i);
            stars += SaveLoadData.GetStars(i);
            i++;
        }
        SaveLoadData.SetStarsTotal(stars);
        gameData.starsTotal = stars;

        SaveGame.Save<GameData>(identifier, gameData);
        Debug.Log("game_saved!");
    }
    private void LoadGameData()
    {
        gameData = SaveGame.Load<GameData>(
            identifier,
            new GameData());

        SaveLoadData.SetLives(gameData.lives);
        SaveLoadData.SaveCoordinates(gameData.xPos, gameData.yPos, gameData.zPos);
        SaveLoadData.SetLevelProgress(gameData.maxLevel);
        SaveLoadData.SetInProgress(gameData.inProgress);

        int i = 0;
        foreach (int item in gameData.stars)
        {
            SaveLoadData.SetStars(i, gameData.stars[i]);
            i++;
        }
        SaveLoadData.SetStarsTotal(gameData.starsTotal);

        Debug.Log("game_loaded!");
    }
}