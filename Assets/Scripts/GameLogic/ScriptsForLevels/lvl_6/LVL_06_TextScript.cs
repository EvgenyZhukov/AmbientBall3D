using PlayerPrefsSavingMethods;
using TMPro;
using UnityEngine;
/// <summary>
/// Частный случай логики поведения текста на конкретном уровне, необходим контроль переменных получаемых от родителя
/// </summary>
public class LVL_06_TextScript : LevelTextScript
{
    public TMP_Text n, o, m2, o2, r, e2, a2, r2, r3, o3, w, s;
    public TMP_Text ar;

    void Start()
    {
        progression = SaveLoadData.GetTextProgress();
    }

    void FixedUpdate()
    {
        if (textOnLaunch)
        {
            if (n.transform.parent.gameObject.activeSelf) FaderTextOn(n);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOn(o);
            if (m2.transform.parent.gameObject.activeSelf) FaderTextOn(m2);
            if (o2.transform.parent.gameObject.activeSelf) FaderTextOn(o2);
            if (r.transform.parent.gameObject.activeSelf) FaderTextOn(r);
            if (e2.transform.parent.gameObject.activeSelf) FaderTextOn(e2);
            if (a2.transform.parent.gameObject.activeSelf) FaderTextOn(a2);
            if (r2.transform.parent.gameObject.activeSelf) FaderTextOn(r2);
            if (r3.transform.parent.gameObject.activeSelf) FaderTextOn(r3);
            if (o3.transform.parent.gameObject.activeSelf) FaderTextOn(o3);
            if (w.transform.parent.gameObject.activeSelf) FaderTextOn(w);
            if (s.transform.parent.gameObject.activeSelf) FaderTextOn(s);
            if (ar.transform.parent.gameObject.activeSelf) FaderTextOn(ar);
        }
        if (textOff)
        { 
          TextLogicLevel_06();
        }
    }
    private void TextLogicLevel_06()
    {
        if (progression >= 1)
        {
            if (n.transform.parent.gameObject.activeSelf) FaderTextOff(n);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOff(o);
            if (m2.transform.parent.gameObject.activeSelf) FaderTextOff(m2);
            if (o2.transform.parent.gameObject.activeSelf) FaderTextOff(o2);
            if (r.transform.parent.gameObject.activeSelf) FaderTextOff(r);
            if (e2.transform.parent.gameObject.activeSelf) FaderTextOff(e2);
            if (a2.transform.parent.gameObject.activeSelf) FaderTextOff(a2);
            if (r2.transform.parent.gameObject.activeSelf) FaderTextOff(r2);
            if (r3.transform.parent.gameObject.activeSelf) FaderTextOff(r3);
            if (o3.transform.parent.gameObject.activeSelf) FaderTextOff(o3);
            if (w.transform.parent.gameObject.activeSelf) FaderTextOff(w);
            if (s.transform.parent.gameObject.activeSelf) FaderTextOff(s);
            if (ar.transform.parent.gameObject.activeSelf) FaderTextOff(ar);
        }
    }
}
