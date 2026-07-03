using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFadeTwo : MonoBehaviour
{
    public Image fadeImage;             // 블랙 오버레이 Image
    private float fadeDuration = 3f;     // 페이드 지속 시간

    private bool isFading = false;

    private void Start()
    {
        StartFade();
    }
    public void StartFade()
    {
        if (!isFading)
            StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.5f);

        isFading = true;
        float timer = 0f;
        Color color = fadeImage.color;

        color.a = 1;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;
    }
}
