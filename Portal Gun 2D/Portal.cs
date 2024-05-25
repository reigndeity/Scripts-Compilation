using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private Transform destination;

    public bool isOrange;
    public float distance = 0.2f;

    void Start()
    {
        if (isOrange == false)
        {
            destination = GameObject.FindGameObjectWithTag("OrangePortal").GetComponent<Transform>();
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("BluePortal").GetComponent<Transform>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Vector2.Distance(transform.position, collision.transform.position) > distance && !collision.CompareTag("Wall") || 
            collision.gameObject.tag == "Box" && Vector2.Distance(transform.position, collision.transform.position) > distance && !collision.CompareTag("Wall"))
        {
            collision.transform.position = new Vector2(destination.position.x, destination.position.y);
        }
    }

}