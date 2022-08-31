using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingColumn : MonoBehaviour
{
    public GameObject column1;
    public GameObject column2;
    public GameObject column3;
    public GameObject column4;

    public Rigidbody platform1;
    public Rigidbody platform2;
    public Rigidbody platform3;
    public Rigidbody platform4;

    [SerializeField] private bool activated = false;
    private bool locker = false;
    private float timer = 0f;        // Таймер

    int minForce = 4;
    int maxForce = 6;

    void Start()
    {
        if (platform1.GetComponent<Rigidbody>().velocity.y < -0.1)
        {
            activated = true; 
            platform1.isKinematic = false;
            platform2.isKinematic = false;
            platform3.isKinematic = false;
            platform4.isKinematic = false;
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
            // Обрушение каскадов платформы по таймеру
            if (timer >= 0.5)
            {
                platform1.isKinematic = false;
            }
            if (timer >= 1.5)
            {
                platform2.isKinematic = false;
            }
            if (timer >= 2.5)
            {
                platform3.isKinematic = false;
            }
            if (timer >= 3.0)
            {
                platform4.isKinematic = false;
            }
            if (!locker)
            {
                Invoke("PushForce_1", 0.51f);
                Invoke("PushForce_2", 1.51f);
                Invoke("PushForce_3", 2.51f);
                Invoke("PushForce_4", 3.01f);
                locker = true;
            }
            // Отключение объекта
            if (gameObject.activeSelf && platform4.transform.position.y < -100)
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
        Vector3 forcePoint = column1.transform.position;
        platform1.AddForceAtPosition(transform.forward * 1000 * Random.Range(minForce, maxForce), forcePoint);
    }
    private void PushForce_2()
    {
        Vector3 forcePoint = column2.transform.position;
        platform2.AddForceAtPosition(transform.right * 1000 * Random.Range(minForce, maxForce), forcePoint);
    }
    private void PushForce_3()
    {
        Vector3 forcePoint = column3.transform.position;
        platform3.AddForceAtPosition(transform.forward * 1000 * Random.Range(minForce, maxForce), forcePoint);
    }
    private void PushForce_4()
    {
        Vector3 forcePoint = column4.transform.position;
        platform4.AddForceAtPosition(-transform.right * 500 * Random.Range(minForce, maxForce), forcePoint);
    }
    /// <summary>
    /// Метод самоуничтожения
    /// </summary>
    private void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
