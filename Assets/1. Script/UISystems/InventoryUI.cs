using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 인벤토리 창을 관리하는 UI 컨트롤러.
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;     // 인벤토리 전체 패널
    public Transform slotParent;                // 아이템 슬롯들이 들어갈 부모 오브젝트
    public GameObject slotPrefab;            // 아이템 슬롯 프리팹

    private bool isOpen = false;               // 인벤토리 열림 여부
    private PlayerController player;          // 플레이어 참조

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        inventoryPanel.SetActive(false);    // 시작 시 닫기
    }

    private void Update()
    {
        // Tab 키로 인벤토리 열기/닫기
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpen) Close();
            else Open();
        }
    }

    /// <summary>
    /// 인벤토리 열기 (UI 업데이트 포함)
    /// </summary>
    public void Open()
    {
        isOpen = true;
        inventoryPanel.SetActive(true);
        Refresh();
    }

    /// <summary>
    /// 인벤토리 닫기
    /// </summary>
    public void Close()
    {
        isOpen = false;
        inventoryPanel.SetActive(false);
    }

    /// <summary>
    /// 현재 인벤토리 리스트로 UI 갱신
    /// </summary>
    private void Refresh()
    {
        // 기존 슬롯 제거
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        // 새 슬롯 생성
        foreach (string item in player.inventory)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
        }
    }
}
