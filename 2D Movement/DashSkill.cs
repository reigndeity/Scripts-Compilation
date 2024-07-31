using System.Collections;
using UnityEngine;

public class DashSkill : MonoBehaviour
{
    public Rigidbody2D rb;
    public TrailRenderer tr;

    public bool canDash = true;
    public bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    public bool IsDashing => isDashing;

    // Direction for dashing as a Vector2
    private Vector2 dashDirection = Vector2.right; // Default to right

    public void StartDash()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }

    public void SetDashDirection(Vector2 direction)
    {
        dashDirection = direction;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        Vector2 dashVelocity = dashDirection * dashingPower;
        rb.velocity = dashVelocity;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
