﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingColumn : MonoBehaviour
{
    public Rigidbody platform;
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
        }
    }

    void Update()
    {
        if (activated)
        {
            if (!locker)
            {
                platform.isKinematic = false;
                Invoke("PushForce", 0.01f);
                locker = true;
            }
            if (gameObject.activeSelf && platform.transform.position.y < -100)
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
    private void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
}
