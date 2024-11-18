using UnityEngine;

public class MovedColumnSimple : MonoBehaviour
{
    [SerializeField] private bool activated = false;
    [SerializeField] private bool pressed = false;
    public float speed = 1f;               
    private float timer = 0f;
    public float range = 2f;

    /// <summary>
    /// Активирует перемещение объекта при входе игроком в коллайдер
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
        // Процесс передвижения
        if (activated && !pressed)
        {
                transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
                timer += 1f * Time.deltaTime;
        }
        if (timer >= range)
        {
            pressed = true;
        }
    }
}