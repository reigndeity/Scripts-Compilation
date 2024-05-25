using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    Camera cam;
    public Transform pivot;

    public GameObject bluePortal;
    public GameObject orangePortal;

    public float maxRayDistance; // Maximum distance the ray can travel
    public float pickUpDistance = 5f; // Maximum distance to pick up objects
    public float dropDistance = 5f; // Maximum distance to drop objects
    public LayerMask ignoreLayers; // Layer mask to exclude specific layers
    public LayerMask pickableLayer; // Layer mask to specify pickable objects

    private GameObject pickedObject;
    private Collider2D pickedObjectCollider; // Store the collider of the picked object
    public KeyCode pickUpKey = KeyCode.F; // Key to pick up and place objects

    void Start()
    {
        cam = Camera.main;

        // Set up the ignore layer mask to ignore the "Portal" layer
        ignoreLayers = LayerMask.GetMask("Portal");
        ignoreLayers = ~ignoreLayers; // Invert the mask to ignore specified layers
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.position);

        Vector3 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 worldMousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        Vector2 direction = (worldMousePos - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRayDistance, ignoreLayers);

        Vector2 targetPosition;

        if (hit.collider != null)
        {
            targetPosition = hit.point;
            Debug.DrawRay(transform.position, direction * hit.distance, Color.white);
        }
        else
        {
            targetPosition = (Vector2)transform.position + direction * maxRayDistance;
            Debug.DrawRay(transform.position, direction * maxRayDistance, Color.red);
        }

        // Move BluePortal with left click
        if (Input.GetMouseButtonDown(0))
        {
            bluePortal.transform.position = targetPosition;
            Debug.Log("Moved BluePortal to " + targetPosition);
        }

        // Move OrangePortal with right click
        if (Input.GetMouseButtonDown(1))
        {
            orangePortal.transform.position = targetPosition;
            Debug.Log("Moved OrangePortal to " + targetPosition);
        }

        // Pick up and place objects
        if (Input.GetKeyDown(pickUpKey))
        {
            if (pickedObject == null)
            {
                // Attempt to pick up an object
                RaycastHit2D pickUpHit = Physics2D.Raycast(transform.position, direction, maxRayDistance, pickableLayer);
                if (pickUpHit.collider != null)
                {
                    float distanceToPickUp = Vector2.Distance(transform.position, pickUpHit.point);
                    if (distanceToPickUp <= pickUpDistance)
                    {
                        pickedObject = pickUpHit.collider.gameObject;
                        pickedObjectCollider = pickedObject.GetComponent<Collider2D>();
                        if (pickedObjectCollider != null)
                        {
                            pickedObjectCollider.enabled = false; // Disable the collider
                        }
                        Debug.Log("Picked up " + pickedObject.name);
                    }
                }
            }
            else
            {
                // Calculate the drop distance
                float distanceToDrop = Vector2.Distance(transform.position, targetPosition);
                if (distanceToDrop > dropDistance)
                {
                    // Limit the target position to the drop distance
                    targetPosition = (Vector2)transform.position + direction * dropDistance;
                }

                // Place the picked-up object
                pickedObject.transform.position = targetPosition;
                if (pickedObjectCollider != null)
                {
                    pickedObjectCollider.enabled = true; // Enable the collider
                }
                Debug.Log("Placed " + pickedObject.name + " at " + targetPosition);
                pickedObject = null;
                pickedObjectCollider = null;
            }
        }

        // Move the picked-up object with the mouse, without changing its initial distance
        if (pickedObject != null)
        {
            Vector3 newMousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.WorldToScreenPoint(pickedObject.transform.position).z));
            pickedObject.transform.position = new Vector3(newMousePos.x, newMousePos.y, pickedObject.transform.position.z);
        }
    }
}
