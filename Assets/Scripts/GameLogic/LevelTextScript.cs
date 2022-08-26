using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTextScript : MonoBehaviour
{
    private float changeStep = 0.02f; // �������� ������������ ������
    public int progression; //�������� �� ����� �����
    public bool textOff = false; //�������� �� ������� ����� �������������
    public bool getTextProgress = false;
    public bool textOnLaunch = true;

    /// <summary>
    /// ������ ����� ������, ������ ��� �������
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
    /// ������ ����� ������, ������ ��� ����������
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
