using UnityEngine;

public class FallingColumn : MonoBehaviour
{
    public Rigidbody platform;
    [SerializeField] private bool activated = false;
    private bool locker = false;
    int minForce = 4;
    int maxForce = 6;
    public GameObject activator;

    public AudioSource columnSound1;
    public AudioSource columnSound2;
    public AudioSource columnSound3;
    bool soundLocker = false;

    bool activatedChecked = false;

    void Start()
    {
        ActivateCheck();
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
            activator.SetActive(false);
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
    private void SoundStarter()
    {
        if (!soundLocker)
        {
            int soundChoice = Random.Range(0, 3);
            switch (soundChoice)
            {
                case 0:
                    columnSound1.pitch = Random.Range(0.98f, 1f);
                    columnSound1.Play();
                    break;
                case 1:
                    columnSound2.pitch = Random.Range(0.98f, 1f);
                    columnSound2.Play();
                    break;
                case 2:
                    columnSound3.pitch = Random.Range(0.98f, 1f);
                    columnSound3.Play();
                    break;
            }
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
