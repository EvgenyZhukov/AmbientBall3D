using UnityEngine;

public class SwitcherColumnMover : MonoBehaviour
{
    [Header("Передвигаемый объект")]
    public MovedObject movedObject;
    [Header("Противоположенный выключатель")]
    public SwitcherColumnMover switcherColumnMover;

    public bool activated;  //используется в других скриптах
    [SerializeField] private bool pressed;
    private float speed = 1f;               // Скорость нажатия кнопки
    private float timer = 0f;
    public GameObject column;
    public GameObject activator;
    public Material buttonMaterialOn;
    public Material buttonMaterialOff;
    [SerializeField] private bool changed = false;
    [SerializeField] private bool locker = true;
    public int numberButton;
    public bool starter = true;
    public AudioSource buttonSound;
    bool soundReady = true;

    /// <summary>
    /// Активирует кнопку при входе в коллайдер игрока
    /// </summary>
    /// <param name="other">Коллайдер игрока</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activator.SetActive(true);
            activated = true;
            switcherColumnMover.locker = false;

            movedObject.switcher = numberButton;
            movedObject.inMove = true;

            if (soundReady)
            {
                buttonSound.Play();
                soundReady = false;
            }
        }
    }

    void Update()
    {
        if (starter)
        {
            if (activator.activeSelf)
            {
                activated = true;
                pressed = true;
                movedObject.switcher = numberButton;
            }
            else
            {
                activated = false;
                pressed = false;
            }
            starter = false;
        }

        if (switcherColumnMover.activated && !locker)
        {
            activated = false;
            activator.SetActive(false);
            locker = true;
        }

        if (activated && !pressed)
        {
            transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
            timer += 1f * Time.deltaTime;
        }
        // Процесс отжатия кнопки
        else if (!activated && pressed)
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
