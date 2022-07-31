using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityElevatorKey : MonoBehaviour
{
    public GameObject explode;
    public ButtonKey buttonKey;
    public float force;

    public bool activated = false;

    /// <summary>
    /// Подъем, срабатывает при соприкосновении коллайдеров
    /// </summary>
    /// <param name="other">Коллайдер объекта попавшего в лифт</param>
    void OnTriggerStay(Collider other)
    {
        Rigidbody player;
        if (other.CompareTag("Player") && activated == true)
        {
            player = other.GetComponent<Rigidbody>();
            player.AddForce(Vector3.up * force);  // Прикладывает подъемную силу
        }
    }

    void FixedUpdate()
    {
        if (buttonKey.activated == true)
        {
            activated = true;
            explode.SetActive(true);
        }
    }
}
