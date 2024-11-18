using PlayerPrefsSavingMethods;
using TMPro;
using UnityEngine;
/// <summary>
/// Частный случай логики поведения текста на конкретном уровне, необходим контроль переменных получаемых от родителя
/// </summary>
public class LVL_07_TextScript : LevelTextScript
{
    public TMP_Text t,r,a,n,s,f,o,r2,m,l,e,v,e2,l2;
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
            if (r.transform.parent.gameObject.activeSelf) FaderTextOn(r);
            if (a.transform.parent.gameObject.activeSelf) FaderTextOn(a);
            if (n.transform.parent.gameObject.activeSelf) FaderTextOn(n);
            if (s.transform.parent.gameObject.activeSelf) FaderTextOn(s);
            if (f.transform.parent.gameObject.activeSelf) FaderTextOn(f);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOn(o);
            if (r2.transform.parent.gameObject.activeSelf) FaderTextOn(r2);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOn(m);
            if (l.transform.parent.gameObject.activeSelf) FaderTextOn(l);
            if (e.transform.parent.gameObject.activeSelf) FaderTextOn(e);
            if (v.transform.parent.gameObject.activeSelf) FaderTextOn(v);
            if (e2.transform.parent.gameObject.activeSelf) FaderTextOn(e2);
            if (l2.transform.parent.gameObject.activeSelf) FaderTextOn(l2);
            if (ar.transform.parent.gameObject.activeSelf) FaderTextOn(ar);
        }
        if (textOff)
        { 
          TextLogicLevel_07();
        }
    }
    private void TextLogicLevel_07()
    {
        if (progression >= 1)
        {
            if (t.transform.parent.gameObject.activeSelf) FaderTextOff(t);
            if (r.transform.parent.gameObject.activeSelf) FaderTextOff(r);
            if (a.transform.parent.gameObject.activeSelf) FaderTextOff(a);
            if (n.transform.parent.gameObject.activeSelf) FaderTextOff(n);
            if (s.transform.parent.gameObject.activeSelf) FaderTextOff(s);
            if (f.transform.parent.gameObject.activeSelf) FaderTextOff(f);
            if (o.transform.parent.gameObject.activeSelf) FaderTextOff(o);
            if (r2.transform.parent.gameObject.activeSelf) FaderTextOff(r2);
            if (m.transform.parent.gameObject.activeSelf) FaderTextOff(m);
            if (l.transform.parent.gameObject.activeSelf) FaderTextOff(l);
            if (e.transform.parent.gameObject.activeSelf) FaderTextOff(e);
            if (v.transform.parent.gameObject.activeSelf) FaderTextOff(v);
            if (e2.transform.parent.gameObject.activeSelf) FaderTextOff(e2);
            if (l2.transform.parent.gameObject.activeSelf) FaderTextOff(l2);
            if (ar.transform.parent.gameObject.activeSelf) FaderTextOff(ar);
        }
    }
}
