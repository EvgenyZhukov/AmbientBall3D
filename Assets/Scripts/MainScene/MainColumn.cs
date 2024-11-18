using UnityEngine;

/// <summary>
/// Анимирует колонны в меню
/// </summary>
public class MainColumn : MonoBehaviour
{
    private float speed;
    public float fixedSpeed;
   
    void Start()
    {
        speed = Random.Range(-0.1f, 0.1f);
    }

    void Update()
    {
        if (fixedSpeed == 0)
        {
            transform.position += new Vector3(0.0f, Mathf.Sin(Time.time) * speed * Time.deltaTime, 0.0f);
        }
        else
        {
            transform.position -= new Vector3(0.0f, Mathf.Sin(Time.time) * fixedSpeed * Time.deltaTime, 0.0f);
        }
    }
}
