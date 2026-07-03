using UnityEngine;

public class PortalEffect : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeedX = 30f;  // X축 회전 속도 (degrees/sec)
    public float rotationSpeedY = 0f;    // Y축 회전 속도 (degrees/sec)
    public float rotationSpeedZ = 500f;  // Z축 회전 속도 (degrees/sec)

    [Header("Scaling Settings")]
    public float duration = 60f;       // 1분간 최대 스케일
    public float maxScaleMultiplier = 8f;

    private Vector3 initialScale;
    private float elapsedTime = 0f;

    private Quaternion initialRotation;

    void Start()
    {
        initialScale = transform.localScale;
        initialRotation = transform.rotation; // 현재 Y=-120도 포함 초기 회전 저장
    }

    void Update()
    {
        // 옆으로(X축) 회전: Quaternion으로 현재 회전에 누적
        float deltaZ = rotationSpeedZ * Time.deltaTime;

        transform.Rotate(0f, 0f, deltaZ, Space.Self);

        // 점점 커지기
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(1f, maxScaleMultiplier, elapsedTime / duration);
            transform.localScale = initialScale * scaleFactor;
        }
    }
}
