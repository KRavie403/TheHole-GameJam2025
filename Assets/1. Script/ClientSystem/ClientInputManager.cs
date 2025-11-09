using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInputManager : MonoBehaviour
{
    public float moveSpeed = 5f;          // 이동 속도
    public float gravity = -9.81f;        // 중력값
    public float groundCheckDistance = 0.2f; // 바닥 체크 거리
    public LayerMask groundMask;          // "Ground" 레이어 마스크

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private Animator _anim;

    private bool isMove = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleMoveInput();
        if (_anim != null)
        {
            _anim.SetBool("IsMoving", isMove);
        }
    }

    private void HandleMoveInput()
    {
        isMove = false;

        float horizontal = Input.GetAxisRaw("Horizontal");  // A/D, 좌우 화살표
        float vertical = Input.GetAxisRaw("Vertical");      // W/S, 상하 화살표

        Vector3 moveVector = new Vector3(horizontal, 0, vertical).normalized;

        // 바닥 체크 (경사면 포함)
        Vector3 groundCheckPos = transform.position + Vector3.down * (controller.height / 2f - controller.skinWidth + 0.1f);
        isGrounded = Physics.CheckSphere(groundCheckPos, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f; // 바닥에 붙이기 위한 약간의 보정

        if (moveVector.magnitude > 0)
        {
            isMove = true;

            // 카메라 기준 이동 (TPS 카메라 기준 이동을 원하면 Camera.main.transform.forward 사용)
            Vector3 moveDir = transform.TransformDirection(moveVector);

            controller.Move(moveDir * moveSpeed * Time.deltaTime);

            AudioManager.Inst.PlayCharacterEffect(3);
        }

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Blend Tree 업데이트
        float MotionSpeed = moveSpeed;
        _anim.SetFloat("MoveAxisX", Mathf.Clamp(horizontal * MotionSpeed, -1f, 1f));
        _anim.SetFloat("MoveAxisY", Mathf.Clamp(vertical * MotionSpeed, -1f, 1f));

        // 기존 isMove bool
        _anim.SetBool("IsMoving", isMove);
    }
}
