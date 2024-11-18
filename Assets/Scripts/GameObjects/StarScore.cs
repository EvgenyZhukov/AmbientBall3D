using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefsSavingMethods;

public class StarScore : MonoBehaviour
{
    public GameObject starPoint;
    public GameObject starEffect;
    //public AudioSource StarSound;
    public GameScript gameScript;
    public SaveGameScript saveGameScript;
    public ParticleSystem explode1;
    public ParticleSystem explode2;
    public bool locker = false;
    public AudioSource starSound;

    void Start()
    {
        gameScript = FindObjectOfType<GameScript>();
        saveGameScript = FindObjectOfType<SaveGameScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !locker)
        {
            starSound.Play();
            MoveStar();
            Invoke("Off", 3.1f);
            gameScript.starsScore++;
            if (SceneManager.GetActiveScene().buildIndex == 9)
            {
                EndlessLevelStarCollect();
            }
            locker = true;
        }
    }
    void MoveStar()
    {
        var stars = explode1.main;
        var fo = explode2.forceOverLifetime;
        var fo2 = explode2.noise;
        stars.loop = false;
        fo.enabled = true;
        fo2.enabled = true;
    }
    void Off()
    {
        starPoint.SetActive(false);
    }
    /// <summary>
    /// Метод выполняемый при сборе звезды в бесконечном режиме (замена чекпоинта)
    /// </summary>
    void EndlessLevelStarCollect()
    {
        EndlessLevelManager endlessLevelManager = FindObjectOfType<EndlessLevelManager>();
        endlessLevelManager.scoreUp = true;
        saveGameScript.saving = true;
        SaveLoadData.SetInProgress(true);
        SaveLoadData.SaveCoordinates(transform.position.x, transform.position.y, transform.position.z);
    }
}
