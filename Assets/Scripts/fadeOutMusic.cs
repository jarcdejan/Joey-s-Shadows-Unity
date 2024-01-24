using System.Collections;
using UnityEngine;

public class fadeOutMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeOutTime;

    void Start()
    {
        StartCoroutine(FadeOutAudio(fadeOutTime));
    }

    IEnumerator FadeOutAudio(float duration)
    {
        float startVolume = audioSource.volume;

        // Gradually decrease volume to 0 over the specified duration
        float timer = 0f;
        while (timer < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / duration);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure volume is set to 0 at the end
        audioSource.volume = 0f;

        // Stop the audio playback (optional)
        audioSource.Stop();
    }
}

