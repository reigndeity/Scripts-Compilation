using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker2D : MonoBehaviour
{
    public PlayerController2D playerController2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            playerController2D.isGrounded = true;
        }
    }
}