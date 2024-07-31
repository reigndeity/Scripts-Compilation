using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveX;
    public float moveSpeed;
    public bool isFacingRight;
    public LayerMask groundMask;
    public float jumpingPower;
    public bool isGrounded;
    

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
            CharacterMovement();
            Flip();
    }

    private void CharacterMovement()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && rb.velocity.x != 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.8f);
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && moveX < 0f || !isFacingRight && moveSpeed > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
