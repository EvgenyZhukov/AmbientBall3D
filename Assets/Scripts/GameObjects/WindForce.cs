using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    public float force = 5;

    /// <summary>
    /// Сдвигающая сила, срабатывает при соприкосновении коллайдеров
    /// </summary>
    /// <param name="other">Коллайдер объекта попавшего в лифт</param>
    void OnTriggerStay(Collider other)
    {
        Rigidbody player;
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Rigidbody>();
            player.AddForce(Vector3.right * force);  // Прикладывает силу
        }
    }
}