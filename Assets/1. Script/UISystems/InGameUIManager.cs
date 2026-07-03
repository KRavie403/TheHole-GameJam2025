using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인게임 UI를 관리하는 클래스.
/// 키 가이드, NPC 대화창, 상호작용 버튼의 표시 및 갱신을 담당.
/// </summary>
public class InGameUIManager : MonoBehaviour
{
    [Header("Key Guide UI")]
    [SerializeField] private GameObject keyGuidePanel;
    [SerializeField] private GameObject keyGuideOnOff;
    [SerializeField] private Image keyGuideImage;
    [SerializeField] private TMP_Text keyGuideText;

    [Header("Interaction UI")]
    [SerializeField] private GameObject interactButton;
    [SerializeField] private RectTransform interactButtonRect;

    [Header("Dialog UI")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text npcNameText;
    [SerializeField] private TMP_Text dialogText;

    private Camera mainCamera;

    public bool IsKeyGuideActive => keyGuidePanel != null && keyGuidePanel.activeSelf;

    private void Awake()
    {
        mainCamera = Camera.main;

        if (keyGuideImage == null && keyGuideOnOff != null)
            keyGuideImage = keyGuideOnOff.GetComponentInChildren<Image>();

        if (keyGuideImage == null)
            Logger.LogError("KeyGuide Image를 찾을 수 없습니다.", this);

        SetKeyGuide(true);
        ClearDialog();
        HideInteractButton();
    }

    #region Key Guide

    public void KeyGuideOn() => SetKeyGuide(true);

    public void KeyGuideOff() => SetKeyGuide(false);

    public void ToggleKeyGuide()
    {
        if (keyGuidePanel == null)
            return;

        SetKeyGuide(!keyGuidePanel.activeSelf);
    }

    private void SetKeyGuide(bool active)
    {
        keyGuidePanel.SetActive(active);

        if (keyGuideImage != null)
        {
            Color color = keyGuideImage.color;
            color.a = active ? 1f : 0.5f;
            keyGuideImage.color = color;
        }

        if (keyGuideText != null)
        {
            keyGuideText.text = active ? "Key Guide Off" : "Key Guide On";
            keyGuideText.alpha = active ? 1f : 0.5f;
        }
    }

    public void ShowKeyGuideToggle()
    {
        if (keyGuideOnOff != null)
            keyGuideOnOff.SetActive(true);
    }

    public void HideKeyGuideToggle()
    {
        if (keyGuideOnOff != null)
            keyGuideOnOff.SetActive(false);
    }

    #endregion

    #region Dialog

    public void SetDialog(string npcName, string text)
    {
        dialogPanel?.SetActive(true);

        if (npcNameText != null)
            npcNameText.text = npcName;

        if (dialogText != null)
            dialogText.text = text;
    }

    public void ClearDialog()
    {
        dialogPanel?.SetActive(false);

        if (npcNameText != null)
            npcNameText.text = string.Empty;

        if (dialogText != null)
            dialogText.text = string.Empty;
    }

    #endregion

    #region Interaction

    /// <summary>
    /// 상호작용 버튼을 표시하고 월드 좌표에 맞춰 위치를 갱신
    /// </summary>
    public void ShowInteractButton(Vector3 worldPos)
    {
        interactButton.SetActive(true);
        UpdateInteractButton(worldPos);
    }

    /// <summary>
    /// 상호작용 버튼을 숨김
    /// </summary>
    public void HideInteractButton()
    {
        interactButton.SetActive(false);
    }

    /// <summary>
    /// 월드 좌표를 화면 좌표로 변환하여 상호작용 버튼 위치를 갱신
    /// </summary>
    public void UpdateInteractButton(Vector3 worldPos)
    {
        if (!interactButton.activeSelf)
            return;

        interactButtonRect.position =
            mainCamera.WorldToScreenPoint(worldPos);
    }

    #endregion
}