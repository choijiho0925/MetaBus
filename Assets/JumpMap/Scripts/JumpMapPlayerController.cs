using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class JumpMapPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 1.0f; // 플레이어 이동 속도

    [Header("Jump Settings")]
    [SerializeField] private float minVerticalJumpForce = 1.0f; // 최소 수직 점프 힘
    [SerializeField] private float maxVerticalJumpForce = 10.0f; // 최대 수직 점프 힘
    [SerializeField] private float maxChargeTime = 2.0f; // 최대 충전 시간 (초)
    [SerializeField] private float minHorizontalJumpForce =1.0f; // 최소 좌우 점프 힘
    [SerializeField] private float maxHorizontalJumpForce = 8.0f; // 최대 좌우 점프 힘

    [Header("Bounce Settings")]
    [SerializeField] private float minBounceSpeed = 1.0f; // 튕김을 발생 시킬 최소 속도
    [SerializeField] private float bouncePowerMultiplier = 1.0f; // 튕기는 힘 배율

    [Header("Player Settings")]
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private LayerMask obstacleLayer; // 충돌체 레이어

    private Rigidbody2D rb; // 플레이어의 Rigidbody2D 컴포넌트

    private float verticalChargeTime = 0.0f; // 수직 점프 충전 시간 (초)
    private float horizontalChargeTime = 0.0f; // 수평 점프 충전 시간 (초)

    [SerializeField]private bool isCharging = false; // 점프 충전 상태
    [SerializeField]private bool isGrounded = true; //공중 점프 방지

    private Vector2 moveDirection = Vector2.zero; // 플레이어 이동 방향
    private Vector2 horizontalJumpDirection = Vector2.zero; // 플레이어 수평 점프 방향

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        HandleInput();
        RotatePlayer();
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {

    }

    private void HandleInput()
    {
        if (isGrounded)
        {
            if (!isCharging) 
            {
                Move(); // 플레이어 이동 처리
                if (Input.GetKey(KeyCode.Space))
                {                   
                    ChargeJumpInput(); // 플레이어 점프 충전 처리
                    isCharging = true; // 충전 시작
                }
            }
            else 
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    ChargeJumpInput(); // 플레이어 점프 충전 처리
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    Jump(); // 플레이어 점프 입력 처리
                }
            }
        }
    }

    private void Move() // 플레이어 이동 처리
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2(horizontal, 0f);
        bool leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (leftPressed || rightPressed)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y); // 이동 속도 설정
        }
        
    }

    private void ChargeJumpInput() // 플레이어 점프 충전 처리
    {
        verticalChargeTime += Time.deltaTime; // 충전 시간 누적
        verticalChargeTime = Mathf.Min(verticalChargeTime, maxChargeTime); // 최대 충전 시간 제한
        
        bool leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        
        if ((leftPressed || rightPressed)) // 충전 중 방향 결정
        {
            horizontalChargeTime += Time.deltaTime;
            horizontalChargeTime = Mathf.Min(horizontalChargeTime, maxChargeTime);

            if (leftPressed && rightPressed)
            {
                horizontalJumpDirection = Vector2.zero;
            }
            else if (leftPressed)
            {
                horizontalJumpDirection = Vector2.left;
            }
            else if (rightPressed)
            {
                horizontalJumpDirection = Vector2.right;
            }
        }
    }

    private void Jump() // 플레이어 점프 입력 처리
    {
        if (verticalChargeTime > 0f || horizontalChargeTime > 0f)
        {
            ExecuteChargedJump();
        }

        // 초기화
        isCharging = false; // 충전 종료
        verticalChargeTime = 0f;
        horizontalChargeTime = 0f;
        horizontalJumpDirection = Vector2.zero;      
    }

    private void ExecuteChargedJump() // 점프 힘 계산
    {
        // 충전 비율 계산 (0~1)
        float verticalRatio = verticalChargeTime / maxChargeTime;
        float horizontalRatio = horizontalChargeTime / maxChargeTime;

        // 최종 점프 힘 계산
        float verticalForce = Mathf.Lerp(minVerticalJumpForce, maxVerticalJumpForce, verticalRatio);
        float horizontalForce = Mathf.Lerp(minHorizontalJumpForce, maxHorizontalJumpForce, horizontalRatio);

        // 위쪽 + 좌/우 방향 (있는 경우)
        Vector2 jumpForce = Vector2.up * verticalForce; // 수직

        if (horizontalJumpDirection != Vector2.zero)
        {
            jumpForce += horizontalJumpDirection.normalized * horizontalForce;
        }

        // 점프 실행
        rb.velocity = Vector2.zero;
        rb.AddForce(jumpForce, ForceMode2D.Impulse);
        isGrounded = false; // 점프 후 공중 상태로 변경
    }

    private void RotatePlayer() // 플레이어 회전
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0)
        {
            playerSpriteRenderer.flipX = false; //오른쪽
        }
        else if (horizontal < 0)
        {
            playerSpriteRenderer.flipX = true; //왼쪽
        }
    }

    private void CollisionDecision(Collision2D collision)
    {
        {
            foreach (ContactPoint2D contact in collision.contacts) //충돌 지점
            {
                Vector2 normal = contact.normal; // 충돌한 표면의 방향
                Vector2 velocity = rb.velocity; // 현재 속도의 방향과 크기
                float speed = velocity.magnitude;
                float angle = Vector2.Angle(normal, Vector2.up); // 충돌한 표면과 수직 방향의 각도
                int layer = collision.collider.gameObject.layer; // 충돌한 물체의 레이어

                Debug.Log($"Layer: {collision.collider.gameObject.layer}, Angle: {Vector2.Angle(contact.normal, Vector2.up)}, Speed : {speed}");

                if (((1 << layer) & obstacleLayer) != 0 && angle < 5f) //바닥
                {
                    isGrounded = true;
                    return;
                }
                else if (((1 << layer) & obstacleLayer) != 0 && angle > 85f && angle < 95f) //옆면
                {
                    Bounce(contact, velocity); // 튕김 처리
                    return;
                }
            }
        }
    }

    private void Bounce(ContactPoint2D contact, Vector2 velocity) // 튕김 처리
    {
        float speed = velocity.magnitude;
        
        if (speed < minBounceSpeed) return; // 튕기는 힘이 너무 약하면 무시

        // 수평 방향 결정 (왼쪽 벽이면 오른쪽으로 튕기기)
        Vector2 horizontalBounce = contact.point.x < transform.position.x ? Vector2.right : Vector2.left;

        // 수직 방향이 남아있으면, 위로 튕기기
        Vector2 verticalBounce = velocity.y > 0.7f ? Vector2.up : Vector2.zero;

        // 최종 튕김 방향 = 수평 + 수직 normalized를 통해 힘 분산을 방지
        Vector2 bounceDirection = (horizontalBounce + verticalBounce).normalized;

        rb.velocity = Vector2.zero; // 튕기기 전 속도 제거
        rb.AddForce(bounceDirection * speed * bouncePowerMultiplier, ForceMode2D.Impulse); // 튕김 힘 적용
    }
    

    private void Landing() // 착지 시 마찰력 없음
    {
        Vector2 velocity = rb.velocity;
        velocity.x = 0f;
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.collider.gameObject.layer) & obstacleLayer) != 0)
        {
            CollisionDecision(collision);
            if (isGrounded) 
            {
                Landing(); // 착지 시 마찰력 없음
            }
        }

        if (collision.gameObject.CompareTag("Sword")) // 급하게 잡업하느랴 여기에 집어넣었습니다 ㅠㅠ
        {
           GoMetabus();
        }
    }

    public void GoMetabus() // 급하게 잡업하느랴 여기에 집어넣었습니다 ㅠㅠ
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f; // 게임 속도 초기화
    }
}



