using UnityEngine;

/// <summary>
/// Телепортирует игрока
/// </summary>
public class TeleportPlayer : MonoBehaviour
{
    public PlayerController playerController;
    public AudioScript audioScript;
    public GameScript gameScript;
    public bool teleported = false;     // Проверяет, было ли недавно телепортирование, что бы не попасть в замкнутый цикл
    public TeleportPlayer target;       // Соединяет телепорты
    public ParticleSystem explode;
    public GameObject activator;
    public ButtonColumn button;
    [SerializeField] private bool activated = false;
    public Collider playerCol;


    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        audioScript = FindObjectOfType<AudioScript>();
        gameScript = FindObjectOfType<GameScript>();
    }
    void Update()
    {
        if (button.activated)
        {
            activator.SetActive(true);
        }
        if (activator.activeSelf)
        {
            activated = true;
        }
    }
    /// <summary>
    /// Вход, срабатывает при соприкосновении коллайдеров
    /// </summary>
    /// <param name="other">Коллайдер объекта попавшего в телепорт</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && activated)
        {
            playerCol = other;
            //transform.position = Vector3.MoveTowards(transform.position, playerCol.gameObject.transform.position, stepMovePlayerTowardTeleport * Time.deltaTime);
            if (!teleported)    // Если телепортирования только что не было
            {
                RestartParticleSystem(explode);
                target.gameObject.SetActive(true);
                RestartParticleSystem(target.explode);
                audioScript.teleportSound.Play();
                Invoke("Hide", 0.25f);
                Invoke("Relocate", 0.6f);
            }
        }
    }
    /// <summary>
    /// Включает телепорты при выходе из коллайдера
    /// </summary>
    /// <param name="other">Коллайдер объекта попавшего в телепорт</param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            teleported = false;
        }
    }
    void Hide()
    {
        Rigidbody playerRb = playerCol.GetComponent<Rigidbody>();
        playerRb.velocity = new Vector3(0, 0, 0);
        gameScript.player.gameObject.SetActive(false); // Выключает модель игрока
    }

    void Relocate()
    {
        gameScript.player.gameObject.SetActive(true); // Включает модель игрока
        target.teleported = true;                                                   // Выключает телепорт места назначения
        playerCol.gameObject.transform.position = target.gameObject.transform.position; // Присваеваем телепортируемому объекту позицию телепорта назначения
        explode.Clear();
    }

    void RestartParticleSystem(ParticleSystem particleSystem)
    {
        particleSystem.Clear();
        particleSystem.Play();
    }
}
