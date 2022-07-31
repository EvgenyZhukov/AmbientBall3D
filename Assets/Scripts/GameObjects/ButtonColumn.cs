using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColumn : MonoBehaviour
{
    public bool activated = false;  //используется в других скриптах
    [SerializeField] private bool pressed = false;
    private float speed = 1f;               // Скорость нажатия кнопки
    private float timer = 0f;
    public GameObject column;
    public GameObject activator;
    public Material buttonMaterialOn;
    public Material buttonMaterialOff;
    [SerializeField] private bool changed = false;

    /// <summary>
    /// Активирует кнопку при входе в коллайдер игрока
    /// </summary>
    /// <param name="other">Коллайдер игрока</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activator.SetActive(true);
        }
    }
    
    void Update()
    {
        if (activator.activeSelf)
        {
            activated = true;
        }

        // Процесс вдавливания кнопки
        if (activated && !pressed)
        {
            transform.position -= new Vector3(0.0f, 0.5f * speed * Time.deltaTime, 0.0f);
            timer += 1f * Time.deltaTime;
        }
        if (timer >= 1)
        {
            pressed = true;
        }

        // Отключение свечения кнопки
        if (pressed && !changed)
        {
            column.GetComponent<MeshRenderer>().material = buttonMaterialOff;
            changed = true;
        }
    }
}
