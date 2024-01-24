using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class fadeInSCreen : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioMixer audioMixer;
    public Canvas canvas;
    public float fadeTime;

    private static bool scriptLoaded = false;

    void Awake()
    {
        if (!scriptLoaded)
        {
           
            DontDestroyOnLoad(gameObject);
            scriptLoaded = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayerPrefs.SetFloat("SliderVolumeLevel", 0.5f);
        float mix = 0.5f*40-30;
        audioMixer.SetFloat("volume", mix);
        canvas.gameObject.SetActive(false);
        StartCoroutine(FadeOutAudio(fadeTime));
    }

    IEnumerator FadeOutAudio(float duration)
    {
        if (audioSource != null) {
            float startVolume = 0f;
            float targetVol = 0.5f; 

            CanvasGroup canvasCanvasGroup = canvas.GetComponent<CanvasGroup>();
            if (canvasCanvasGroup == null)
            {
                canvasCanvasGroup = canvas.gameObject.AddComponent<CanvasGroup>();
            }
            
            canvasCanvasGroup.alpha = 0f;
            canvas.gameObject.SetActive(true);


            float timer = 0f;
            while (timer < fadeTime)
            {
                float alpha = Mathf.Lerp(0f, 1f, timer / fadeTime);
                audioSource.volume = Mathf.Lerp(startVolume, targetVol, timer / duration);

                canvasCanvasGroup.alpha = alpha;

                timer += Time.deltaTime;
                yield return null;
            }

            canvasCanvasGroup.alpha = 1f;
            audioSource.volume = targetVol;
        }
    }
}

