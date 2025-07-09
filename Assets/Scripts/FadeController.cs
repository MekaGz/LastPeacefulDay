using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1f;
    public GameObject deathUI;

    public void StartDeathSequence(GameObject deathUI)
    {
        deathUI.SetActive(true);
        StartCoroutine(FadeAndShow(deathUI));
    }

    IEnumerator FadeAndShow(GameObject deathUI)
    {
        // Fade to black
        Color color = fadeImage.color;
        while (color.a < 1)
        {
            color.a += Time.deltaTime * fadeSpeed;
            fadeImage.color = color;
            yield return null;
        }

        // Mostrar UI luego de fade
        yield return new WaitForSeconds(1f);
        deathUI.SetActive(true);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeToClear());
    }

    IEnumerator FadeToClear()
    {
        deathUI.SetActive(true);
        Color color = fadeImage.color;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = color;
            yield return null;
        }

    }
}
