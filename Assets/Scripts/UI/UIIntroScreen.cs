using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIntroScreen : MonoBehaviour
{
    public RectTransform carRectTransform;
    public float scaleDuration = 0.5f;
    public float displayDuration = 2f;
    public GameObject nextPanel;

    private void Start()
    {
        StartCoroutine(ScaleAndFadeOut());
    }

    private IEnumerator ScaleAndFadeOut()
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        while (elapsedTime < scaleDuration)
        {
            carRectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        carRectTransform.localScale = endScale;

        yield return new WaitForSeconds(displayDuration);

        elapsedTime = 0f;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (elapsedTime < scaleDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;

        gameObject.SetActive(false);
        nextPanel.SetActive(true);
    }
}
