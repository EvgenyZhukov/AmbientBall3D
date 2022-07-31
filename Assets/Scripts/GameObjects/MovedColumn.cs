using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedColumn : MonoBehaviour
{

    [SerializeField] private bool activated = false;
    [SerializeField] private bool pressed = false;
    private float speed = 1f;               // Скорость нажатия кнопки
    private float timer = 0f;
    private float range = 2f;
    public GameObject column;

    /// <summary>
    /// Активирует кнопку при входе в коллайдер игрока
    /// </summary>
    /// <param name="other">Коллайдер игрока</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                activated = true;
        }
    }
    
    void Update()
    {
        // Процесс вдавливания кнопки
        if (activated && !pressed)
        {
            transform.position -= new Vector3(0.0f, 0.5f * speed * Time.deltaTime, 0.0f);
            timer += 1f * Time.deltaTime;
        }
        if (timer >= range*2)
        {
            pressed = true;
        }
    }
}
