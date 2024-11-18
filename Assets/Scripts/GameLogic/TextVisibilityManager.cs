using TMPro;
using UnityEngine;

public class TextVisibilityManager : MonoBehaviour
{
    private TextData[] textObjects;

    [System.Serializable]
    public struct TextData
    {
        public TMP_Text textObject;
        public bool isHidden;
    }

    void Start()
    {
        // ������� ��� ������� TMP_Text �� ������� �������
        TMP_Text[] tmpTexts = GetComponentsInChildren<TMP_Text>(true);

        // �������������� ������ ��������
        textObjects = new TextData[tmpTexts.Length];

        // ��������� ������ ��������
        for (int i = 0; i < tmpTexts.Length; i++)
        {
            textObjects[i].textObject = tmpTexts[i];
            textObjects[i].isHidden = false; // �� ��������� ������ �� ������
        }
    }
    // ����� ��� ������� ��������, ���� �� �������� �������
    public void HideTextObjects()
    {

        for (int i = 0; i < textObjects.Length; i++)
        {
            // ���������, ��� �������� �������
            if (textObjects[i].textObject.transform.parent.gameObject.activeSelf)
            {
                // �������� ������ � ���������� ���
                textObjects[i].textObject.transform.parent.gameObject.SetActive(false);
                textObjects[i].isHidden = true;
            }
        }
    }
    // ����� ��� ����������� ����� ������� ��������
    public void ShowHiddenTextObjects()
    {

        for (int i = 0; i < textObjects.Length; i++)
        {
            // ���������, ��� �������� ��� ����� �����
            if (textObjects[i].isHidden)
            {
                // ���������� ������
                textObjects[i].textObject.transform.parent.gameObject.SetActive(true);
                textObjects[i].isHidden = false;
            }
        }
    }
}
