using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Частный случай логики поведения текста на конкретном уровне, необходим контроль переменных получаемых от родителя
/// </summary>
public class LVL_03_TextScript : LevelTextScript
{
    [Header("Уровень 2")]
    public TMP_Text f;
    public TMP_Text a;
    public TMP_Text s;
    public TMP_Text t;
    public TMP_Text m;
    public TMP_Text o;
    public TMP_Text v;
    public TMP_Text e;
    public TMP_Text ar1, ar2, ar3;

    void Start()
    {
        progression = SaveLoadData.GetTextProgress();
    }

    void FixedUpdate()
    {
        if (textOnLaunch)
        {
            if (f.transform.parent.gameObject.activeSelf) FaderTextOn(f);
            if (a.transform.parent.gameObject.activeSelf) FaderTextOn(a);
            if (s.transform.parent.gameObject.activeSelf) FaderTextOn(s);
            if (t.transform.parent.gameObject.activeSelf) FaderTextOn(t);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOn(m);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOn(o);
            if (v.transform.parent.gameObject.activeSelf) FaderTextOn(v);
            if (e.transform.parent.gameObject.activeSelf) FaderTextOn(e);
            if (ar1.transform.parent.gameObject.activeSelf) FaderTextOn(ar1);
            if (ar2.transform.parent.gameObject.activeSelf) FaderTextOn(ar2);
            if (ar3.transform.parent.gameObject.activeSelf) FaderTextOn(ar3);
        }

        if (textOff)
        { 
          TextLogicLevel_03();
        }
    }

    private void TextLogicLevel_03()
    {
        if (progression >= 1)
        {
            if (f.transform.parent.gameObject.activeSelf) FaderTextOff(f);
            if (a.transform.parent.gameObject.activeSelf) FaderTextOff(a);
            if (s.transform.parent.gameObject.activeSelf) FaderTextOff(s);
            if (t.transform.parent.gameObject.activeSelf) FaderTextOff(t);
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
    }
}
