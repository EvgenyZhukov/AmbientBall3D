using PlayerPrefsSavingMethods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Частный случай логики поведения текста на конкретном уровне, необходим контроль переменных получаемых от родителя
/// </summary>
public class LVL_04_TextScript : LevelTextScript
{
    public TMP_Text l, o, n, g, j, u, m, p, f, o2, r, m2;
    public TMP_Text ar1;

    void Start()
    {
        progression = SaveLoadData.GetTextProgress();
    }

    void FixedUpdate()
    {
        if (textOnLaunch)
        {
            if (l.transform.parent.gameObject.activeSelf) FaderTextOn(l);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOn(o);
            if (n.transform.parent.gameObject.activeSelf) FaderTextOn(n);
            if (g.transform.parent.gameObject.activeSelf) FaderTextOn(g);
            if (j.transform.parent.gameObject.activeSelf) FaderTextOn(j);
            if (u.transform.parent.gameObject.activeSelf) FaderTextOn(u);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOn(m);
            if (p.transform.parent.gameObject.activeSelf) FaderTextOn(p);
            if (f.transform.parent.gameObject.activeSelf) FaderTextOn(f);
            if (o2.transform.parent.gameObject.activeSelf) FaderTextOn(o2);
            if (r.transform.parent.gameObject.activeSelf) FaderTextOn(r);
            if (m2.transform.parent.gameObject.activeSelf) FaderTextOn(m2);
            if (ar1.transform.parent.gameObject.activeSelf) FaderTextOn(ar1);
        }

        if (textOff)
        { 
          TextLogicLevel_04();
        }
    }

    private void TextLogicLevel_04()
    {
        if (progression >= 1)
        {
            if (l.transform.parent.gameObject.activeSelf) FaderTextOff(l);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOff(o);
            if (n.transform.parent.gameObject.activeSelf) FaderTextOff(n);
            if (g.transform.parent.gameObject.activeSelf) FaderTextOff(g);
            if (j.transform.parent.gameObject.activeSelf) FaderTextOff(j);
            if (u.transform.parent.gameObject.activeSelf) FaderTextOff(u);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOff(m);
            if (p.transform.parent.gameObject.activeSelf) FaderTextOff(p);
            if (f.transform.parent.gameObject.activeSelf) FaderTextOff(f);
            if (o2.transform.parent.gameObject.activeSelf) FaderTextOff(o2);
            if (r.transform.parent.gameObject.activeSelf) FaderTextOff(r);
            if (m2.transform.parent.gameObject.activeSelf) FaderTextOff(m2);
            if (ar1.transform.parent.gameObject.activeSelf) FaderTextOff(ar1);
        }
    }
}
