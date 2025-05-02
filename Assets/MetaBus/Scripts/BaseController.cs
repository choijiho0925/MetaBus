using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected AnimationHandler animationHandler;

    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // 현재 바라보는 방향
    public Vector2 LookDirection { get { return lookDirection; } }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    protected virtual void HandleAction()
    {

    }

    protected void Movement(Vector2 direction)
    {
        direction = direction * 5.0f; 

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    protected void Rotate(Vector2 direction)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0)
        {
            characterRenderer.flipX = false; 
        }
        else if (horizontal < 0)
        {
            characterRenderer.flipX = true; 
        }
    }
}
