using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waitToLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public Button backToStart;
    public Button tryAgain;

    public RawImage optionsScreen;

    void Start()
    {
        backToStart.gameObject.SetActive(false);
        tryAgain.gameObject.SetActive(false);
        optionsScreen.gameObject.SetActive(false);
        StartCoroutine(FadeButtonsIn(2f));
    }

    IEnumerator FadeButtonsIn(float fadeTime)
    {
        yield return new WaitForSeconds(7);

        CanvasGroup backToStartCanvasGroup = backToStart.GetComponent<CanvasGroup>();
        if (backToStartCanvasGroup == null)
        {
            backToStartCanvasGroup = backToStart.gameObject.AddComponent<CanvasGroup>();
        }

        CanvasGroup tryAgainCanvasGroup = tryAgain.GetComponent<CanvasGroup>();
        if (tryAgainCanvasGroup == null)
        {
            tryAgainCanvasGroup = tryAgain.gameObject.AddComponent<CanvasGroup>();
        }

        CanvasGroup canvasCanvasGroup = optionsScreen.GetComponent<CanvasGroup>();
        if (canvasCanvasGroup == null)
        {
            canvasCanvasGroup = optionsScreen.gameObject.AddComponent<CanvasGroup>();
        }

        backToStartCanvasGroup.alpha = 0f;
        tryAgainCanvasGroup.alpha = 0f;

        backToStart.gameObject.SetActive(true);
        tryAgain.gameObject.SetActive(true);
        optionsScreen.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < fadeTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeTime);

            backToStartCanvasGroup.alpha = alpha;
            tryAgainCanvasGroup.alpha = alpha;
            canvasCanvasGroup.alpha = alpha;

            timer += Time.deltaTime;
            yield return null;
        }

        backToStartCanvasGroup.alpha = 1f;
        tryAgainCanvasGroup.alpha = 1f;
    }

    IEnumerator TestCor() {
        Debug.Log("Coroutine started");

        yield return new WaitForSeconds(7);

        Debug.Log("Buttons should appear now");

        backToStart.gameObject.SetActive(true);
        tryAgain.gameObject.SetActive(true);
    }
}
