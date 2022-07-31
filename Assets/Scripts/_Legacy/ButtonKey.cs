using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonKey : MonoBehaviour
{
    public bool activated = false;
    public bool pressed = false;
    float speed = 1f;               // Скорость нажатия кнопки
    public float timer = 0f;
    //public GameObject explode;
    public Material buttonMaterial;


    /// <summary>
    /// Активирует кнопку при входе в коллайдер игрока
    /// </summary>
    /// <param name="other">Коллайдер игрока</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            activated = true;
        }
    }

    void Awake()
    {
        buttonMaterial = GetComponent<Renderer>().material;
    }

    void FixedUpdate()
    {
        // Процесс вдавливания кнопки
        if (activated == true && pressed == false)
        {
            transform.position -= new Vector3(0.0f, 0.5f * speed * Time.fixedDeltaTime, 0.0f);
            timer += 1f * Time.fixedDeltaTime;
        }
        if (timer >= 1)
        {
            pressed = true;
            //explode.SetActive(false);
        }

        // Отключение свечения кнопки
        if (pressed == true)
        {
            buttonMaterial.DisableKeyword("_EMISSION");
        }
    }
}
