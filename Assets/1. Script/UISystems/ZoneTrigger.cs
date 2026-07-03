using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZoneTrigger : MonoBehaviour
{
    [Header("References")]
    public InGameUIManager uiManager;
    public DialogManager dialogManager;
    public ClientInputManager playerInput;
    public CameraFollow cameraFollow;

    public Image fadeImage;
    private float fadeDuration = 3f;     // 페이드 지속 시간
    private bool isFading = false;
    ScreenFade screenfade;


    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Logger.Log("확인: 포털 존에 입장");
        

        // 캐릭터 태그 확인
        if (other.CompareTag("Player"))
        {
            Logger.Log("무지");
            StartFade();

            if (cameraFollow != null) cameraFollow.SetFirstPerson(true);
            if (playerInput != null) playerInput.SetInputActive(false);
            if (uiManager != null) uiManager.HideKeyGuideToggle();
            if (uiManager != null) uiManager?.ToggleKeyGuide();

            StartCoroutine(HandleZoneSequence());
        }
    }

    private IEnumerator HandleZoneSequence()
    {

        yield return new WaitForSeconds(1f);

        // JSON duration 기반 대사 재생
        TextAsset json = Resources.Load<TextAsset>("Dialogs/WarningDialog");
        if (json != null)
        {
            DialogData data = JsonUtility.FromJson<DialogData>(json.text);
            yield return dialogManager.PlayDialog(data);    
        }

        if (cameraFollow != null) cameraFollow.SetFirstPerson(false);
        if (playerInput != null) playerInput.SetInputActive(true);
        if (uiManager != null) uiManager.ShowKeyGuideToggle();
        if (uiManager != null) uiManager.ClearDialog();
        if (uiManager != null) uiManager?.ToggleKeyGuide();
    }

    public void StartFade()
    {
        if (!isFading)
            StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {

        isFading = true;
        float timer = 0f;
        Color color = fadeImage.color;
        color.a = 1;

        yield return new WaitForSeconds(1f);

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, timer / fadeDuration);
            fadeImage.color = color;
        Logger.Log($"변환color: a {color.a}");
            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;
    }
}
