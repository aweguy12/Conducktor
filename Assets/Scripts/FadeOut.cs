using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float delay = 0f;

    private Renderer objectrenderer;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        objectrenderer = GetComponent<Renderer>();
        Debug.Log("Renderer: " + objectrenderer);

        if (objectrenderer == null)
        {
            Debug.LogError("Renderer missing");
            enabled = false;
            return;
        }
        color = objectrenderer.material.color;
        Debug.Log("Initial Color: " + color);
        StartFadeOut();
    }

    public void StartFadeOut()
    {
        Debug.Log("StartFadeOut called");
        StartCoroutine(FadeOutCoroutine());
    }
    IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;
        Color currentColor = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            currentColor.a = alpha;
            objectrenderer.material.color = currentColor;

            yield return null;
        }

        currentColor.a = 0f;
        objectrenderer.material.color = currentColor;
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);

    }

}
