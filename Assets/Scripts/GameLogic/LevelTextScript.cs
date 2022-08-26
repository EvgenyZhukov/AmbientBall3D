using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTextScript : MonoBehaviour
{
    private float changeStep = 0.02f; // скорость исчезновения текста
    public int progression; //получает из плеер префс
    public bool textOff = false; //получает из объекта сцены переключателя
    public bool getTextProgress = false;
    public bool textOnLaunch = true;

    /// <summary>
    /// Меняет альфу текста, делает его видимым
    /// </summary>
    public void FaderTextOn(TMP_Text textObj)
    {
        if (textObj.color.a < 1)
        {
            float aText = textObj.color.a;
            aText += changeStep;
            textObj.color = new Color(textObj.color.r, textObj.color.g, textObj.color.b, aText);
        }
        else if (textObj.color.a >= 1)
        {
            textOnLaunch = false;
        }
    }
    /// <summary>
    /// Меняет альфу текста, делает его прозрачным
    /// </summary>
    public void FaderTextOff(TMP_Text textObj)
    {
        if (textObj.color.a > 0)
        {
            float aText = textObj.color.a;
            aText -= changeStep;
            textObj.color = new Color(textObj.color.r, textObj.color.g, textObj.color.b, aText);
        }
        else if (textObj.color.a <= 0)
        {
            textOff = false;
            textObj.transform.parent.gameObject.SetActive(false);
        }
    }
}
