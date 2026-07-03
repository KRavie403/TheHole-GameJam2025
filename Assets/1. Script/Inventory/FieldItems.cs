using System.Collections;
using UnityEngine;


public class FieldItems : Interactable
{
    [Header("References")]
    [SerializeField] private Item item;
    [SerializeField] private DialogManager dialogManager;


    #region Cinematic

    /// <summary>
    /// 시네마틱 연출을 순차적으로 실행
    /// </summary>
    private IEnumerator CinematicSequence()
    {

        // NPC 대화 시작
        AudioManager.Inst.PlayCharacterEffect(7);

        yield return PlayIntroDialog();

        Logger.Log("대화 종료");
        uiManager?.ShowKeyGuideToggle();
        uiManager?.ClearDialog();
        uiManager?.ToggleKeyGuide();

        Destroy(gameObject);
    }

    /// <summary>
    /// 인트로 대사 재생
    /// </summary>
    private IEnumerator PlayIntroDialog()
    {
        TextAsset json = Resources.Load<TextAsset>("Dialogs/itemDialog");

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

    /// <summary>
    /// 필드 아이템 데이터 설정
    /// </summary>
    protected override void Interact()
    {
        Inventory.Instance.AddItem(item);

        uiManager.HideInteractButton();

        // 사라질 오브젝트 시간 설정 = 3초
        WorldManager.Instance.VanishInterval = 2f;

        StartCoroutine(CinematicSequence());
    }


}