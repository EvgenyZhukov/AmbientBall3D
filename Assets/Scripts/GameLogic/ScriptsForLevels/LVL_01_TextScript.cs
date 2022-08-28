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
            if (m.transform.parent.gameObject.activeSelf) FaderTextOn(m);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOn(o);
            if (v.transform.parent.gameObject.activeSelf) FaderTextOn(v);
            if (e.transform.parent.gameObject.activeSelf) FaderTextOn(e);
            if (ar1.transform.parent.gameObject.activeSelf) FaderTextOn(ar1);
        }

        if (textOff)
        { 
          TextLogicLevel_01();
        }
    }

    private void TextLogicLevel_01()
    {
        if (progression >= 1)
        {
            if (m.transform.parent.gameObject.activeSelf) FaderTextOff(m);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOff(o);
            if (v.transform.parent.gameObject.activeSelf) FaderTextOff(v);
            if (e.transform.parent.gameObject.activeSelf) FaderTextOff(e);
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
        }
        if (progression >= 5)
        {
            if (ar5.transform.parent.gameObject.activeSelf) FaderTextOff(ar5);
        }
        if (progression >= 6)
        {
            if (ar6.transform.parent.gameObject.activeSelf) FaderTextOff(ar6);
        }
        if (progression >= 7)
        {
            if (ar7.transform.parent.gameObject.activeSelf) FaderTextOff(ar7);
            if (ar8.transform.parent.gameObject.activeSelf) FaderTextOff(ar8);
        }
        if (progression >= 8)
        {
            if (ar9.transform.parent.gameObject.activeSelf) FaderTextOff(ar9);
        }
        if (progression >= 9)
        {
            if (ar10.transform.parent.gameObject.activeSelf) FaderTextOff(ar10);
        }
    }
}
