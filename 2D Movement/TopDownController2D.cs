using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownController2D : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveX;
    public float moveY;
    public float moveSpeed;
    public bool isFacingRight;

    void Update()
    {
        // Gets the input from unity
        moveX = Input.GetAxisRaw("Horizontal");
        moveY =  Input.GetAxisRaw("Vertical");

        // Flips the sprite
        Flip();
    }

    void FixedUpdate()
    {
        // This makes it moves
        rb.velocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
    }

    void Flip()
    {
        if (isFacingRight && moveX < 0f || !isFacingRight && moveX > 0f)
        {
            Vector2 localScale =  transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
       
    }

}
