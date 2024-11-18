using System;
using System.Collections;
using UnityEngine;

public class CollapsingColumn : MonoBehaviour
{
    public GameObject[] columns;
    public Rigidbody[] platforms;
    public AudioSource[] columnSounds;
    public Vector3[] forcePoints;
    private bool[] soundLockers = new bool[4];

    [SerializeField] private bool activated = false;
    private bool locker = false;

    int minForce = 4;
    int maxForce = 6;

    public GameObject activator;

    bool activatedChecked = false;

    void Start()
    {
        ActivateCheck();
    }

    /// <summary>
    /// Выполняется при выходе игрока из коллайдера платформы
    /// </summary>
    /// <param name="other">Коллайдер игрока</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activated = true;
            activator.SetActive(false);
        }
    }

    void Update()
    {
        if (activated)
        {
            //Запуск падения платформ
            if (!locker)
            {
                Debug.Log("Активировалось крушение колонн!");
                StartCoroutine(ColumnsCollapsing());
                locker = true;
            }

            //Выключение объекта
            if (gameObject.activeSelf && platforms[3].transform.position.y < -100)
            {
                gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Проверяет, активировано ли падение платформ
    /// </summary>
    private void ActivateCheck()
    {
        if (!activatedChecked && !activator.activeSelf)
        {
            activated = true;

            platforms[0].isKinematic = false;
            platforms[1].isKinematic = false;
            platforms[2].isKinematic = false;
            platforms[3].isKinematic = false;

            activatedChecked = true;
        }
    }
    private IEnumerator ColumnsCollapsing()
    {
        yield return new WaitForSeconds(0.5f);
        if (!soundLockers[0])
        {
            platforms[0].isKinematic = false;
            columnSounds[0].pitch = PitchRandomizer();
            columnSounds[0].Play();
            soundLockers[0] = true;
        }
        forcePoints[0] = columns[0].transform.position;
        platforms[0].AddForceAtPosition(transform.forward * 1000 * ForceRandomizer(), forcePoints[0]);

        yield return new WaitForSeconds(1f);
        if (!soundLockers[1])
        {
            platforms[1].isKinematic = false;
            columnSounds[1].pitch = PitchRandomizer();
            columnSounds[1].Play();
            soundLockers[1] = true;
        }
        forcePoints[1] = columns[1].transform.position;
        platforms[1].AddForceAtPosition(transform.right * 1000 * ForceRandomizer(), forcePoints[1]);

        yield return new WaitForSeconds(1f);
        if (!soundLockers[2])
        {
            platforms[2].isKinematic = false;
            columnSounds[2].pitch = PitchRandomizer();
            columnSounds[2].Play();
            soundLockers[2] = true;
        }
        forcePoints[2] = columns[2].transform.position;
        platforms[2].AddForceAtPosition(transform.forward * 1000 * ForceRandomizer(), forcePoints[2]);
        
        yield return new WaitForSeconds(0.5f);
        if (!soundLockers[3])
        {
            platforms[3].isKinematic = false;
            columnSounds[3].pitch = PitchRandomizer();
            columnSounds[3].Play();
            soundLockers[3] = true;
        }
        forcePoints[3] = columns[3].transform.position;
        platforms[3].AddForceAtPosition(-transform.right * 500 * ForceRandomizer(), forcePoints[3]);
    }
    float PitchRandomizer()
    {
        float pitch = UnityEngine.Random.Range(0.98f, 1f);
        return pitch;
    }
    float ForceRandomizer()
    {
        float force = UnityEngine.Random.Range(minForce, maxForce);
        return force;
    }
}