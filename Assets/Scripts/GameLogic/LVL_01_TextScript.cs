using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (change)
        { 
          TextLogicLevel_01();
        }
    }

    private void TextLogicLevel_01()
    {
        switch (progression)
        {
            case 1:
                FaderText(m);
                FaderText(o);
                FaderText(v);
                FaderText(e);
                FaderText(ar1);
                break;
            case 2:
                FaderText(ar2);
                break;
            case 3:
                FaderText(ar3);
                break;
            case 4:
                FaderText(ar4);
                break;
            case 5:
                FaderText(ar5);
                break;
            case 6:
                FaderText(ar6);
                break;
            case 7:
                FaderText(ar7);
                FaderText(ar8);
                break;
            case 8:
                FaderText(ar9);
                break;
            case 9:
                FaderText(ar10);
                break;
            default:
                break;
        }
    }
}
