using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight;
    public int Size;
    public GameObject[] DragonPrefab;
    public bool isGrounded;

    // List to store hit objects
    private List<GameObject> hitObjects = new List<GameObject>();

    void Update()
    {
        // Get the half height of the object
        float halfHeight = GetComponent<Collider>().bounds.extents.y;

        // Calculate the starting position for the raycast from the bottom
        Vector3 raycastStartPosition = transform.position - new Vector3(0f, halfHeight, 0f);

        // Perform the raycast from the new starting position
        RaycastHit hit;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Physics.Raycast(raycastStartPosition, Vector3.down, out hit, 0.5f))
        {
            isGrounded = true;
        }
        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Apply movement
        MovePlayer(moveDirection);

        // Check for jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        // Debug draw to visualize the ray
        Debug.DrawRay(raycastStartPosition, Vector3.down * 0.5f, Color.green);
    }

    void Jump()
    {
        // Apply a vertical force for jumping
        GetComponent<Rigidbody>().velocity = new Vector3(0f, Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(Physics.gravity.y)), 0f);

        // Bounce the POI around the player
        BouncePOI();
    }

    void MovePlayer(Vector3 moveDirection)
    {
        // Convert input direction to world space
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Calculate movement direction in world space
        Vector3 desiredMoveDirection = (forward * moveDirection.z + right * moveDirection.x).normalized;

        // Move the player
        transform.Translate(desiredMoveDirection * moveSpeed * Time.deltaTime);
    }

    void BouncePOI()
    {
        // Find all nearby POI objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, Size * 2f);



        foreach (Collider col in colliders)
        {
            // Check if the object is a POI (adjust the tag or layer as needed)
            if (col.CompareTag("POI"))
            {
                // Bounce the POI (you may need to adjust this based on your specific requirements)
                col.GetComponent<Rigidbody>().velocity = Vector3.up * 5f;
            }
        }
    }

}

