using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ClientInputManager : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _gravity = -9.81f;

    [Header("References")]
    [SerializeField] private InGameUIManager UIManager;   // UI 매니저 연결
    [SerializeField] private float _interactRange = 2f;              // 상호작용 거리

    [SerializeField] private float rotationSpeed = 15f;

    private CharacterController controller;
    private Vector3 _velocity;
    private bool _isGrounded;
    private Animator _anim;

    [SerializeField] private bool canProcessInput = true; 
    public bool CanProcessInput => canProcessInput; 
    public void SetInputActive(bool active) 
    { 
        canProcessInput = active; 
    }


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (canProcessInput)
        {
            HandleUIInput();        // UI 관련 입력 처리
            HandleMove();           // 이동 처리    
        }
    }

    private void HandleUIInput()
    {
        // KeyGuide
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.KeyGuide]))
        {
            UIManager?.ToggleKeyGuide();
        }

        #region Menu
        //Menu
        //if (Input.GetKeyDown(KeySetting.keys[KeyAction.Menu]))
        //{
        //    UIManager?.ToggleMenu();
        //}
        #endregion
    }

    private void HandleMove()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // KeySetting 기반 입력
        if (Input.GetKey(KeySetting.keys[KeyAction.Forward])) vertical += 1f;
        if (Input.GetKey(KeySetting.keys[KeyAction.Backward])) vertical -= 1f;
        if (Input.GetKey(KeySetting.keys[KeyAction.Right])) horizontal += 1f;
        if (Input.GetKey(KeySetting.keys[KeyAction.Left])) horizontal -= 1f;

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        // 카메라 기준 이동
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDir = camForward * vertical + camRight * horizontal;

        if (moveDir.magnitude > 1f)
            moveDir.Normalize();

        // 회전
        if (moveDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime);
        }

        // 중력
        _isGrounded = controller.isGrounded;
        if (_isGrounded && _velocity.y < 0) _velocity.y = -2f;
        _velocity.y += _gravity * Time.deltaTime;

        // 점프 처리
        if (_isGrounded &&
        Input.GetKeyDown(KeySetting.keys[KeyAction.Jump]))
        {
            _velocity.y = Mathf.Sqrt(2f * Mathf.Abs(_gravity) * 1.5f);

            if (_anim != null)
            {
                _anim.SetTrigger("Jump");
            }
        }

        // 이동 적용
        Vector3 finalMove = moveDir * _moveSpeed;
        finalMove.y = _velocity.y;

        controller.Move(finalMove * Time.deltaTime);

        // 애니메이션 업데이트
        if (_anim != null)
        {
            Vector3 localMove = transform.InverseTransformDirection(moveDir);

            _anim.SetFloat(
                "MoveAxisX",
                Mathf.Clamp(localMove.x, -1f, 1f),
                0.1f,
                Time.deltaTime);

            _anim.SetFloat(
                "MoveAxisY",
                Mathf.Clamp(localMove.z, -1f, 1f),
                0.1f,
                Time.deltaTime);

            _anim.SetBool("IsMoving", moveDir.sqrMagnitude > 0.01f);
        }
    }

}
