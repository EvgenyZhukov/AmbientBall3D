using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{

    public bool activated = false;
    public bool pressed = false;
    float speed = 1f;               // Скорость нажатия кнопки
    public float timer = 0f;
    public GameObject column;
    public GameObject activator;
    public Material buttonMaterialOff;
    public Material buttonMaterialOn;
    [SerializeField] private bool changed = false;
    public AudioSource buttonSound;
    bool soundReady = true;

    private void Start()
    {
        if (activator.activeSelf)
        {
            activated = true;
            pressed = true;
        }
        else
        {
            activated = false;
            pressed = false;
        }
    }

    /// <summary>
    /// Активирует кнопку при входе в коллайдер игрока
    /// </summary>
    /// <param name="other">Коллайдер игрока</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activator.SetActive(true);
            activated = true;

            if (soundReady)
            {
                buttonSound.Play();
                soundReady = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activated && !pressed) 
            {
                soundReady = true;
            }
        }
    }

    void FixedUpdate()
    {
        // Процесс нажатия кнопки
        if (activated && !pressed)
        {
            transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
            timer += 1f * Time.deltaTime;
        }
        // Процесс отжатия кнопки
        if (!activated && pressed)
        {
            transform.position += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
            timer += 1f * Time.deltaTime;
        }
        if (timer >= 1)
        {
            changed = false;
            pressed = !pressed;
            timer = 0;
        }

        if (!changed)
        {
            // Отключение свечения кнопки
            if (pressed)
            {
                column.GetComponent<MeshRenderer>().material = buttonMaterialOff;
                changed = true;
                soundReady = false;
            }
            // Включение свечения кнопки
            else if (!pressed)
            {
                column.GetComponent<MeshRenderer>().material = buttonMaterialOn;
                changed = true;
                soundReady = true;
            }
        }
    }
}
