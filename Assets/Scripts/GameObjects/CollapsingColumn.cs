using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingColumn : MonoBehaviour
{
    public Rigidbody platform1;
    public Rigidbody platform2;
    public Rigidbody platform3;
    public Rigidbody platform4;

    public GameObject plate1;
    public GameObject plate2;
    public GameObject plate3;
    public GameObject plate4;

    [SerializeField] private bool activated = false;
    private bool locker = false;
    private float timer = 0f;        // Таймер

    int minForce = 5;
    int maxForce = 7;

    float delta = 0.2f;

    void Start()
    {
        if (platform1.GetComponent<Rigidbody>().velocity.y < -0.1)
        {
            activated = true;
        }
    }

    /// <summary>
    /// Выполняется при выходе игрока из коллайдера платформы
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
        if (activated)
        {
            // Запускает таймер при соприконовении с игроком
            if (timer < 5)
            {
                timer += 1f * Time.deltaTime;
            }
            if (!locker)
            {
                Invoke("PushForce_1", 0.5f);
                Invoke("PushForce_2", 1.5f);
                Invoke("PushForce_3", 2.5f);
                Invoke("PushForce_4", 3.0f);
                locker = true;
            }
            // Обрушение каскадов платформы по таймеру
            if (timer >= 0.5 + delta)
            {
                plate1.SetActive(false);
            }
            if (timer >= 1.5 + delta)
            {
                plate2.SetActive(false);
            }
            if (timer >= 2.5 + delta)
            {
                plate3.SetActive(false);
            }
            if (timer >= 3.0 + delta)
            {
                plate4.SetActive(false);
            }
            // Отключение объекта
            if (platform4.transform.position.y < -100)
            {
                Invoke("SelfDestroy", 0f);
            }
        }
    }

    /// <summary>
    /// Метод падения колонн
    /// </summary>
    private void PushForce_1()
    {
        //Vector3 forcePoint = transform.position;
        //platform1.AddForceAtPosition(transform.forward * 1000 * Random.Range(minForce, maxForce), forcePoint);

        platform1.AddForce(transform.forward * 1000 * Random.Range(minForce, maxForce));
    }

    private void PushForce_2()
    {
        //Vector3 forcePoint = transform.position;
        //platform2.AddForceAtPosition(transform.right * 1000 * Random.Range(minForce, maxForce), forcePoint);

        platform2.AddForce(transform.right * 1000 * Random.Range(minForce, maxForce));
    }

    private void PushForce_3()
    {
        //Vector3 forcePoint = transform.position;
        //platform3.AddForceAtPosition(transform.forward * 1000 * Random.Range(minForce, maxForce), forcePoint);

        platform3.AddForce(transform.forward * 1000 * Random.Range(minForce, maxForce));
    }

    private void PushForce_4()
    {
        //Vector3 forcePoint = transform.position;
        //platform4.AddForceAtPosition(-transform.right * 1000 * Random.Range(minForce, maxForce), forcePoint);

        platform4.AddForce(-transform.right * 1000 * Random.Range(minForce, maxForce));
    }

    /// <summary>
    /// Метод самоуничтожения
    /// </summary>
    private void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
