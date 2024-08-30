using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISplashScreen : MonoBehaviour
{
    public CanvasGroup logoCanvasGroup;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;
    public GameObject nextPanel;

    private void Start()
    {
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            logoCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        logoCanvasGroup.alpha = 1;

        yield return new WaitForSeconds(displayDuration);

        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            logoCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        logoCanvasGroup.alpha = 0;

        gameObject.SetActive(false);
        nextPanel.SetActive(true);
    }
}