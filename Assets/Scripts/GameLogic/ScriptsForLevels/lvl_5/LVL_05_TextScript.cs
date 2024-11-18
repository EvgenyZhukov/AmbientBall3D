using PlayerPrefsSavingMethods;
using TMPro;
using UnityEngine;
/// <summary>
/// Частный случай логики поведения текста на конкретном уровне, необходим контроль переменных получаемых от родителя
/// </summary>
public class LVL_05_TextScript : LevelTextScript
{
    public TMP_Text b, i, g, m, a, s, s2, f, o, r, m2;
    public TMP_Text ar1, ar2, ar2b, ar3, ar4, ar4b, ar5, ar6, ar6b;
    public TMP_Text ww, ii, nn, dd;

    void Start()
    {
        progression = SaveLoadData.GetTextProgress();
    }

    void FixedUpdate()
    {
        if (textOnLaunch)
        {
            if (b.transform.parent.gameObject.activeSelf) FaderTextOn(b);
            if (i.transform.parent.gameObject.activeSelf) FaderTextOn(i);
            if (g.transform.parent.gameObject.activeSelf) FaderTextOn(g);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOn(m);
            if (a.transform.parent.gameObject.activeSelf) FaderTextOn(a);
            if (s.transform.parent.gameObject.activeSelf) FaderTextOn(s);
            if (s2.transform.parent.gameObject.activeSelf) FaderTextOn(s2);
            if (f.transform.parent.gameObject.activeSelf) FaderTextOn(f);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOn(o);
            if (r.transform.parent.gameObject.activeSelf) FaderTextOn(r);
            if (m2.transform.parent.gameObject.activeSelf) FaderTextOn(m2);
            if (ar1.transform.parent.gameObject.activeSelf) FaderTextOn(ar1);

            if (ww.transform.parent.gameObject.activeSelf) FaderTextOn(ww);
            if (ii.transform.parent.gameObject.activeSelf) FaderTextOn(ii);
            if (nn.transform.parent.gameObject.activeSelf) FaderTextOn(nn);
            if (dd.transform.parent.gameObject.activeSelf) FaderTextOn(dd);
        }

        if (textOff)
        { 
          TextLogicLevel_05();
        }
    }

    private void TextLogicLevel_05()
    {
        if (progression >= 1)
        {
            if (b.transform.parent.gameObject.activeSelf) FaderTextOff(b);
            if (i.transform.parent.gameObject.activeSelf) FaderTextOff(i);
            if (g.transform.parent.gameObject.activeSelf) FaderTextOff(g);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOff(m);
            if (a.transform.parent.gameObject.activeSelf) FaderTextOff(a);
            if (s.transform.parent.gameObject.activeSelf) FaderTextOff(s);
            if (s2.transform.parent.gameObject.activeSelf) FaderTextOff(s2);
            if (f.transform.parent.gameObject.activeSelf) FaderTextOff(f);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOff(o);
            if (r.transform.parent.gameObject.activeSelf) FaderTextOff(r);
            if (m2.transform.parent.gameObject.activeSelf) FaderTextOff(m2);
            if (ar1.transform.parent.gameObject.activeSelf) FaderTextOff(ar1);
        }
        
        if (progression >= 2)
        {
            if (ar2.transform.parent.gameObject.activeSelf) FaderTextOff(ar2);
            if (ar2b.transform.parent.gameObject.activeSelf) FaderTextOff(ar2b);
        }
        if (progression >= 3)
        {
            if (ar3.transform.parent.gameObject.activeSelf) FaderTextOff(ar3);
        }
        if (progression >= 4)
        {
            if (ar4.transform.parent.gameObject.activeSelf) FaderTextOff(ar4);
            if (ar4b.transform.parent.gameObject.activeSelf) FaderTextOff(ar4b);
        }
        if (progression >= 5)
        {
            if (ar5.transform.parent.gameObject.activeSelf) FaderTextOff(ar5);
        }
        if (progression >= 6)
        {
            if (ar6.transform.parent.gameObject.activeSelf) FaderTextOff(ar6);
            if (ar6b.transform.parent.gameObject.activeSelf) FaderTextOff(ar6b);
        }
    }
}
