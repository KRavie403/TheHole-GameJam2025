using UnityEngine;

public class HoleGrowthController : MonoBehaviour
{
    [Header("Growth Settings")]
    public float maxScaleMultiplier = 3f;
    [Tooltip("각 미션 클리어시 증가 비율")]
    public float[] growthSteps = { 0.8f, 1.0f, 1.5f, 2f, 3f };

    private int currentStep = 0;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    /// <summary>
    /// 미션 클리어 시 이 함수를 외부에서 호출
    /// </summary>
    public void OnMissionCleared()
    {
        if (currentStep >= growthSteps.Length)
        {
            Debug.Log("이미 최대 크기입니다!");
            return;
        }

        float nextScale = growthSteps[currentStep];
        transform.localScale = originalScale * nextScale;

        Debug.Log($"[VFXGrowthController] {gameObject.name} 스케일 {nextScale}배로 변경됨");

        currentStep++;
    }

    /// <summary>
    /// 초기 크기로 리셋
    /// </summary>
    public void ResetScale()
    {
        currentStep = 0;
        transform.localScale = originalScale;
        Debug.Log("[VFXGrowthController] 스케일 초기화 완료");
    }
}
