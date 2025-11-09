using System.Collections;
using UnityEngine;

public class StartCinematic : MonoBehaviour
{
    [Header("References")]
    public Animator playerAnimator;              // 플레이어 Animator
    public CameraFollow cameraFollow;         // CameraFollow 스크립트
    public ClientInputManager playerInput;    // 플레이어 입력 스크립트

    [Header("Settings")]
    public Transform firstPersonViewPoint;    // 캐릭터 눈 위치
    public float cinematicDuration = 2.5f;      // GetUp 애니메이션 길이

    private void Start()
    {
        // 플레이어 입력 잠금
        if (playerInput != null)
            playerInput.enabled = false;

        // 1인칭 시점으로 전환
        if (cameraFollow != null)
            cameraFollow.SetFirstPerson(true);

        // GetUp 애니메이션 재생
        if (playerAnimator != null)
            playerAnimator.Play("Stand Up");

        // 시네마틱 끝나면 Idle + 3인칭 전환
        StartCoroutine(EndCinematic());
    }

    private IEnumerator EndCinematic()
    {
        // 시네마틱 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(cinematicDuration);

        // Idle 상태로 전환
        if (playerAnimator != null)
            playerAnimator.Play("Idle");

        // 카메라를 3인칭으로 전환
        if (cameraFollow != null)
            cameraFollow.SetFirstPerson(false);

        // 플레이어 입력 재개
        if (playerInput != null)
            playerInput.enabled = true;
    }
}
