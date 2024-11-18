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
        // Находим все объекты TMP_Text на текущем объекте
        TMP_Text[] tmpTexts = GetComponentsInChildren<TMP_Text>(true);

        // Инициализируем массив структур
        textObjects = new TextData[tmpTexts.Length];

        // Заполняем массив структур
        for (int i = 0; i < tmpTexts.Length; i++)
        {
            textObjects[i].textObject = tmpTexts[i];
            textObjects[i].isHidden = false; // По умолчанию ничего не скрыто
        }
    }
    // Метод для скрытия объектов, если их родитель активен
    public void HideTextObjects()
    {

        for (int i = 0; i < textObjects.Length; i++)
        {
            // Проверяем, что родитель активен
            if (textObjects[i].textObject.transform.parent.gameObject.activeSelf)
            {
                // Скрываем объект и запоминаем его
                textObjects[i].textObject.transform.parent.gameObject.SetActive(false);
                textObjects[i].isHidden = true;
            }
        }
    }
    // Метод для отображения ранее скрытых объектов
    public void ShowHiddenTextObjects()
    {

        for (int i = 0; i < textObjects.Length; i++)
        {
            // Проверяем, что родитель был ранее скрыт
            if (textObjects[i].isHidden)
            {
                // Показываем объект
                textObjects[i].textObject.transform.parent.gameObject.SetActive(true);
                textObjects[i].isHidden = false;
            }
        }
    }
}
