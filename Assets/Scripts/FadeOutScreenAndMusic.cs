using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutScreenAndMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public Canvas canvasToFade;
    public float fadeOutTime;

    public void fadeOnButtonPress() 
    {
        StartCoroutine(FadeOut(fadeOutTime));
    }

    IEnumerator FadeOut(float duration)
    {
        if (audioSource != null && canvasToFade != null)
        {
            float startVolume = audioSource.volume;
            CanvasGroup canvasGroup = canvasToFade.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = canvasToFade.gameObject.AddComponent<CanvasGroup>();
            }

            float timer = 0f;
            while (timer < duration)
            {
                float normalizedTime = timer / duration;

                audioSource.volume = Mathf.Lerp(startVolume, 0f, normalizedTime);
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, normalizedTime);

                timer += Time.deltaTime;
                yield return null;
            }
            audioSource.volume = 0f;
            canvasGroup.alpha = 0.01f;

            audioSource.Stop();
            SceneManager.LoadScene("videoScene", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("startScreen");
        }
    }
}
