using UnityEngine;

/// <summary>
/// 인벤토리 UI를 관리하는 클래스.
/// 인벤토리 열기/닫기 및 슬롯 UI 갱신을 담당.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject inventoryPanel;

    [Header("Slots")]
    [SerializeField] private InventorySlot[] slots = new InventorySlot[16];

    public bool canProcessInput = true;


    /// <summary>
    /// 인벤토리 UI가 열려있는지 여부
    /// </summary>
    public bool IsOpen => isOpen;

    private bool isOpen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // 시작 시 모든 슬롯을 빈 상태로 초기화
        foreach (InventorySlot slot in slots)
        {
            slot.ClearSlot();
        }
    }

    private void Start()
    {
        Close();
    }

    private void Update()
    {
        if (canProcessInput)
        {
            HandleInventoryInput();
        }
    }

    #region Inventory Window

    /// <summary>
    /// 인벤토리 열기/닫기
    /// </summary>
    private void HandleInventoryInput()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Inventory]))
        {
            if (isOpen)
                Close();
            else
                Open();
        }
    }

    /// <summary>
    /// 인벤토리 열기
    /// </summary>
    public void Open()
    {
        isOpen = true;
        inventoryPanel.SetActive(true);
    }

    /// <summary>
    /// 인벤토리 닫기
    /// </summary>
    public void Close()
    {
        isOpen = false;
        inventoryPanel.SetActive(false);
    }

    #endregion

    #region Slot

    /// <summary>
    /// 지정한 슬롯의 아이템 UI를 갱신
    /// </summary>
    public void UpdateSlot(int index, Item item)
    {
        if (index < 0 || index >= slots.Length)
        {
            Logger.LogWarning("유효하지 않은 슬롯 인덱스입니다.", this);
            return;
        }

        if (item == null)
        {
            Logger.LogWarning("아이템 데이터가 없습니다.", this);
            return;
        }

        slots[index].SetItem(item.itemImage);

        Logger.Log($"슬롯 {index} : {item.itemName} 추가", this);
    }

    #endregion
}