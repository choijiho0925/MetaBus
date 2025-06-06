using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapPlane : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;
    public bool isDead = false;

    bool isFlap = false;

    public bool godMode = false;

    FlapPlaneManager gameManager;
    FlapPlaneUIManager gameUIManager;

    void Start()
    {
        gameManager = FlapPlaneManager.Instance;
        gameUIManager = FlapPlaneUIManager.Instance;
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDead)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false ;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp( (_rigidbody.velocity.y * 10), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;

        if (isDead) return;

        isDead = true;

        animator.SetInteger("IsDie", 1);

        _rigidbody.gravityScale = 30f;

        gameUIManager?.ChangeState(UIState.Score);
        gameManager?.GameOver();
    }
}
