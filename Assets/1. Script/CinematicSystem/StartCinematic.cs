using System.Collections;
using UnityEngine;

/// <summary>
/// 게임 시작 연출(시네마틱)을 관리하는 클래스.
/// 플레이어 입력, 카메라, 애니메이션 및 대사를 순차적으로 제어.
/// </summary>
public class StartCinematic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator npcAnimator;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private ClientInputManager playerInput;
    [SerializeField] private InGameUIManager uiManager;
    [SerializeField] private DialogManager dialogManager;

    private void Awake()
    {
       if(playerInput == null)
            playerInput = GetComponent<ClientInputManager>();
    }

    private void Start()
    {
        // 플레이어 입력 비활성화
        if (playerInput != null)
        {
            playerInput.SetInputActive(false);
            InventoryUI.Instance.canProcessInput = false;
        }

        AudioManager.Inst.PlayBasicEffect(0);

        Logger.Log("대화 종료");
        uiManager?.ClearDialog();
        uiManager?.HideKeyGuideToggle();
        uiManager?.ToggleKeyGuide();

        cameraFollow?.SetFirstPerson(true);

        playerAnimator?.SetTrigger("StandUp");

        StartCoroutine(CinematicSequence());
    }

    #region Cinematic

    /// <summary>
    /// 시네마틱 연출을 순차적으로 실행
    /// </summary>
    private IEnumerator CinematicSequence()
    {
        AnimatorStateInfo playerState = playerAnimator.GetCurrentAnimatorStateInfo(0);

        // Stand Up 상태 진입 대기
        while (!playerState.IsName("Stand Up"))
        {
            yield return null;
            playerState = playerAnimator.GetCurrentAnimatorStateInfo(0);
        }

        // Stand Up 애니메이션 종료 대기
        while (playerState.normalizedTime < 1f)
        {
            yield return null;
            playerState = playerAnimator.GetCurrentAnimatorStateInfo(0);
        }

        // NPC 대화 시작
        npcAnimator.SetBool("IsTalking", true);
        AudioManager.Inst.PlayCharacterEffect(7);

        yield return PlayIntroDialog();

        // NPC 대화 종료
        npcAnimator.SetBool("IsTalking", false);

        // 플레이 상태 복귀
        cameraFollow?.SetFirstPerson(false);

        if (playerInput != null)
        {
            playerInput.SetInputActive(true);
            InventoryUI.Instance.canProcessInput = true;
        }

        uiManager?.ShowKeyGuideToggle();
        uiManager?.ClearDialog();
        uiManager?.ToggleKeyGuide();

        playerAnimator?.SetTrigger("Idle");
    }

    /// <summary>
    /// 인트로 대사 재생
    /// </summary>
    private IEnumerator PlayIntroDialog()
    {
        TextAsset json = Resources.Load<TextAsset>("Dialogs/IntroDialog");

        if (json == null)
        {
            Logger.LogWarning("IntroDialog.json을 찾을 수 없습니다.", this);
            yield break;
        }

        DialogData dialogData = JsonUtility.FromJson<DialogData>(json.text);

        if (dialogData != null)
            yield return dialogManager.PlayDialog(dialogData);
    }

    #endregion
}