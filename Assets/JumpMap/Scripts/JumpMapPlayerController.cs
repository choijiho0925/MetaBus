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
    [SerializeField] private float moveSpeed = 1.0f; // �÷��̾� �̵� �ӵ�

    [Header("Jump Settings")]
    [SerializeField] private float minVerticalJumpForce = 1.0f; // �ּ� ���� ���� ��
    [SerializeField] private float maxVerticalJumpForce = 10.0f; // �ִ� ���� ���� ��
    [SerializeField] private float maxChargeTime = 2.0f; // �ִ� ���� �ð� (��)
    [SerializeField] private float minHorizontalJumpForce =1.0f; // �ּ� �¿� ���� ��
    [SerializeField] private float maxHorizontalJumpForce = 8.0f; // �ִ� �¿� ���� ��

    [Header("Bounce Settings")]
    [SerializeField] private float minBounceSpeed = 1.0f; // ƨ���� �߻� ��ų �ּ� �ӵ�
    [SerializeField] private float bouncePowerMultiplier = 1.0f; // ƨ��� �� ����

    [Header("Player Settings")]
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private LayerMask obstacleLayer; // �浹ü ���̾�

    private Rigidbody2D rb; // �÷��̾��� Rigidbody2D ������Ʈ

    private float verticalChargeTime = 0.0f; // ���� ���� ���� �ð� (��)
    private float horizontalChargeTime = 0.0f; // ���� ���� ���� �ð� (��)

    [SerializeField]private bool isCharging = false; // ���� ���� ����
    [SerializeField]private bool isGrounded = true; //���� ���� ����

    private Vector2 moveDirection = Vector2.zero; // �÷��̾� �̵� ����
    private Vector2 horizontalJumpDirection = Vector2.zero; // �÷��̾� ���� ���� ����

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
                Move(); // �÷��̾� �̵� ó��
                if (Input.GetKey(KeyCode.Space))
                {                   
                    ChargeJumpInput(); // �÷��̾� ���� ���� ó��
                    isCharging = true; // ���� ����
                }
            }
            else 
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    ChargeJumpInput(); // �÷��̾� ���� ���� ó��
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    Jump(); // �÷��̾� ���� �Է� ó��
                }
            }
        }
    }

    private void Move() // �÷��̾� �̵� ó��
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2(horizontal, 0f);
        bool leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (leftPressed || rightPressed)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y); // �̵� �ӵ� ����
        }
        
    }

    private void ChargeJumpInput() // �÷��̾� ���� ���� ó��
    {
        verticalChargeTime += Time.deltaTime; // ���� �ð� ����
        verticalChargeTime = Mathf.Min(verticalChargeTime, maxChargeTime); // �ִ� ���� �ð� ����
        
        bool leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        
        if ((leftPressed || rightPressed)) // ���� �� ���� ����
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

    private void Jump() // �÷��̾� ���� �Է� ó��
    {
        if (verticalChargeTime > 0f || horizontalChargeTime > 0f)
        {
            ExecuteChargedJump();
        }

        // �ʱ�ȭ
        isCharging = false; // ���� ����
        verticalChargeTime = 0f;
        horizontalChargeTime = 0f;
        horizontalJumpDirection = Vector2.zero;      
    }

    private void ExecuteChargedJump() // ���� �� ���
    {
        // ���� ���� ��� (0~1)
        float verticalRatio = verticalChargeTime / maxChargeTime;
        float horizontalRatio = horizontalChargeTime / maxChargeTime;

        // ���� ���� �� ���
        float verticalForce = Mathf.Lerp(minVerticalJumpForce, maxVerticalJumpForce, verticalRatio);
        float horizontalForce = Mathf.Lerp(minHorizontalJumpForce, maxHorizontalJumpForce, horizontalRatio);

        // ���� + ��/�� ���� (�ִ� ���)
        Vector2 jumpForce = Vector2.up * verticalForce; // ����

        if (horizontalJumpDirection != Vector2.zero)
        {
            jumpForce += horizontalJumpDirection.normalized * horizontalForce;
        }

        // ���� ����
        rb.velocity = Vector2.zero;
        rb.AddForce(jumpForce, ForceMode2D.Impulse);
        isGrounded = false; // ���� �� ���� ���·� ����
    }

    private void RotatePlayer() // �÷��̾� ȸ��
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0)
        {
            playerSpriteRenderer.flipX = false; //������
        }
        else if (horizontal < 0)
        {
            playerSpriteRenderer.flipX = true; //����
        }
    }

    private void CollisionDecision(Collision2D collision)
    {
        {
            foreach (ContactPoint2D contact in collision.contacts) //�浹 ����
            {
                Vector2 normal = contact.normal; // �浹�� ǥ���� ����
                Vector2 velocity = rb.velocity; // ���� �ӵ��� ����� ũ��
                float speed = velocity.magnitude;
                float angle = Vector2.Angle(normal, Vector2.up); // �浹�� ǥ��� ���� ������ ����
                int layer = collision.collider.gameObject.layer; // �浹�� ��ü�� ���̾�

                Debug.Log($"Layer: {collision.collider.gameObject.layer}, Angle: {Vector2.Angle(contact.normal, Vector2.up)}, Speed : {speed}");

                if (((1 << layer) & obstacleLayer) != 0 && angle < 5f) //�ٴ�
                {
                    isGrounded = true;
                    return;
                }
                else if (((1 << layer) & obstacleLayer) != 0 && angle > 85f && angle < 95f) //����
                {
                    Bounce(contact, velocity); // ƨ�� ó��
                    return;
                }
            }
        }
    }

    private void Bounce(ContactPoint2D contact, Vector2 velocity) // ƨ�� ó��
    {
        float speed = velocity.magnitude;
        
        if (speed < minBounceSpeed) return; // ƨ��� ���� �ʹ� ���ϸ� ����

        // ���� ���� ���� (���� ���̸� ���������� ƨ���)
        Vector2 horizontalBounce = contact.point.x < transform.position.x ? Vector2.right : Vector2.left;

        // ���� ������ ����������, ���� ƨ���
        Vector2 verticalBounce = velocity.y > 0.7f ? Vector2.up : Vector2.zero;

        // ���� ƨ�� ���� = ���� + ���� normalized�� ���� �� �л��� ����
        Vector2 bounceDirection = (horizontalBounce + verticalBounce).normalized;

        rb.velocity = Vector2.zero; // ƨ��� �� �ӵ� ����
        rb.AddForce(bounceDirection * speed * bouncePowerMultiplier, ForceMode2D.Impulse); // ƨ�� �� ����
    }
    

    private void Landing() // ���� �� ������ ����
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
                Landing(); // ���� �� ������ ����
            }
        }

        if (collision.gameObject.CompareTag("Sword")) // ���ϰ� ����ϴ��� ���⿡ ����־����ϴ� �Ф�
        {
           GoMetabus();
        }
    }

    public void GoMetabus() // ���ϰ� ����ϴ��� ���⿡ ����־����ϴ� �Ф�
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f; // ���� �ӵ� �ʱ�ȭ
    }
}



