using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate: MonoBehaviour
{
    public float totalWeight = 0f;
    public GameObject gate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Box"))
        {
            totalWeight += collision.GetComponent<Rigidbody2D>().mass;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Box"))
        {
            totalWeight -= collision.GetComponent<Rigidbody2D>().mass;
        }
    }

    private void Update()
    {
        if (totalWeight >= 2)
        {
            gate.SetActive(false);
        }
        else
        {
            gate.SetActive(true);
        }
    }
}