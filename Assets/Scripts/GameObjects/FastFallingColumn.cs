using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFallingColumn : MonoBehaviour
{
    public Rigidbody platform;
    [SerializeField] private bool activated = false;

    void Start()
    {
        if (platform.GetComponent<Rigidbody>().velocity.y < -0.1)
        {
            activated = true;
            platform.isKinematic = false;
        }
    }

    /// <summary>
    /// Выполняется при входе игрока в коллайдер платформы
    /// </summary>
    /// <param name="other">Коллайдер игрока</param>
    void OnTriggerEnter(Collider other)
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
            if (platform.transform.position.y < -100)
            {
                Invoke("SelfDestroy", 0f);
            }
        }
    }
    private void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
