using PlayerPrefsSavingMethods;
using TMPro;
using UnityEngine;
/// <summary>
/// Частный случай логики поведения текста на конкретном уровне, необходим контроль переменных получаемых от родителя
/// </summary>
public class LVL_08_TextScript : LevelTextScript
{
    public TMP_Text t,e,l,e2,p,o,r,t2,m,a,z,e3;
    public TMP_Text ar;

    void Start()
    {
        progression = SaveLoadData.GetTextProgress();
    }

    void FixedUpdate()
    {
        if (textOnLaunch)
        {
            if (t.transform.parent.gameObject.activeSelf) FaderTextOn(t);
            if (e.transform.parent.gameObject.activeSelf) FaderTextOn(e);
            if (l.transform.parent.gameObject.activeSelf) FaderTextOn(l);
            if (e2.transform.parent.gameObject.activeSelf) FaderTextOn(e2);
            if (p.transform.parent.gameObject.activeSelf) FaderTextOn(p);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOn(o);
            if (r.transform.parent.gameObject.activeSelf) FaderTextOn(r);
            if (t2.transform.parent.gameObject.activeSelf) FaderTextOn(t2);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOn(m);
            if (a.transform.parent.gameObject.activeSelf) FaderTextOn(a);
            if (z.transform.parent.gameObject.activeSelf) FaderTextOn(z);
            if (e3.transform.parent.gameObject.activeSelf) FaderTextOn(e3);
            if (ar.transform.parent.gameObject.activeSelf) FaderTextOn(ar);
        }
        if (textOff)
        { 
          TextLogicLevel_08();
        }
    }
    private void TextLogicLevel_08()
    {
        if (progression >= 1)
        {
            if (t.transform.parent.gameObject.activeSelf) FaderTextOff(t);
            if (e.transform.parent.gameObject.activeSelf) FaderTextOff(e);
            if (l.transform.parent.gameObject.activeSelf) FaderTextOff(l);
            if (e2.transform.parent.gameObject.activeSelf) FaderTextOff(e2);
            if (p.transform.parent.gameObject.activeSelf) FaderTextOff(p);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOff(o);
            if (r.transform.parent.gameObject.activeSelf) FaderTextOff(r);
            if (t2.transform.parent.gameObject.activeSelf) FaderTextOff(t2);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOff(m);
            if (a.transform.parent.gameObject.activeSelf) FaderTextOff(a);
            if (z.transform.parent.gameObject.activeSelf) FaderTextOff(z);
            if (e3.transform.parent.gameObject.activeSelf) FaderTextOff(e3);
            if (ar.transform.parent.gameObject.activeSelf) FaderTextOff(ar);
        }
    }
}
