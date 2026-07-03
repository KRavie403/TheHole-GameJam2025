using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected InGameUIManager uiManager;

    /// <summary>
    /// 플레이어가 상호작용 범위 안에 있는지 여부
    /// </summary>
    protected bool playerInRange;

    protected virtual void Start()
    {
        // Inspector에서 연결하지 않은 경우 자동 탐색
        if (uiManager == null)
            uiManager = FindFirstObjectByType<InGameUIManager>();
    }

    protected virtual void Update()
    {
        if (!playerInRange)
            return;

        // 아이템 월드 좌표를 화면 좌표로 변환하여 상호작용 UI 위치 갱신
        uiManager.UpdateInteractButton(transform.position + Vector3.up * 1.5f);

        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Interact]))
        {
            Interact();
        }
    }

    protected abstract void Interact();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInRange = true;
        uiManager.ShowInteractButton(transform.position + Vector3.up * 1.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInRange = false;
        uiManager.HideInteractButton();
    }
}