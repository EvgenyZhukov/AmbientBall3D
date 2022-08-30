using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingColumn : MonoBehaviour
{
    public Rigidbody platform;
    public GameObject plate;
    [SerializeField] private bool activated = false;
    private bool locker = false;
    int minForce = 4;
    int maxForce = 6;

    void Start()
    {
        if (platform.GetComponent<Rigidbody>().velocity.y < -0.1)
        {
            activated = true;
            platform.isKinematic = false;
        }
    }

    /// <summary>
    /// Выполняется при выходе игрока из коллайдера платформы
    /// </summary>
    /// <param name="other">Коллайдер игрока</param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activated = true;
            platform.isKinematic = false;
        }
    }

    void Update()
    {
        if (activated)
        {
            if (!locker)
            {
                Invoke("PushForce", 0.0f);
                Invoke("Fall", 0.2f);
                locker = true;
            }
            if (platform.transform.position.y < -100)
            {
                Invoke("SelfDestroy", 0f);
            }
        }
    }
    private void PushForce()
    {
        //Debug.Log(transform.position);
        Vector3 forcePoint = transform.position;
        platform.AddForceAtPosition(transform.right * 1000 * Random.Range(minForce, maxForce), forcePoint);
    }
    private void Fall()
    {
        plate.SetActive(false);
    }
    private void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
