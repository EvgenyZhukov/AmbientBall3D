using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Частный случай логики поведения текста на конкретном уровне, необходим контроль переменных получаемых от родителя
/// </summary>
public class LVL_02_TextScript : LevelTextScript
{
    [Header("Уровень 2")]
    public TMP_Text j;
    public TMP_Text u;
    public TMP_Text m;
    public TMP_Text p;
    public TMP_Text ar1, ar2, ar3, ar4, ar5, ar6, ar7, ar8, ar9, ar10;

    void Start()
    {
        progression = SaveLoadData.GetTextProgress();
    }

    void FixedUpdate()
    {
        if (textOnLaunch)
        {
            if (j) FaderTextOn(j);
            if (u) FaderTextOn(u);
            if (m) FaderTextOn(m);
            if (p) FaderTextOn(p);
            if (ar1) FaderTextOn(ar1);
        }

        if (textOff)
        { 
          TextLogicLevel_02();
        }
    }

    private void TextLogicLevel_02()
    {
        switch (progression)
        {
            case 1:
                FaderTextOff(j);
                FaderTextOff(u);
                FaderTextOff(m);
                FaderTextOff(p);
                FaderTextOff(ar1);
                break;
            case 2:
                FaderTextOff(ar2);
                break;
            case 3:
                FaderTextOff(ar3);
                break;
            case 4:
                FaderTextOff(ar4);
                break;
            case 5:
                FaderTextOff(ar5);
                break;
            case 6:
                FaderTextOff(ar6);
                break;
            case 7:
                FaderTextOff(ar7);
                FaderTextOff(ar8);
                break;
            case 8:
                FaderTextOff(ar9);
                break;
            case 9:
                FaderTextOff(ar10);
                break;
            default:
                break;
        }
    }
}
