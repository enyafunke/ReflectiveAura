using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundSourceAngry;
    [SerializeField] AudioSource backgroundSourceHappy;
    [SerializeField] AudioSource backgroundSourceSad;
    [SerializeField] AudioSource effectSourceAngry;
    [SerializeField] AudioSource effectSourceHappy;
    [SerializeField] AudioSource effectSourceSad;
    [SerializeField] AudioClip happy;
    [SerializeField] AudioClip sad;
    [SerializeField] AudioClip angry;
    [SerializeField] AudioClip rain;
    [SerializeField] AudioClip birds;
    [SerializeField] AudioClip fire;

    public void AngrySound()
    {
        backgroundSourceAngry.clip = angry;
        backgroundSourceAngry.Play();
        StartCoroutine(StartFade(backgroundSourceAngry, 5, 1));
        effectSourceAngry.clip = fire;
        effectSourceAngry.Play();
        StartCoroutine(StartFade(effectSourceAngry, 5, 1));
    }

    public void HappySound()
    {
        backgroundSourceHappy.clip = happy;
        backgroundSourceHappy.Play();
        StartCoroutine(StartFade(backgroundSourceHappy, 5, 1));
        effectSourceHappy.clip = birds;
        effectSourceHappy.Play();
        StartCoroutine(StartFade(effectSourceHappy, 5, 1));
    }

    public void SadSound()
    {
        backgroundSourceSad.clip = sad;
        backgroundSourceSad.Play();
        StartCoroutine(StartFade(backgroundSourceSad, 5, 1));
        effectSourceSad.clip = rain;
        effectSourceSad.Play();
        StartCoroutine(StartFade(effectSourceSad, 5, 1));
    }

    public void NeutralSound()
    {
        StartCoroutine(StartFade(backgroundSourceAngry, 5, 0));
        StartCoroutine(StartFade(backgroundSourceHappy, 5, 0));
        StartCoroutine(StartFade(backgroundSourceSad, 5, 0));
        StartCoroutine(StartFade(effectSourceAngry, 5, 0));
        StartCoroutine(StartFade(effectSourceHappy, 5, 0));
        StartCoroutine(StartFade(effectSourceSad, 5, 0));
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
