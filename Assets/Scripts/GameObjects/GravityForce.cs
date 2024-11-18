using UnityEngine;

public class GravityForce : MonoBehaviour
{
    public float force = 100;
    public PlayerController playerController;
    public AudioSource elevatorSound;
    public GameObject gravityElevator;
    private bool locker = false;
    bool soundOn = false;
    float soundChangeStep = 0.5f;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (!locker)
        {
            if (gravityElevator.activeSelf)
            {
                elevatorSound.Play();
                locker = true;
            }
            else
            {
                elevatorSound.Stop();
                locker = true;
            }
        }

        if (soundOn)
        {
            if (elevatorSound.volume < 1)
            {
                elevatorSound.volume += soundChangeStep * Time.deltaTime;
            }
        }
        if (!soundOn)
        {
            if (elevatorSound.volume > 0)
            {
                elevatorSound.volume -= soundChangeStep * Time.deltaTime;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundOn = true;
        }
    }
    /// <summary>
    /// Подъем, срабатывает при соприкосновении коллайдеров
    /// </summary>
    /// <param name="other">Коллайдер объекта попавшего в лифт</param>
    void OnTriggerStay(Collider other)
    {
        Rigidbody player;
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Rigidbody>();
            player.AddForce(Vector3.up * force * Time.deltaTime);  // Прикладывает подъемную силу
            playerController.moveForce = playerController.moveForceInElevator;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerController.playerForm == 3)
            {
                playerController.moveForce = playerController.moveForceInJumpBigWeight;
            }
            else
            {
                playerController.moveForce = playerController.moveForceInJump;
            }
            soundOn = false;
        }
    }
}