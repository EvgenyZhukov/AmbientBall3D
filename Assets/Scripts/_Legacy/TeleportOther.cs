using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Телепортирует все кроме игрока
/// </summary>
public class TeleportOther : MonoBehaviour
{

    public bool teleported = false;     // Проверяет, было ли недавно телепортирование, что бы не попасть в замкнутый цикл
    public TeleportOther target;       // Соединяет телепорты
    public GameObject explode;

    /// <summary>
    /// Вход, срабатывает при соприкосновении коллайдеров
    /// </summary>
    /// <param name="other">Коллайдер объекта попавшего в телепорт</param>
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            explode.SetActive(false);
            explode.SetActive(true);

            if (!teleported)    // Если телепортирования только что не было
            {
                target.teleported = true;                                                   // Выключает телепорт места назначения
                other.gameObject.transform.position = target.gameObject.transform.position; // Присваеваем телепортируемому 
                                                                                            // объекту позицию телепорта назначения

            }
        }
    }

    /// <summary>
    /// Включает телепорты при выходе из коллайдера
    /// </summary>
    /// <param name="other">Коллайдер объекта попавшего в телепорт</param>
    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            teleported = false;
        }
    }
}
