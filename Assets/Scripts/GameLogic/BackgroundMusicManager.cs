using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip[] tracks; // ������ ��� �������� ���� ������
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = false;
        audioSource.playOnAwake = false;

        PlayRandomTrack();
    }

    void Update()
    {
        // �������� ���������� ���������������
        if (!audioSource.isPlaying)
        {
            PlayRandomTrack();
        }
    }

    void PlayRandomTrack()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 9)
        {
            if (tracks.Length > 1)
            {
                int randomTrackIndex = Random.Range(0, tracks.Length);

                // ��������, ����� �� �������������� ��� �� ����
                if (audioSource.clip != tracks[randomTrackIndex])
                {
                    audioSource.clip = tracks[randomTrackIndex];
                    audioSource.Play();
                }
                else
                {
                    // ���� ���� ���������, �������� ������
                    PlayRandomTrack();
                }
            }
            else
            {
                audioSource.clip = tracks[0];
                audioSource.Play();
            }
        }
        else
        {
            audioSource.clip = tracks[currentSceneIndex];
            audioSource.Play();
            audioSource.loop = true;
        }
    }
}
