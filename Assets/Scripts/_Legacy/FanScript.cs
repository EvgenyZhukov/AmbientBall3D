using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{

    public float force = 15;

    /// <summary>
    /// Сдвигание игрока, срабатывает при соприкосновении коллайдеров
    /// </summary>
    /// <param name="other">Коллайдер объекта попавшего в поток</param>
    void OnTriggerStay(Collider other)
    {
        Rigidbody player;
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Rigidbody>();
            player.AddForce(transform.forward * force);  // Прикладывает сдвигающую силу
        }
    }
}
