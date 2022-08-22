using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Частный случай логики поведения текста на конкретном уровне, необходим контроль переменных получаемых от родителя
/// </summary>
public class LVL_01_TextScript : LevelTextScript
{
    [Header("Уровень 1")]
    public TMP_Text m;
    public TMP_Text o;
    public TMP_Text v;
    public TMP_Text e;
    public TMP_Text ar1, ar2, ar3, ar4, ar5, ar6, ar7, ar8, ar9, ar10;

    void Start()
    {
        progression = SaveLoadData.GetTextProgress();
    }

    void FixedUpdate()
    {
        if (textOnLaunch)
        {
            if (m) FaderTextOn(m);
            if (o) FaderTextOn(o);
            if (v) FaderTextOn(v);
            if (e) FaderTextOn(e);
            if (ar1) FaderTextOn(ar1);
        }

        if (textOff)
        { 
          TextLogicLevel_01();
        }
    }

    private void TextLogicLevel_01()
    {
        switch (progression)
        {
            case 1:
                FaderTextOff(m);
                FaderTextOff(o);
                FaderTextOff(v);
                FaderTextOff(e);
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
