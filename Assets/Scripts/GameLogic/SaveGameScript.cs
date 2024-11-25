﻿using BayatGames.SaveGameFree;
using PlayerPrefsSavingMethods;
using System.Collections;
using UnityEngine;
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
    }
    void Update()
    {
        if (saving)
        {
            StartCoroutine(SaveGameAsync());
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
        public int[] stars = new int[9];
        public int starsTotal;
        public int starsEndlessModeTotal;

        public bool continuousTaken;
    }
    #endregion

    IEnumerator SaveGameAsync()
    {
        gameData.lives = SaveLoadData.GetLives();
        SaveLoadData.LoadCoordinates(out gameData.xPos, out gameData.yPos, out gameData.zPos);
        gameData.maxLevel = SaveLoadData.GetLevelProgress();
        gameData.inProgress = SaveLoadData.GetInProgress();

        int stars = 0;
        int endlessModeStars = 0;
        int i = 0;
        foreach (int item in gameData.stars)
        {
            if (i != 9)
            {
                gameData.stars[i] = SaveLoadData.GetStars(i);
                stars += SaveLoadData.GetStars(i);
                i++;
            }
            else
            {
                //gameData.stars[i] = SaveLoadData.GetStarsEndlessMode();
                endlessModeStars = SaveLoadData.GetStarsEndlessModeTotal();
                i++;
            }
        }
        SaveLoadData.SetStarsTotal(stars);
        SaveLoadData.SetStarsEndlessModeTotal(endlessModeStars);
        gameData.starsTotal = stars;
        gameData.starsEndlessModeTotal = endlessModeStars;

        gameData.continuousTaken = SaveLoadData.GetContinuousTaken();

        yield return new WaitForEndOfFrame(); // Ждем конец кадра, чтобы избежать статтеринга

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
        SaveLoadData.SetStarsEndlessModeTotal(gameData.starsEndlessModeTotal);
        SaveLoadData.SetContinuousTaken(gameData.continuousTaken);

        Debug.Log("game_loaded!");
    }
}