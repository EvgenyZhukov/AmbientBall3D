using UnityEngine;
using UnityEngine.Audio;

public class AudioScriptMain : MonoBehaviour
{
    public bool fadeIn;
    public bool fadeOut;
    private float fadeInStep = 25f;
    private float fadeOutStep = 35f;
    public AudioMixer masterMixer;
    private float volumeValue;

    [Header("Музыка")]
    public AudioSource music;

    [Header("Звуки UI")]
    public AudioSource clickSound;
    public AudioSource errorSound;

    [Header("Звуки игрока")]
    public AudioSource jumpSound;

    private void Start()
    {
        fadeIn = true;
        volumeValue = -40f;
    }

    void Update()
    {
        FadeIn();
        FadeOut();
    }

    private void FadeIn()
    {
        if (fadeIn)
        {
            volumeValue += fadeInStep * Time.deltaTime;
            masterMixer.SetFloat("masterVolume", volumeValue);
            if (volumeValue >= 0)
            {
                fadeIn = false;
                masterMixer.SetFloat("masterVolume", 0f);
            }
        }
    }
    private void FadeOut()
    {
        if (fadeOut)
        {
            volumeValue -= fadeOutStep * Time.deltaTime;
            masterMixer.SetFloat("masterVolume", volumeValue);
            if (volumeValue <= -40f)
            {
                fadeOut = false;
                masterMixer.SetFloat("masterVolume", -40f);
            }
        }
    }
}
