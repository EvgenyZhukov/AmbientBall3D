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

    /// <summary>
    /// Меняет альфу текста (делает его прозрачным и наоборот)
    /// </summary>
    public void FaderText(TMP_Text textObj)
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
}
