using UnityEngine;

/// <summary>
/// 게임 설정 메뉴(UI) 및 입력을 관리하는 컨트롤러.
/// </summary>
public class SettingsController : Singleton<SettingsController>
{
    [Header("UI")]
    public GameObject Settings;
    public GameObject SettingGroup;

    [Header("Tabs")]
    public CanvasGroup AudioGroup;
    public CanvasGroup ControlGroup;

    /// <summary>
    /// 설정 메뉴 활성화 여부
    /// </summary>
    public bool IsMenuActive => SettingGroup != null && SettingGroup.activeSelf;

    private void Start()
    {
        if (SettingGroup != null)
            SettingGroup.SetActive(false);
    }

    private void Update()
    {
        HandleMenu();
    }

    /// <summary>
    /// 오디오 설정 탭 활성화
    /// </summary>
    public void ClickAudio()
    {
        CanvasGroupOn(AudioGroup);
        CanvasGroupOff(ControlGroup);
    }

    /// <summary>
    /// 조작 설정 탭 활성화
    /// </summary>
    public void ClickControl()
    {
        CanvasGroupOff(AudioGroup);
        CanvasGroupOn(ControlGroup);
    }

    /// <summary>
    /// 설정 메뉴 열기
    /// </summary>
    public void MenuOn()
    {
        if (SettingGroup == null)
            return;

        SettingGroup.SetActive(true);

        CanvasGroupOn(AudioGroup);
        CanvasGroupOff(ControlGroup);

        // 메뉴가 열려있는 동안 게임 입력을 멈춘다.
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// 설정 메뉴 닫기
    /// </summary>
    public void MenuOff()
    {
        if (SettingGroup == null)
            return;

        SettingGroup.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// 설정 메뉴 토글
    /// </summary>
    private void Menu()
    {
        if (SettingGroup.activeSelf)
            MenuOff();
        else
            MenuOn();
    }

    /// <summary>
    /// 설정 메뉴 입력 처리
    /// </summary>
    private void HandleMenu()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.Menu]))
        {
            Menu();
        }
    }

    /// <summary>
    /// CanvasGroup을 활성화
    /// </summary>
    private void CanvasGroupOn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// CanvasGroup을 비활성화
    /// </summary>
    private void CanvasGroupOff(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}