using UnityEngine;

public class FastFallingColumn : MonoBehaviour
{
    public Rigidbody platform;
    [SerializeField] private bool activated = false;
    public GameObject activator;
    private bool locker = false;

    public AudioSource columnSound;
    bool soundLocker = false;

    bool activatedChecked = false;

    void Start()
    {
        ActivateCheck();
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
            activator.SetActive(false);
            platform.isKinematic = false;
            SoundStarter();
        }
    }

    void Update()
    {
        if (activated)
        {
            if (!locker)
            {
                platform.isKinematic = false;
                locker = true;
            }
            if (gameObject.activeSelf && platform.transform.position.y < -100)
            {
                Invoke("SelfDestroy", 0f);
            }
        }
    }
    private void SelfDestroy()
    {
        gameObject.SetActive(false);
    }
    private void SoundStarter()
    {
        if (!soundLocker)
        {
            columnSound.pitch = Random.Range(0.9f, 1f);
            columnSound.Play();
            soundLocker = true;
        }
    }

    private void ActivateCheck()
    {
        if (!activatedChecked)
        {
            if (!activator.activeSelf)
            {
                activated = true;
                platform.isKinematic = false;
            }
        }
        activatedChecked = true;
    }
}
