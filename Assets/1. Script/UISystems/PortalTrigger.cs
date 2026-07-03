using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PortalTrigger : MonoBehaviour
{
    [Header("References")]
    public GameObject LastRequest;                 // 포털 UI 루트 오브젝트
    public GameObject uiManager;                   // TypingEffect가 붙은 오브젝트
    [SerializeField] private GameObject ZoneCollider;
    [SerializeField] private ClientInputManager playerInput;

    [Header("Fade (UI 전체 페이드)")]
    public CanvasGroup fadeGroup;

    private float fadeDuration = 2f;

    private bool isFading = false;     // 페이드 중복 실행 방지
    private bool isTriggered = false;  // 포털 중복 트리거 방지

    private void Start()
    {
        // 시작 시 UI 비활성화
        if (LastRequest != null)
            LastRequest.SetActive(false);

        // 초기 페이드 상태
        if (fadeGroup != null)
            fadeGroup.alpha = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 이미 실행됐거나 플레이어가 아니면 무시
        if (isTriggered) return;
        if (!other.CompareTag("Player")) return;

        isTriggered = true;

        Logger.Log("포털 충돌 발생");

        // UI 활성화
        if (LastRequest != null)
            LastRequest.SetActive(true);

        // 플레이어 입력 비활성화
        if (playerInput != null)
            playerInput.SetInputActive(false);

        // 이동 제한 콜라이더 제거
        if (ZoneCollider != null)
            ZoneCollider.SetActive(false);

        // 게임 오디오 일시 정지
        AudioListener.pause = true;

        // CanvasGroup 재참조 (SetActive 이후 안전 처리)
        if (fadeGroup == null && LastRequest != null)
            fadeGroup = LastRequest.GetComponent<CanvasGroup>();

        // 초기 페이드 값 설정
        if (fadeGroup != null)
            fadeGroup.alpha = 0f;

        // 페이드 시작
        if (!isFading)
            StartCoroutine(FadeOut());

        // 대화 + 씬 전환 시퀀스 시작
        StartCoroutine(HandlePortalSequence());
    }

    private IEnumerator HandlePortalSequence()
    {
        Logger.Log("대화 시퀀스 시작");

        // 연출 딜레이
        yield return new WaitForSeconds(3f);

        Logger.Log("대화 시작");

        // 타이핑 효과 실행 및 완료까지 대기
        if (uiManager != null)
        {
            TypingEffect t = uiManager.GetComponent<TypingEffect>();

            if (t != null)
            {
                t.enabled = true;
                yield return new WaitUntil(() => t.IsFinished);
            }
        }

        Logger.Log("대화 종료 → 씬 전환 예약");

        // 15초 후 타이틀 씬 이동
        GameSceneManager.Inst.LoadSceneDelayed("0. Title", 15f);
    }

    private IEnumerator FadeOut()
    {
        isFading = true;

        float timer = 0f;

        // 0 → 1 페이드
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Clamp01(timer / fadeDuration);

            if (fadeGroup != null)
                fadeGroup.alpha = alpha;

            yield return null;
        }

        if (fadeGroup != null)
            fadeGroup.alpha = 1f;

        isFading = false;
    }
}