using PlayerPrefsSavingMethods;
using TMPro;
using UnityEngine;
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
    public TMP_Text ar1, ar2, ar3, ar4, ar5, ar6, ar7, ar8;

    void Start()
    {
        progression = SaveLoadData.GetTextProgress();
    }

    void FixedUpdate()
    {
        if (textOnLaunch)
        {
            if (j.transform.parent.gameObject.activeSelf) FaderTextOn(j);
            if (u.transform.parent.gameObject.activeSelf) FaderTextOn(u);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOn(m);
            if (p.transform.parent.gameObject.activeSelf) FaderTextOn(p);
            if (ar1.transform.parent.gameObject.activeSelf) FaderTextOn(ar1);
        }

        if (textOff)
        { 
          TextLogicLevel_02();
        }
    }

    private void TextLogicLevel_02()
    {
        if (progression >= 1)
        {
            if (j.transform.parent.gameObject.activeSelf) FaderTextOff(j);
            if (u.transform.parent.gameObject.activeSelf) FaderTextOff(u);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOff(m);
            if (p.transform.parent.gameObject.activeSelf) FaderTextOff(p);
            if (ar1.transform.parent.gameObject.activeSelf) FaderTextOff(ar1);
        }
        if (progression >= 2)
        {
            if (ar2.transform.parent.gameObject.activeSelf) FaderTextOff(ar2);
        }
        if (progression >= 3)
        {
            if (ar3.transform.parent.gameObject.activeSelf) FaderTextOff(ar3);
        }
        if (progression >= 4)
        {
            if (ar4.transform.parent.gameObject.activeSelf) FaderTextOff(ar4);
            if (ar5.transform.parent.gameObject.activeSelf) FaderTextOff(ar5);
        }
        if (progression >= 5)
        {
            if (ar6.transform.parent.gameObject.activeSelf) FaderTextOff(ar6);
            if (ar7.transform.parent.gameObject.activeSelf) FaderTextOff(ar7);
        }
        if (progression >= 6)
        {
            if (ar8.transform.parent.gameObject.activeSelf) FaderTextOff(ar8);
        }
    }
}
