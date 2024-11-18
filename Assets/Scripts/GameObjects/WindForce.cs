using UnityEngine;

public class WindForce : MonoBehaviour
{
    public float force = -200;
    public bool invert;

    public AudioSource windSound;
    public bool volumeUp = false;
    public float soundChangeStep = 0.8f;

    /// <summary>
    /// Сдвигающая сила, срабатывает при соприкосновении коллайдеров
    /// </summary>
    /// <param name="other">Коллайдер объекта попавшего в лифт</param>
    void OnTriggerStay(Collider other)
    {
        Rigidbody player;
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Rigidbody>();
            if (invert)
            {
                player.AddForce(Vector3.forward * force * Time.deltaTime);
            }
            else
            {
                player.AddForce(Vector3.right * force * Time.deltaTime);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            windSound.Play();
            volumeUp = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            volumeUp = false;
        }
    }
    private void Update()
    {
            if (volumeUp)
            {
                if (windSound.volume < 1)
                {
                    windSound.volume += soundChangeStep * Time.deltaTime;
                }
            }
            if (!volumeUp)
            {
                if (windSound.volume > 0)
                {
                    windSound.volume -= soundChangeStep * Time.deltaTime;
                    if (windSound.volume <= 0)
                    {
                        windSound.Stop();
                    }
                }
            }
    }
}