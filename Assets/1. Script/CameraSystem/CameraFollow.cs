using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;                        // 따라갈 캐릭터
    public Transform firstPersonViewPoint;         // 1인칭 위치 (캐릭터 눈 위치)

    [Header("Camera Settings")]
    public float distance = 4.0f;                  // TPS 캐릭터 뒤 거리
    public float height = 2.0f;                    // TPS 캐릭터 위 높이
    public float smoothSpeed = 10f;                // 카메라 이동 부드러움
    public bool isFirstPerson = false;             // 시점 모드: false = 3인칭, true = 1인칭

    [Header("Rotation Settings")]
    public float mouseSensitivity = 3.0f;
    public float minPitch = -30f;
    public float maxPitch = 60f;
    public float rotationSmooth = 10f;            // 캐릭터 회전 부드러움

    [Header("Movement Settings")]
    public float moveSpeed = 5f;                   // 캐릭터 이동 속도

    private float yaw;
    private float pitch;
    private CharacterController characterController;

    private void Start()
    {
        if (target == null)
        {
            Logger.LogWarning("CameraFollow의 Target이 지정되지 않았습니다.");
            return;
        }

        characterController = target.GetComponent<CharacterController>();
        if (characterController == null)
            Logger.LogWarning("Target 객체에 CharacterController 컴포넌트를 찾을 수 없습니다.");

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (!target) return;

        // 마우스 입력 회전
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion camRotation = Quaternion.Euler(pitch, yaw, 0);

        // 카메라 위치 결정
        Vector3 desiredPosition = isFirstPerson && firstPersonViewPoint != null
            ? firstPersonViewPoint.position
            : target.position - camRotation * Vector3.forward * distance + Vector3.up * height;

        // 카메라 이동 적용
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = camRotation;

        // 캐릭터 이동 & 회전 처리 (TPS)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            // 카메라 기준으로 이동 방향 계산
            Quaternion yawRotation = Quaternion.Euler(0, yaw, 0);
            Vector3 moveDir = yawRotation * inputDir;

            // 캐릭터 회전 적용
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            target.rotation = Quaternion.Slerp(target.rotation, targetRotation, rotationSmooth * Time.deltaTime);

            // 이동 적용
            if (characterController != null)
            {
                characterController.Move(moveDir * moveSpeed * Time.deltaTime);
            }
        }
    }



    /// <summary>
    /// 시점 전환
    /// </summary>
    public void SetFirstPerson(bool firstPerson)
    {
        isFirstPerson = firstPerson;
    }
}
