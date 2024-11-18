using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour
{
    // MasterMixer.SetFloat("masterVolume", volumeValue);

    public bool fadeIn;
    public bool fadeOut;
    private float fadeInStep = 35f;
    private float fadeOutStep = 35f;
    public AudioMixer masterMixer;
    private float volumeValue = -40f;

    [Header("Музыка")]
    public AudioSource music;

    [Header("Звуки UI")]
    public AudioSource clickSound;
    public AudioSource errorSound;

    [Header("Звуки игрока")]
    public AudioSource jumpSound;
    public AudioSource transformSound;
    public AudioSource hitSound;
    public AudioSource defeatSound;
    public AudioSource rollingSound;
    public AudioSource teleportSound;

    private void Start()
    {
            FadeIn();
    }
    private void FadeIn()
    {
        StartCoroutine(FadeVolume(0f, fadeInStep, true));
    }
    public void FadeOut()
    {
        StartCoroutine(FadeVolume(-40f, fadeOutStep, false));
    }
    private IEnumerator FadeVolume(float targetVolume, float step, bool fadeIn)
    {
        while ((fadeIn && volumeValue < targetVolume) || (!fadeIn && volumeValue > targetVolume))
        {
            volumeValue += (fadeIn ? 1 : -1) * step * Time.deltaTime;
            masterMixer.SetFloat("masterVolume", volumeValue);
            yield return null;
        }

        volumeValue = targetVolume;
        masterMixer.SetFloat("masterVolume", volumeValue);
    }
    public void PlayJumpSound()
    {
        jumpSound.pitch = Random.Range(0.95f, 1f);
        jumpSound.Play();
    }
}
