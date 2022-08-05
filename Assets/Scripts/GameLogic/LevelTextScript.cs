using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTextScript : MonoBehaviour
{
    private float changeStep = 0.04f; // скорость исчезновения текста
    public int progression; //получает из плеер префс
    public bool change = false; //получает из объекта сцены переключателя
    public bool getTextProgress = false;
    private int sceneNumber;

    [Header("Уровень 1")]
    public TMP_Text m;
    public TMP_Text o;
    public TMP_Text v;
    public TMP_Text e;
    public TMP_Text ar1, ar2, ar3, ar4, ar5, ar6, ar7, ar8, ar9, ar10;
    

    void Start()
    {
        progression = SaveLoadData.GetTextProgress();
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    void FixedUpdate()
    {
        if (change)
        {
            switch (sceneNumber)
            {
                case 1:
                    TextLogicLevel_01();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Меняет альфу текста (делает его прозрачным и наоборот)
    /// </summary>
    private void FaderText(TMP_Text textObj)
    {
        if (textObj.color.a > 0)
        {
            float aText = textObj.color.a;
            aText -= changeStep;
            textObj.color = new Color(textObj.color.r, textObj.color.g, textObj.color.b, aText);
        }
        else if (textObj.color.a <= 0)
        {
            change = false;
            textObj.transform.parent.gameObject.SetActive(false);
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
