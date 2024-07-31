using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveX;
    public float moveY;
    public float moveSpeed = 5f;
    public bool isFacingRight;

    public Rigidbody2D rb;
    public DashSkill dashSkillScript;

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        Flip();

        if (dashSkillScript.IsDashing)
        {
            return;
        }

        // Activating The Dash Skill
        if (Input.GetKeyDown(KeyCode.J))
        {
            // Determine dash direction based on both horizontal and vertical input
            Vector2 dashDirection = new Vector2(moveX, moveY).normalized;

            // If no input is provided, use the player's current facing direction
            if (dashDirection == Vector2.zero)
            {
                // Use the player's facing direction for the default dash direction
                if (isFacingRight)
                {
                    dashDirection = Vector2.right; // Default to right if facing right
                }
                else
                {
                    dashDirection = Vector2.left; // Default to left if facing left
                }
            }
            dashSkillScript.SetDashDirection(dashDirection);
            dashSkillScript.StartDash();
        }
    }

    private void FixedUpdate()
    {
        if (dashSkillScript.IsDashing)
        {
            return;
        }

        // Makes The Player Move
        rb.velocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
    }

    private void Flip()
    {
        if (isFacingRight && moveX < 0f || !isFacingRight && moveX > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
