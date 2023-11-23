using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight;
    public int Size;
    public bool isGrounded;
    public bool Still;


    public string BounceBackTag = "POI"; // Set the target tag in the Inspector
    public float radius = 5f; // Set the search radius in the Inspector
    public float forceStrength = 10f; // Adjust the force strength in the Inspector
  
    // List to store hit objects
    private List<GameObject> hitObjects = new List<GameObject>();


    private void Start()
    {

    }
    void Update()
    {
        // Get the position of the player
        Vector3 playerPosition = transform.position;

        //Get horizontal and vertical input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Apply movement
        MovePlayer(moveDirection);

        // Check player is moving
        bool isMoving = (horizontal != 0 || vertical != 0);

        //Ground check
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.15f))
        {
            isGrounded = true;
            if (!Still)
            {
                //ON Ground run once
                Stomping();
                Debug.Log("standing still");
                Still = true;
            }else if (Still)
            {
                //On Ground run on update
                
                
            }
        }
        else
        {
            Still = false;
            isGrounded = false;
            Debug.Log("Flying");

        }
        Debug.DrawRay(playerPosition, Vector3.down * 0.15f, isGrounded ? Color.green : Color.red);

        // Check Move and jump
        if (isGrounded && isMoving)
        {
            Jump();
        }
        else
        {
           

        }
        
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
    void Jump()
    {
        // Apply a vertical force for jumping
        GetComponent<Rigidbody>().velocity = new Vector3(0f, Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(Physics.gravity.y)), 0f);
        Debug.Log("Jump");
    }

    void Stomping()
    {
        Vector3 playerPosition = transform.position;
        // Find all colliders in the specified radius around the player
        Collider[] colliders = Physics.OverlapSphere(playerPosition, radius);

        // Iterate through the colliders and filter based on the target tag
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(BounceBackTag))
            {
                // This collider has the specified tag, so it's a target object
                GameObject targetObject = collider.gameObject;

                // Check if the object is grounded using a script named GroundCheckScript
                bool isGrounded = targetObject.GetComponent<Bounce>().IsGrounded();

                // Calculate the direction from the target object to the player
                Vector3 forceDirection = playerPosition - targetObject.transform.position;

                // Calculate a force based on the distance
                float distance = Vector3.Distance(playerPosition, targetObject.transform.position);
                float forceMagnitude = forceStrength / (distance + 1f);

                // Check if the object is on the ground and has not been pushed recently
                if (isGrounded && !targetObject.GetComponent<Bounce>().HasBeenPushed)
                {
                    // Apply the force to push the object upward
                    targetObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, Mathf.Sqrt(distance * 2f * Mathf.Abs(Physics.gravity.y)), 0f);

                    // Set a flag to indicate that the object has been pushed
                    targetObject.GetComponent<Bounce>().HasBeenPushed = true;
                }
                else //Reset when on air
                {
                    targetObject.GetComponent<Bounce>().HasBeenPushed = false;
                }
            }
        }
    }





}

