using System.Collections;
using UnityEngine;

public class ObjectDisablerScript : MonoBehaviour
{
    public ParticleSystem particleStarsStream;
    bool activated = false;

    IEnumerator StartParticleSystem()
    {
        var stars = particleStarsStream.main;
        particleStarsStream.Play();
        stars.loop = true;

        yield return null; // Подождать один кадр

        activated = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            StartCoroutine(StartParticleSystem());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && activated)
        {
            var stars = particleStarsStream.main;
            stars.loop = false;

            activated = false;
        }
    }
}