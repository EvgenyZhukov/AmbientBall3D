using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public bool fadeIn;
    public bool fadeOut;
    public AudioSource music;
    public AudioSource errorSound;
    private float fadeInStep = 0.005f;
    private float fadeOutStep = 0.02f;

    void FixedUpdate()
    {
        FadeIn();
        FadeOut();
    }

    private void FadeIn()
    {
        if (fadeIn)
        {
            music.volume += fadeInStep;
            if (music.volume >= 1)
            {
                fadeIn = false;
            }
        }
    }
    private void FadeOut()
    {
        if (fadeOut)
        {
            music.volume -= fadeOutStep;
            if (music.volume <= 0)
            {
                fadeOut = false;
            }
        }
    }



}
