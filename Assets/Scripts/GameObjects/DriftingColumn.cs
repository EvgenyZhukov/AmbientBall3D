using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Анимирует колонны в меню
/// </summary>
public class DriftingColumn : MonoBehaviour
{
    public bool x;
    public float speedX;
    public bool y;
    public float speedY;
    public bool z;
    public float speedZ;

    void FixedUpdate()
    {
        if (x)
        {
            transform.position += new Vector3(Mathf.Sin(Time.time) * speedX * Time.deltaTime, 0.0f, 0.0f);
        }
        if (y)
        {
            transform.position += new Vector3(0.0f, Mathf.Sin(Time.time) * speedY * Time.deltaTime, 0.0f);
        }
        if (z)
        {
            transform.position += new Vector3(0.0f, 0.0f, Mathf.Sin(Time.time) * speedZ * Time.deltaTime);
        }
    }
}
